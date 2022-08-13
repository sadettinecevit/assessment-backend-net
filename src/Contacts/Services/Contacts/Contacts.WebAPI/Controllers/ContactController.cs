using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] GetContactDto request)
        {
            HandlerResponse<List<GetByIdContactResponseDto>> response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdContactDto request)
        {
            HandlerResponse<GetByIdContactResponseDto> response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Add(CreateContactDto request)
        {
            IActionResult result;
            HandlerResponse<Contact> response = await _mediator.Send(request);

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
        public async Task<IActionResult> Update(UpdateContactDto request)
        {
            IActionResult result;
            HandlerResponse<Contact> response = await _mediator.Send(request);

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
        public async Task<IActionResult> Delete(DeleteContactDto request)
        {
            IActionResult result;
            HandlerResponse<Contact> response = await _mediator.Send(request);

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