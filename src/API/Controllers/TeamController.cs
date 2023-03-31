using API.Controllers.Base;
using Application.Dto;
using Application.Interfaces.Features.Team;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

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

            var filePath = Path.GetRandomFileName();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var json = System.IO.File.ReadAllText(filePath);

            _createTeamCommand.TournamentId = tournamentId;
            _createTeamCommand.Seed = JsonConvert.DeserializeObject<List<TeamDto>>(json);

            return Ok(await Mediator.Send(_createTeamCommand));
        }
    }
}