using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Lostgen.Client.Modules.CharacterCreation
{
    public class CharacterCreationClient : BaseScript
    {
        private int _cam = -1;
        private Guid _currentPlayerId;

        public CharacterCreationClient()
        {
            // 1. Événement de lancement par le serveur
            EventHandlers["lostgen:client:startCharacterCreation"] += new Action<string>(StartCharacterCreation);

            // 2. Événement de fin/spawn reçu depuis le serveur
            EventHandlers["lostgen:client:spawnPlayerAfterCreation"] += new Action<float, float, float>(OnSpawnPlayerAfterCreation);

            // 3. Callback NUI
            API.RegisterNuiCallbackType("saveCharacter");
            EventHandlers["__cfx_nui:saveCharacter"] += new Action<IDictionary<string, object>, CallbackDelegate>(OnSaveCharacterNui);

            API.RegisterNuiCallbackType("changeGender");
            EventHandlers["__cfx_nui:changeGender"] += new Action<IDictionary<string, object>, CallbackDelegate>(OnChangeGenderNui);

            API.RegisterNuiCallbackType("updateHeadBlend");
            EventHandlers["__cfx_nui:updateHeadBlend"] += new Action<IDictionary<string, object>, CallbackDelegate>((data, cb) =>
            {
                int ped = Game.PlayerPed.Handle;

                int shapeFirstID = Convert.ToInt32(data["shapeFirstID"]);   // ID Mère (0 à 45)
                int shapeSecondID = Convert.ToInt32(data["shapeSecondID"]); // ID Père (0 à 45)

                // Convertir de 0-100% (JS) vers 0.0-1.0 (FiveM Float)
                float shapeMix = Convert.ToSingle(data["shapeMix"]) / 100f; // Ressemblance Visage
                float skinMix = Convert.ToSingle(data["skinMix"]) / 100f;   // Ressemblance Peau

                // Application de la génétique GTA V
                API.SetPedHeadBlendData(
                    ped,
                    shapeFirstID, shapeSecondID, 0,
                    shapeFirstID, shapeSecondID, 0,
                    shapeMix, skinMix, 0.0f, false
                );

                cb(new { status = "ok" });
            });
        }

        private async void StartCharacterCreation(string playerIdStr)
        {
            if (!Guid.TryParse(playerIdStr, out _currentPlayerId)) return;

            // 🟢 STEP 1 : Changer le modèle du joueur EN PREMIER
            uint modelHash = (uint)API.GetHashKey("mp_m_freemode_01");
            await Game.Player.ChangeModel(new Model((PedHash)modelHash));

            // On récupère le nouveau Ped généré
            int ped = Game.PlayerPed.Handle;

            // 🟢 STEP 2 : Positionnement et gel du personnage
            Vector3 spawnPos = new Vector3(402.8f, -996.2f, -99.0f);
            API.SetEntityCoords(ped, spawnPos.X, spawnPos.Y, spawnPos.Z, false, false, false, false);
            API.SetEntityHeading(ped, 180.0f);
            API.FreezeEntityPosition(ped, true);

            // 🟢 STEP 3 : Caméra cinématique
            _cam = API.CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", 402.8f, -997.8f, -98.3f, 0.0f, 0.0f, 0.0f, 50.0f, true, 2);
            API.PointCamAtCoord(_cam, 402.8f, -996.2f, -98.5f);
            API.RenderScriptCams(true, true, 1000, true, false);

            // 🟢 STEP 4 : Interface NUI
            API.SetNuiFocus(true, true);
            API.SendNuiMessage("{\"action\": \"openCharacterCreation\"}");
        }

        private void OnSaveCharacterNui(IDictionary<string, object> data, CallbackDelegate callback)
        {
            try
            {
                // Masquer le curseur et l'UI
                API.SetNuiFocus(false, false);

                // Extraction des données du formulaire
                string firstName = data.ContainsKey("firstName") ? data["firstName"].ToString() : "John";
                string lastName = data.ContainsKey("lastName") ? data["lastName"].ToString() : "Doe";
                DateTime birthDay = data.ContainsKey("birthDay") ? DateTime.Parse(data["birthDay"].ToString()) : DateTime.Now.AddYears(-20);
                char gender = data.ContainsKey("gender") ? data["gender"].ToString()[0] : 'M';
                short height = data.ContainsKey("height") ? Convert.ToInt16(data["height"]) : (short)180;

                // Envoi des données au CharacterController (Serveur)
                TriggerServerEvent("lostgen:server:saveCharacter",
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
                Debug.WriteLine($"[ERROR] Échec du traitement OnSaveCharacterNui : {ex.Message}");
                callback(new { status = "error", message = ex.Message });
            }
        }

        private void OnSpawnPlayerAfterCreation(float x, float y, float z)
        {
            int ped = Game.PlayerPed.Handle;

            // Débloquer le joueur et le téléporter au point de spawn final
            API.FreezeEntityPosition(ped, false);
            API.SetEntityCoords(ped, x, y, z, false, false, false, false);
            API.SetEntityHeading(ped, 0.0f);

            // 🟢 Détruire la caméra de création et remettre la caméra de jeu
            API.RenderScriptCams(false, true, 1000, true, false);
            if (API.DoesCamExist(_cam))
            {
                API.DestroyCam(_cam, false);
                _cam = -1;
            }
        }

        private async void OnChangeGenderNui(IDictionary<string, object> data, CallbackDelegate callback)
        {
            string gender = data.ContainsKey("gender") ? data["gender"].ToString() : "M";
            string modelName = (gender == "F") ? "mp_f_freemode_01" : "mp_m_freemode_01";

            uint modelHash = (uint)API.GetHashKey(modelName);
            await Game.Player.ChangeModel(new Model((PedHash)modelHash));

            int ped = Game.PlayerPed.Handle;
            Vector3 spawnPos = new Vector3(402.8f, -996.2f, -99.0f);
            API.SetEntityCoords(ped, spawnPos.X, spawnPos.Y, spawnPos.Z, false, false, false, false);
            API.SetEntityHeading(ped, 180.0f);
            API.FreezeEntityPosition(ped, true);

            callback(new { status = "ok" });
        }
    }
}