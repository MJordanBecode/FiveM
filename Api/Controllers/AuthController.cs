using Microsoft.AspNetCore.Mvc;
using Shared.DTOS;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/auth")] // Définit la route de base : http://localhost:5000/api/auth
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
            // Plus tard, on injectera ici ton projet "Data" (DbContext de ta base de données)
        }

        [HttpPost("connect")] // Route finale : POST http://localhost:5000/api/auth/connect
        public ActionResult<ConnectionResponseDto> HandleConnection([FromBody] ConnectionRequestDto request)
        {
            // Étape de sécurité : Vérifier si la requête est valide
            if (request == null || string.IsNullOrEmpty(request.License))
            {
                return BadRequest(new ConnectionResponseDto
                {
                    IsAllowed = false,
                    RejectReason = "Requête invalide ou licence manquante."
                });
            }

            // --- DEBUT DE LA LOGIQUE (SIMULÉE POUR L'INSTANT) ---
            // C'est ici qu'on interrogera ton projet "Data" plus tard.

            // Exemple de simulation de Ban :
            if (request.PlayerName.ToLower().Contains("troll"))
            {
                return Ok(new ConnectionResponseDto
                {
                    IsAllowed = false,
                    RejectReason = "Vous êtes banni de ce serveur pour comportement inapproprié."
                });
            }

            // Exemple de réponse positive :
            var response = new ConnectionResponseDto
            {
                IsAllowed = true,
                RejectReason = string.Empty,
                UserId = 42,           // ID fictif de base de données
                Role = "Player"        // Rôle par défaut
            };
            // --- FIN DE LA LOGIQUE ---

            // On renvoie un code HTTP 200 (OK) avec notre réponse
            return Ok(response);
        }
    }
}
