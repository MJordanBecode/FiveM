using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lostgen.Client.Modules.CharacterCreation
{
    public class CharacterCreationClient : BaseScript
    {
        private int _cam = -1;
        private Guid _currentPlayerId;

        public CharacterCreationClient()
        {
            // Lancement de la création du personnage
            EventHandlers["lostgen:client:startCharacterCreation"] += new Action<string>(StartCharacterCreation);

            // Spawn après la création
            EventHandlers["lostgen:client:spawnPlayerAfterCreation"] += new Action<float, float, float>(OnSpawnPlayerAfterCreation);

            // Sauvegarde du personnage
            API.RegisterNuiCallbackType("saveCharacter");
            EventHandlers["__cfx_nui:saveCharacter"] += new Action<IDictionary<string, object>, CallbackDelegate>(OnSaveCharacterNui);

            // Changement de sexe
            API.RegisterNuiCallbackType("changeGender");
            EventHandlers["__cfx_nui:changeGender"] += new Action<IDictionary<string, object>, CallbackDelegate>(OnChangeGenderNui);

            // Génétique
            API.RegisterNuiCallbackType("updateHeadBlend");
            EventHandlers["__cfx_nui:updateHeadBlend"] += new Action<IDictionary<string, object>, CallbackDelegate>((data, cb) =>
            {
                int ped = Game.PlayerPed.Handle;

                int shapeFirstID = Convert.ToInt32(data["shapeFirstID"]);
                int shapeSecondID = Convert.ToInt32(data["shapeSecondID"]);

                float shapeMix = Convert.ToSingle(data["shapeMix"]) / 100f;
                float skinMix = Convert.ToSingle(data["skinMix"]) / 100f;

                API.SetPedHeadBlendData(
                    ped,
                    shapeFirstID,
                    shapeSecondID,
                    0,
                    shapeFirstID,
                    shapeSecondID,
                    0,
                    shapeMix,
                    skinMix,
                    0.0f,
                    false
                );

                cb(new { status = "ok" });
            });
        }

        private async void StartCharacterCreation(string playerIdStr)
        {
            Debug.WriteLine("========== CHARACTER CREATION ==========");

            if (!Guid.TryParse(playerIdStr, out _currentPlayerId))
            {
                Debug.WriteLine("GUID invalide.");
                return;
            }

            Debug.WriteLine("GUID OK.");

            Vector3 spawnPos = new Vector3(402.8f, -996.2f, -99.0f);

            // Chargement du modèle
            Model model = new Model("mp_m_freemode_01");
            await model.Request(5000);

            if (!model.IsLoaded)
            {
                Debug.WriteLine("Impossible de charger le modèle.");
                return;
            }

            await Game.Player.ChangeModel(model);
            await Delay(500);

            int ped = Game.PlayerPed.Handle;

            Debug.WriteLine($"PED : {ped}");
            Debug.WriteLine($"MODEL HASH : {Game.PlayerPed.Model.Hash}");

            API.NetworkResurrectLocalPlayer(
                spawnPos.X,
                spawnPos.Y,
                spawnPos.Z,
                180.0f,
                true,
                false
            );

            API.SetPedDefaultComponentVariation(ped);
            API.SetEntityVisible(ped, true, false);
            API.SetEntityInvincible(ped, false);

            API.RequestCollisionAtCoord(spawnPos.X, spawnPos.Y, spawnPos.Z);

            while (!API.HasCollisionLoadedAroundEntity(ped))
            {
                await Delay(0);
            }

            API.SetEntityCoords(
                ped,
                spawnPos.X,
                spawnPos.Y,
                spawnPos.Z,
                false,
                false,
                false,
                false
            );

            API.SetEntityHeading(ped, 180.0f);
            API.FreezeEntityPosition(ped, true);

            model.MarkAsNoLongerNeeded();

            // Caméra
            _cam = API.CreateCamWithParams(
                "DEFAULT_SCRIPTED_CAMERA",
                402.8f,
                -997.8f,
                -98.3f,
                0.0f,
                0.0f,
                0.0f,
                50.0f,
                true,
                2
            );

            API.PointCamAtCoord(_cam, 402.8f, -996.2f, -98.5f);
            API.RenderScriptCams(true, true, 1000, true, false);

            // Ouvre le NUI
            API.SetNuiFocus(true, true);
            API.SendNuiMessage("{\"action\":\"openCharacterCreation\"}");

            Debug.WriteLine("Création du personnage ouverte.");
        }

        private void OnSaveCharacterNui(IDictionary<string, object> data, CallbackDelegate callback)
        {
            try
            {
                API.SetNuiFocus(false, false);

                string firstName = data.ContainsKey("firstName") ? data["firstName"].ToString() : "John";
                string lastName = data.ContainsKey("lastName") ? data["lastName"].ToString() : "Doe";
                DateTime birthDay = data.ContainsKey("birthDay")
                    ? DateTime.Parse(data["birthDay"].ToString())
                    : DateTime.Now.AddYears(-20);

                char gender = data.ContainsKey("gender")
                    ? data["gender"].ToString()[0]
                    : 'M';

                short height = data.ContainsKey("height")
                    ? Convert.ToInt16(data["height"])
                    : (short)180;

                TriggerServerEvent(
                    "lostgen:server:saveCharacter",
                    _currentPlayerId.ToString(),
                    firstName,
                    lastName,
                    birthDay.ToString("o"),
                    gender.ToString(),
                    height
                );

                callback(new { status = "ok" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] {ex}");
                callback(new
                {
                    status = "error",
                    message = ex.Message
                });
            }
        }

        private void OnSpawnPlayerAfterCreation(float x, float y, float z)
        {
            int ped = Game.PlayerPed.Handle;

            API.FreezeEntityPosition(ped, false);
            API.SetEntityCoords(ped, x, y, z, false, false, false, false);
            API.SetEntityHeading(ped, 0.0f);

            API.RenderScriptCams(false, true, 1000, true, false);

            if (API.DoesCamExist(_cam))
            {
                API.DestroyCam(_cam, false);
                _cam = -1;
            }
        }

        private async void OnChangeGenderNui(IDictionary<string, object> data, CallbackDelegate callback)
        {
            string gender = data.ContainsKey("gender")
                ? data["gender"].ToString()
                : "M";

            string modelName = gender == "F"
                ? "mp_f_freemode_01"
                : "mp_m_freemode_01";

            Model model = new Model(modelName);
            await model.Request(5000);

            if (!model.IsLoaded)
            {
                callback(new { status = "error" });
                return;
            }

            await Game.Player.ChangeModel(model);
            await Delay(500);

            int ped = Game.PlayerPed.Handle;

            API.SetPedDefaultComponentVariation(ped);

            API.SetEntityCoords(
                ped,
                402.8f,
                -996.2f,
                -99.0f,
                false,
                false,
                false,
                false
            );

            API.SetEntityHeading(ped, 180.0f);
            API.FreezeEntityPosition(ped, true);

            model.MarkAsNoLongerNeeded();

            callback(new { status = "ok" });
        }
    }
}