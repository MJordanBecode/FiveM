using System.Threading.Tasks;
using CitizenFX.Core;
using FivemCsharpCore.Models;

namespace FivemCsharpCore.Managers
{
    public class PlayerManager
    {
        private readonly ExportDictionary _exports;

        public PlayerManager(ExportDictionary exports)
        {
            _exports = exports;
        }

        // Vérifie si un joueur existe en DB
        public async Task<bool> PlayerExists(string license)
        {
            var result = await _exports["oxmysql"].executeAsync(
                "SELECT COUNT(*) as count FROM players WHERE license = @license",
                new { license }
            );

            return result[0].count > 0;
        }

        // Récupère un joueur par sa license
        public async Task<Players> GetPlayerByLicense(string license)
        {
            var result = await _exports["oxmysql"].executeAsync(
                @"SELECT p.*, j.name as job_name, j.label as job_label, pj.grade
                  FROM players p
                  LEFT JOIN player_jobs pj ON p.id = pj.player_id
                  LEFT JOIN jobs j ON pj.job_id = j.id
                  WHERE p.license = @license",
                new { license }
            );

            var row = result?.Count > 0 ? result[0] : null;
            if (row == null) return null;

            return new Players
            {
                ID = (int)row.id,
                License = (string)row.license,
                FirstName = (string)row.firstname,
                LastName = (string)row.lastname,
                Money = (int)row.money,
                Job = new PlayerJob
                {
                    Name = (string)row.job_name,
                    Label = (string)row.job_label,
                    Grade = (string)row.grade
                }
            };
        }

        // Récupère un joueur par son Nom 

        public async Task<Players> GetPlayerByLastName(string lastName)
        {
            var result = await _exports["oxmysql"].executeAsync(
                @"SELECT p.*, j.name as job_name, j.label as job_label, pj.grade
                  FROM players p
                  LEFT JOIN player_jobs pj ON p.id = pj.player_id
                  LEFT JOIN jobs j ON pj.job_id = j.id
                  WHERE p.LastName = @lastName",
                new { lastName }
            );

            var row = result?.Count > 0 ? result[0] : null;
            if (row == null) return null;

            return new Players
            {
                ID = (int)row.id,
                License = (string)row.license,
                FirstName = (string)row.firstname,
                LastName = (string)row.lastname,
                Money = (int)row.money,
                Job = new PlayerJob
                {
                    Name = (string)row.job_name,
                    Label = (string)row.job_label,
                    Grade = (string)row.grade
                }
            };
        }

        // Crée un nouveau joueur en DB
        public async Task CreatePlayer(Players Player)
        {
            await _exports["oxmysql"].executeAsync(
                "INSERT INTO players (license, firstname, lastname, money) VALUES (@license, @firstname, @lastname, @money)",
                new
                {
                    license = Player.License,
                    firstname = Player.FirstName,
                    lastname = Player.LastName,
                    money = Player.Money
                }
            );
        }

        // Met à jour un joueur en DB
        public async Task SavePlayer(Players Player)
        {

            if (Player.FirstName != null)
                return;
            if (Player.LastName != null)
                return;
            if (Player.Job != null)
                return;
           //if (Player.Money != 0)
           //     return;


            await _exports["oxmysql"].executeAsync(
                "UPDATE players SET firstname = @firstname, lastname = @lastname, money = @money WHERE license = @license",
                new
                {
                    firstname = Player.FirstName,
                    lastname = Player.LastName,
                    //money = Player.Money,
                    license = Player.License
                }
            );
        }
    }
}