using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoTypeController : ControllerBase
    {
        IMediator _mediator;

        public InfoTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] GetInfoTypeDto request)
        {
            HandlerResponse<List<GetByIdInfoTypeResponseDto>> response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdInfoTypeDto request)
        {
            HandlerResponse<GetByIdInfoTypeResponseDto> response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Add([FromQuery] CreateInfoTypeDto request)
        {
            IActionResult result;
            HandlerResponse<InfoType> response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                result = Ok(response);
            }
            else
            {
                result = BadRequest(response);
            }

            return result;
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromQuery] UpdateInfoTypeDto request)
        {
            IActionResult result;
            HandlerResponse<InfoType> response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                result = Ok(response);
            }
            else
            {
                result = BadRequest(response);
            }

            return result;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteInfoTypeDto request)
        {
            IActionResult result;
            HandlerResponse<InfoType> response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                result = Ok(response);
            }
            else
            {
                result = BadRequest(response);
            }

            return result;
        }
    }

}