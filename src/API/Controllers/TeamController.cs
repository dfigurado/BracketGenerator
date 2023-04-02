using API.Controllers.Base;
using Application.Dto;
using Application.Interfaces.Features.Team;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;


namespace API.Controllers
{
    public class TeamController : BaseController
    {
        private readonly ITournamentTeamsQuery _tournamentTeamsQuery;
        private readonly ICreateTeamCommand _createTeamCommand;

        public TeamController(ITournamentTeamsQuery tournamentTeamsQuery, ICreateTeamCommand createTeamCommand)
        {
            _tournamentTeamsQuery = tournamentTeamsQuery;
            _createTeamCommand = createTeamCommand;
        }

        /// <summary>
        /// Get all teams for a tournament
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _tournamentTeamsQuery.TournamentId = id;
            var teams = await Mediator.Send(_tournamentTeamsQuery);
            return Ok(teams);
        }

        /// <summary>
        /// Add teams for a tournament
        /// </summary>
        [HttpPost("EnrollTeams")]
        public async Task<IActionResult> Upload(int tournamentId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");


            var uploadsFolder = Path.Combine("Uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var json = System.IO.File.ReadAllText(filePath);

            var teams = JsonConvert.DeserializeObject<Dictionary<string, List<TeamDto>>>(json)["R16"];

            _createTeamCommand.TournamentId = tournamentId;
            _createTeamCommand.Teams = teams;

            // Delete uploaded file.

            return Ok(await Mediator.Send(_createTeamCommand));
        }
    }
}