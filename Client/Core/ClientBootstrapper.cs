using System;
using CitizenFX.Core;

namespace Client.Core
{
    public class ClientBootstrapper : BaseScript
    {
        public ClientBootstrapper()
        {
            Debug.WriteLine("[Lostgen] Initialisation du module Client...");

            // Enregistrement de nos gestionnaires/scripts clients
            RegisterScripts();

            // 🔥 C'est cette ligne qui prévient le serveur que le client est chargé !
            TriggerServerEvent("lostgen:server:playerReady");
        }

        private void RegisterScripts()
        {
            // C'est ici qu'on démarrera nos futurs scripts clients (ex: gestion du spawn, hud, menus...)
            // ex: new PlayerSpawnController();
        }
    }
}