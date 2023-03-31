
using API.Controllers.Base;
using Application.Dto;
using Application.Interfaces.Features.Tournament;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;

using System.Diagnostics;
using System.Linq;

using System.Threading.Tasks;

namespace API.Controllers
{
    public class TournamentController : BaseController
    {
        private readonly ICreateTournamentCommand _createTournamentCommand;

        public TournamentController(ICreateTournamentCommand createTournamentCommand)
        {
            _createTournamentCommand = createTournamentCommand;
        }

        [HttpPost("NewEvent")]
        public async Task<IActionResult> Create(TournamentDto createTournamentDto)
        {
            _createTournamentCommand.Tournament = createTournamentDto;
            return Ok(await Mediator.Send(_createTournamentCommand));
        }
    }
}