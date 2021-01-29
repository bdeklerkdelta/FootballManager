using FootballManager.Application.Teams.Commands;
using FootballManager.Application.Teams.Models;
using FootballManager.Application.Teams.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballManager.Api.Controllers
{
    public class TeamController : BaseController
    {

        public TeamController(IMediator mediator) 
            : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("teams")]
        public async Task<ActionResult<TeamListViewModel>> Get()
        {
            return ResolveResult(await Mediator.Send(new GetTeamsQuery()));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("teams/{id}")]
        public async Task<ActionResult<TeamViewModel>> GetById(long id)
        {
            return ResolveResult(await Mediator.Send(new GetTeamByIdQuery()
            {
                Id = id
            }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("teams")]
        public async Task<ActionResult<TeamViewModel>> Add([FromBody] AddTeamCommand addTeamCommand)
        {
            return ResolveResult(await Mediator.Send(addTeamCommand));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("teams")]
        public async Task<ActionResult<TeamViewModel>> Update([FromBody] UpdateTeamCommand updateTeamCommand)
        {
            return ResolveResult(await Mediator.Send(updateTeamCommand));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("teams/updatePlayers")]
        public async Task<ActionResult<TeamViewModel>> UpdateTeamPlayers([FromBody] UpdateTeamPlayersCommand updateTeamPlayersCommand)
        {
            return ResolveResult(await Mediator.Send(updateTeamPlayersCommand));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("teams")]
        public async Task<ActionResult<TeamDeleteViewModel>> Delete([FromBody] DeleteTeamCommand deleteTeamCommand)
        {
            return ResolveResult(await Mediator.Send(deleteTeamCommand));
        }
    }
}
