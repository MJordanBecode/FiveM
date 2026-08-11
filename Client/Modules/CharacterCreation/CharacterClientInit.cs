using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Lostgen.Client.Modules.CharacterCreation
{
    public class CharacterCreationClient : BaseScript
    {
        private int _cam = -1;
        private Guid _currentPlayerId;


        public CharacterCreationClient()
        {       
            // 🟢 Désactive l'auto-spawn dès le chargement du script client
            TriggerEvent("spawnmanager:setAutoSpawn", false);

            EventHandlers["lostgen:client:startCharacterCreation"] += new Action<string>(StartCharacterCreation);

            EventHandlers["lostgen:client:spawnPlayerAfterCreation"] += new Action<float, float, float>(OnSpawnPlayerAfterCreation);


            API.RegisterNuiCallbackType("saveCharacter");
            EventHandlers["__cfx_nui:saveCharacter"] += new Action<IDictionary<string, object>, CallbackDelegate>(OnSaveCharacterNui);


            API.RegisterNuiCallbackType("changeGender");
            EventHandlers["__cfx_nui:changeGender"] += new Action<IDictionary<string, object>, CallbackDelegate>(OnChangeGenderNui);


            API.RegisterNuiCallbackType("updateHeadBlend");
            EventHandlers["__cfx_nui:updateHeadBlend"] += new Action<IDictionary<string, object>, CallbackDelegate>(OnUpdateHeadBlend);

            API.RegisterNuiCallbackType("updateHair");
            EventHandlers["__cfx_nui:updateHair"] +=
                new Action<IDictionary<string, object>, CallbackDelegate>(OnUpdateHair);


            API.RegisterNuiCallbackType("updateClothes");
            EventHandlers["__cfx_nui:updateClothes"] +=
                new Action<IDictionary<string, object>, CallbackDelegate>(OnUpdateClothes);

            EventHandlers["lostgen:client:loadCharacter"] += new Action<string>(OnLoadCharacter);
        }



        private void OnUpdateHeadBlend(
    IDictionary<string, object> data,
    CallbackDelegate cb)
        {
            int ped = Game.PlayerPed.Handle;


            int fatherShape = data.ContainsKey("shapeFirstID")
                ? Convert.ToInt32(data["shapeFirstID"])
                : 0;


            int motherShape = data.ContainsKey("shapeSecondID")
                ? Convert.ToInt32(data["shapeSecondID"])
                : 0;


            int fatherSkin = data.ContainsKey("fatherSkin")
                ? Convert.ToInt32(data["fatherSkin"])
                : fatherShape;


            int motherSkin = data.ContainsKey("motherSkin")
                ? Convert.ToInt32(data["motherSkin"])
                : motherShape;



            float shapeMix = data.ContainsKey("shapeMix")
                ? Convert.ToSingle(data["shapeMix"]) / 100f
                : 0.5f;


            float skinMix = data.ContainsKey("skinMix")
                ? Convert.ToSingle(data["skinMix"]) / 100f
                : 0.5f;



            API.SetPedHeadBlendData(
                ped,

                fatherShape,
                motherShape,
                0,

                fatherSkin,
                motherSkin,
                0,

                shapeMix,
                skinMix,

                0.0f,

                false
            );


            cb(new
            {
                status = "ok"
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


            Vector3 spawnPos = new Vector3(
                402.8f,
                -996.2f,
                -99.0f
            );


            Model model = new Model("mp_m_freemode_01");

            await model.Request(5000);


            if (!model.IsLoaded)
            {
                Debug.WriteLine("Impossible de charger le modèle.");
                return;
            }



            await Game.Player.ChangeModel(model);

            await Delay(1000);


            int ped = Game.PlayerPed.Handle;

            Debug.WriteLine($"PED AFTER MODEL CHANGE : {ped}");
            Debug.WriteLine($"MODEL HASH AFTER CHANGE : {Game.PlayerPed.Model.Hash}");


            // seulement si mort
            if (API.IsEntityDead(ped))
            {
                API.ResurrectPed(ped);
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

            Debug.WriteLine($"MODEL AFTER CHANGE : {Game.PlayerPed.Model.Hash}");

            ApplyDefaultCharacterCustomization(ped);


            API.SetEntityInvincible(ped, true);


            API.RequestCollisionAtCoord(
                spawnPos.X,
                spawnPos.Y,
                spawnPos.Z
            );


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


            API.SetEntityHeading(
                ped,
                180f
            );


            API.FreezeEntityPosition(
                ped,
                true
            );



            model.MarkAsNoLongerNeeded();



            // CAMERA

            _cam = API.CreateCamWithParams(
                "DEFAULT_SCRIPTED_CAMERA",
                402.8f,
                -997.8f,
                -98.3f,
                0f,
                0f,
                0f,
                50f,
                true,
                2
            );


            API.PointCamAtCoord(
                _cam,
                402.8f,
                -996.2f,
                -98.5f
            );


            API.RenderScriptCams(
                true,
                true,
                1000,
                true,
                false
            );



            API.SetNuiFocus(
                true,
                true
            );


            API.SendNuiMessage(
                "{\"action\":\"openCharacterCreation\"}"
            );


            Debug.WriteLine("Création du personnage ouverte.");
        }




        private void ApplyDefaultCharacterCustomization(int ped)
        {
            // Reset vêtements
            API.SetPedDefaultComponentVariation(ped);

            API.ClearPedBloodDamage(ped);
            API.ClearPedTasksImmediately(ped);

            // Visage de base
            //API.SetPedHeadBlendData(
            //    ped,
            //    0,
            //    0,
            //    0,
            //    0,
            //    0,
            //    0,
            //    0.5f,
            //    0.5f,
            //    0.0f,
            //    false
            //);

            API.SetPedHeadBlendData(
                ped,
                21, // père
                0,  // mère
                0,
                21,
                0,
                0,
                0.5f,
                0.5f,
                0.0f,
                false
            );



            // Cheveux
            API.SetPedComponentVariation(
                ped,
                2,
                0,
                0,
                0
            );


            API.SetPedHairColor(
                ped,
                0,
                0
            );



            // Haut
            API.SetPedComponentVariation(
                ped,
                11,
                15,
                0,
                0
            );


            // Pantalon
            API.SetPedComponentVariation(
                ped,
                4,
                21,
                0,
                0
            );


            // Chaussures
            API.SetPedComponentVariation(
                ped,
                6,
                34,
                0,
                0
            );



            API.SetEntityVisible(
                ped,
                true,
                false
            );


            API.SetEntityAlpha(
                ped,
                255,
                0
            );
        }





        private void OnSaveCharacterNui(
            IDictionary<string, object> data,
            CallbackDelegate callback)
        {
            try
            {
                API.SetNuiFocus(
                    false,
                    false
                );

                int ped = Game.PlayerPed.Handle;

                string firstName =
                    data.ContainsKey("firstName")
                    ? data["firstName"].ToString()
                    : "John";


                string lastName =
                    data.ContainsKey("lastName")
                    ? data["lastName"].ToString()
                    : "Doe";


                DateTime birthDay = DateTime.Now.AddYears(-20);

                if (data.ContainsKey("birthDay"))
                {
                    string value = data["birthDay"]?.ToString();

                    if (!string.IsNullOrEmpty(value))
                    {
                        DateTime.TryParse(
                            value,
                            out birthDay
                        );
                    }
                }



                char gender = 'M';

                if (data.ContainsKey("gender"))
                {
                    string value = data["gender"]?.ToString();

                    if (!string.IsNullOrEmpty(value))
                        gender = value[0];
                }



                short height =
                    data.ContainsKey("height")
                    ? Convert.ToInt16(data["height"])
                    : (short)180;


                // Récupération du skin envoyé par le NUI
                string faceJson = "{}";
                string hairJson = "{}";
                string clothesJson = "{}";

                if (data.ContainsKey("face") && data["face"] is IDictionary<string, object> faceDict)
                    faceJson = DictToJson(faceDict);

                if (data.ContainsKey("hair") && data["hair"] is IDictionary<string, object> hairDict)
                    hairJson = DictToJson(hairDict);

                if (data.ContainsKey("clothes") && data["clothes"] is IDictionary<string, object> clothesDict)
                    clothesJson = DictToJson(clothesDict);

                Debug.WriteLine("========== SKIN ENVOYE ==========");
                Debug.WriteLine("FACE JSON : " + faceJson);
                Debug.WriteLine("HAIR JSON : " + hairJson);
                Debug.WriteLine("CLOTHES JSON : " + clothesJson);
                Debug.WriteLine("=================================");

                TriggerServerEvent(
                    "lostgen:server:saveCharacter",
                    _currentPlayerId.ToString(),
                    firstName,
                    lastName,
                    birthDay.ToString("o"),
                    gender.ToString(),
                    height,
                    faceJson,
                    hairJson,
                    clothesJson
                );

                callback(new { status = "ok" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                callback(new { status = "error", message = ex.Message });
            }
        }

        //private static string DictToJson(IDictionary<string, object> dict)
        //{
        //    var parts = new List<string>();
        //    foreach (var kvp in dict)
        //    {
        //        string val = kvp.Value?.ToString() ?? "0";
        //        parts.Add($"\"{kvp.Key}\":{val}");
        //    }
        //    return "{" + string.Join(",", parts) + "}";
        //}

        private static string DictToJson(IDictionary<string, object> dict)
        {
            var parts = new List<string>();

            foreach (var kvp in dict)
            {
                string value;

                if (kvp.Value == null)
                {
                    value = "null";
                }
                else if (kvp.Value is IDictionary<string, object> child)
                {
                    value = DictToJson(child);
                }
                else if (kvp.Value is string str)
                {
                    value = $"\"{str}\"";
                }
                else if (kvp.Value is bool boolean)
                {
                    value = boolean.ToString().ToLower();
                }
                else
                {
                    value = kvp.Value.ToString();
                }

                parts.Add($"\"{kvp.Key}\":{value}");
            }

            return "{" + string.Join(",", parts) + "}";
        }



        private void OnSpawnPlayerAfterCreation(
            float x,
            float y,
            float z)
        {
            int ped = Game.PlayerPed.Handle;



            API.FreezeEntityPosition(
                ped,
                false
            );


            API.SetEntityInvincible(
                ped,
                false
            );



            API.SetEntityCoords(
                ped,
                x,
                y,
                z,
                false,
                false,
                false,
                false
            );



            API.SetEntityHeading(
                ped,
                0f
            );



            API.RenderScriptCams(
                false,
                true,
                1000,
                true,
                false
            );



            if (API.DoesCamExist(_cam))
            {
                API.DestroyCam(
                    _cam,
                    false
                );

                _cam = -1;
            }
        }





        private async void OnChangeGenderNui(
            IDictionary<string, object> data,
            CallbackDelegate callback)
        {
            string gender =
                data.ContainsKey("gender")
                ? data["gender"].ToString()
                : "M";


            string modelName =
                gender == "F"
                ? "mp_f_freemode_01"
                : "mp_m_freemode_01";



            Model model = new Model(modelName);


            await model.Request(5000);



            if (!model.IsLoaded)
            {
                callback(new
                {
                    status = "error"
                });

                return;
            }



            await Game.Player.ChangeModel(model);

            await Delay(1000);



            int ped = Game.PlayerPed.Handle;



            ApplyDefaultCharacterCustomization(ped);



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


            API.SetEntityHeading(
                ped,
                180f
            );


            API.FreezeEntityPosition(
                ped,
                true
            );


            model.MarkAsNoLongerNeeded();



            callback(new
            {
                status = "ok"
            });
        }

        private void OnUpdateHair(
    IDictionary<string, object> data,
    CallbackDelegate cb)
        {
            int ped = Game.PlayerPed.Handle;


            int style = Convert.ToInt32(data["style"]);
            int texture = Convert.ToInt32(data["texture"]);
            int color = Convert.ToInt32(data["color"]);
            int highlightColor = Convert.ToInt32(data["highlightColor"]);


            API.SetPedComponentVariation(
                ped,
                2,
                style,
                texture,
                0
            );

            API.SetPedHairColor(
                ped,
                color,
                highlightColor
            );


            cb(new
            {
                status = "ok"
            });
        }

        private void OnUpdateClothes(
    IDictionary<string, object> data,
    CallbackDelegate cb)
        {
            int ped = Game.PlayerPed.Handle;


            // Haut
            API.SetPedComponentVariation(
                ped,
                11,
                Convert.ToInt32(data["torso"]),
                Convert.ToInt32(data["torsoTexture"]),
                0
            );


            // Pantalon
            API.SetPedComponentVariation(
                ped,
                4,
                Convert.ToInt32(data["pants"]),
                Convert.ToInt32(data["pantsTexture"]),
                0
            );


            // Chaussures
            API.SetPedComponentVariation(
                ped,
                6,
                Convert.ToInt32(data["shoes"]),
                Convert.ToInt32(data["shoesTexture"]),
                0
            );


            cb(new
            {
                status = "ok"
            });
        }

        private async void OnLoadCharacter(string payload)
        {
            Debug.WriteLine("[CharacterCreation] >>> Event loadCharacter reçu !");

            TriggerEvent("spawnmanager:setAutoSpawn", false);

            string[] parts = payload.Split('|');

            bool isFemale = parts[0] == "1";
            int fatherShape = int.Parse(parts[1]);
            int motherShape = int.Parse(parts[2]);
            float shapeMix = float.Parse(parts[3], CultureInfo.InvariantCulture);
            int fatherSkin = int.Parse(parts[4]);
            int motherSkin = int.Parse(parts[5]);
            float skinMix = float.Parse(parts[6], CultureInfo.InvariantCulture);
            int hairStyle = int.Parse(parts[7]);
            int hairTexture = int.Parse(parts[8]);
            int hairColor = int.Parse(parts[9]);
            int hairHighlight = int.Parse(parts[10]);
            int torsoDrawable = int.Parse(parts[11]);
            int torsoTexture = int.Parse(parts[12]);
            int pantsDrawable = int.Parse(parts[13]);
            int pantsTexture = int.Parse(parts[14]);
            int shoesDrawable = int.Parse(parts[15]);
            int shoesTexture = int.Parse(parts[16]);

            // 🟢 Position
            float posX = float.Parse(parts[17], CultureInfo.InvariantCulture);
            float posY = float.Parse(parts[18], CultureInfo.InvariantCulture);
            float posZ = float.Parse(parts[19], CultureInfo.InvariantCulture);
            float heading = float.Parse(parts[20], CultureInfo.InvariantCulture);

            // 1) Changer le modèle vers le bon freemode AVANT d'appliquer le skin
            string modelName = isFemale ? "mp_f_freemode_01" : "mp_m_freemode_01";
            Model model = new Model(modelName);

            await model.Request(5000);

            if (!model.IsLoaded)
            {
                Debug.WriteLine("[CharacterCreation] Impossible de charger le modèle freemode.");
                return;
            }

            await Game.Player.ChangeModel(model);
            await Delay(200);

            model.MarkAsNoLongerNeeded();

            int ped = Game.PlayerPed.Handle;
            Debug.WriteLine($"[CharacterCreation] Application du skin sur ped {ped}...");

            // 2) Appliquer le skin
            API.SetPedHeadBlendData(
                ped,
                fatherShape, motherShape, 0,
                fatherSkin, motherSkin, 0,
                shapeMix / 100f, skinMix / 100f, 0.0f,
                false
            );

            API.SetPedComponentVariation(ped, 2, hairStyle, hairTexture, 0);
            API.SetPedHairColor(ped, hairColor, hairHighlight);

            API.SetPedComponentVariation(ped, 11, torsoDrawable, torsoTexture, 0);
            API.SetPedComponentVariation(ped, 4, pantsDrawable, pantsTexture, 0);
            API.SetPedComponentVariation(ped, 6, shoesDrawable, shoesTexture, 0);

            API.SetEntityVisible(ped, true, false);
            API.SetEntityAlpha(ped, 255, 0);

            // 3) Téléporter à la position sauvegardée
            API.RequestCollisionAtCoord(posX, posY, posZ);

            int attempts = 0;
            while (!API.HasCollisionLoadedAroundEntity(ped) && attempts < 500)
            {
                attempts++;
                await Delay(0);
            }

            API.SetEntityCoords(ped, posX, posY, posZ, false, false, false, false);
            API.SetEntityHeading(ped, heading);

            Debug.WriteLine($"[CharacterCreation] Skin appliqué et position restaurée : ({posX}, {posY}, {posZ}).");

            await Delay(1500);
            ped = Game.PlayerPed.Handle; // re-fetch au cas où le ped ait changé
            API.SetEntityCoords(ped, posX, posY, posZ, false, false, false, false);
            API.SetEntityHeading(ped, heading);
            Debug.WriteLine($"[CharacterCreation] Position réappliquée après délai de sécurité : ({posX}, {posY}, {posZ}).");
        }
    }
}