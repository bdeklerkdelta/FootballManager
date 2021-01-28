using FootballManager.Application.Players.Commands;
using FootballManager.Application.Players.Models;
using FootballManager.Application.Players.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballManager.Api.Controllers
{
    public class PlayerController : BaseController
    {

        public PlayerController(IMediator mediator) 
            : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("players")]
        public async Task<ActionResult<PlayerListViewModel>> Get()
        {
            return ResolveResult(await Mediator.Send(new GetPlayersQuery()));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("players/{id}")]
        public async Task<ActionResult<PlayerListViewModel>> GetById(long id)
        {
            return ResolveResult(await Mediator.Send(new GetPlayerByIdQuery()
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
        [Route("players")]
        public async Task<ActionResult<PlayerViewModel>> Add([FromBody] AddPlayerCommand addPlayerCommand)
        {
            return ResolveResult(await Mediator.Send(addPlayerCommand));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("players")]
        public async Task<ActionResult<PlayerViewModel>> Update([FromBody] UpdatePlayerCommand updatePlayerCommand)
        {
            return ResolveResult(await Mediator.Send(updatePlayerCommand));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("players")]
        public async Task<ActionResult<PlayerViewModel>> Delete([FromBody] DeletePlayerCommand deletePlayerCommand)
        {
            return ResolveResult(await Mediator.Send(deletePlayerCommand));
        }
    }
}
