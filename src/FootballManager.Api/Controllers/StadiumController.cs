using FootballManager.Application.Stadiums.Commands;
using FootballManager.Application.Stadiums.Models;
using FootballManager.Application.Stadiums.Queries;
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
    public class StadiumController : BaseController
    {

        public StadiumController(IMediator mediator) 
            : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Stadiums")]
        public async Task<ActionResult<StadiumListViewModel>> Get()
        {
            return ResolveResult(await Mediator.Send(new GetStadiumsQuery()));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Stadiums/{id}")]
        public async Task<ActionResult<StadiumViewModel>> GetById(long id)
        {
            return ResolveResult(await Mediator.Send(new GetStadiumByIdQuery()
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
        [Route("Stadiums")]
        public async Task<ActionResult<StadiumViewModel>> Add([FromBody] AddStadiumCommand addStadiumCommand)
        {
            return ResolveResult(await Mediator.Send(addStadiumCommand));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Stadiums")]
        public async Task<ActionResult<StadiumViewModel>> Update([FromBody] UpdateStadiumCommand updateStadiumCommand)
        {
            return ResolveResult(await Mediator.Send(updateStadiumCommand));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Stadiums")]
        public async Task<ActionResult<StadiumDeleteViewModel>> Delete([FromBody] DeleteStadiumCommand deleteStadiumCommand)
        {
            return ResolveResult(await Mediator.Send(deleteStadiumCommand));
        }
    }
}
