using Contacts.Application.Dto;
using Contacts.Application.Dto.Command;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        IMediator _mediator;

        public ContactInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] GetContactInfoDto request)
        {
            HandlerResponse<List<GetByIdContactInfoResponseDto>> response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdContactInfoDto request)
        {
            HandlerResponse<GetByIdContactInfoResponseDto> response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Add(CreateContactInfoDto request)
        {
            IActionResult result;
            HandlerResponse<ContactInfo> response = await _mediator.Send(request);

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
        public async Task<IActionResult> Update(UpdateContactInfoDto request)
        {
            IActionResult result;
            HandlerResponse<ContactInfo> response = await _mediator.Send(request);

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
        public async Task<IActionResult> Delete(DeleteContactInfoDto request)
        {
            IActionResult result;
            HandlerResponse<ContactInfo> response = await _mediator.Send(request);

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