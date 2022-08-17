using Microsoft.AspNetCore.Mvc;
using Report.Application.Dto;
using Report.Application.Interfaces.Repositories;
using Report.Domain.Entities;

namespace Report.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportLogController : ControllerBase
    {
        public IReportLogRepository _repo { get; set; }
        public ReportLogController(IReportLogRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            IActionResult result = null;

            List<ReportLog> data = _repo.GetAsync().Result.ToList();

            if (data.Any())
            {
                List<ReportLogDto> resultList = new List<ReportLogDto>();

                foreach (ReportLog log in data)
                {
                    resultList.Add(new ReportLogDto
                    {
                        Id = log.UUID,
                        CreateTime = log.RequestDate,
                        Status = log.StatusID
                    });
                }

                result = Ok(resultList);
            }
            else
                result = BadRequest("Data bulunamadı.");

            return result;
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            IActionResult result = null;

            ReportLogDto resultDto = null;

            ReportLog log = _repo.GetByIdAsync(id).Result;

            if (log != null)
            {
                resultDto = new ReportLogDto
                {
                    Id = log.UUID,
                    CreateTime = log.RequestDate,
                    Status = log.StatusID
                };

                result = Ok(resultDto);
            }
            else
                result = BadRequest("veri bulunamadı.");

            return result;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create()
        {
            IActionResult result = null;

            ReportLog response = await _repo.Add(new ReportLog()
            {
                StatusID = 1,
                RequestDate = DateTime.Now
            });

            result = Ok(new ReportLogDto()
            {
                Id = response.UUID,
                CreateTime = response.RequestDate,
                Status = response.StatusID
            });

            return result;
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromQuery] ReportLogDto request)
        {
            IActionResult result = null;
            ReportLog response = null;

            if (request != null && _repo.GetByIdAsync(request.Id).Result != null)
            {
                response = await _repo.Update(new ReportLog()
                {
                    UUID = request.Id,
                    StatusID = request.Status,
                    RequestDate = DateTime.Now
                });
            }
            if (response != null)
            {
                result = Ok(new ReportLogDto()
                {
                    Id = response.UUID,
                    CreateTime = response.RequestDate,
                    Status = response.StatusID
                });
            }
            else
            {
                result = BadRequest();
            }

            return result;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            IActionResult result = null;
            ReportLog response = null;
            ReportLog deletedData = _repo.GetByIdAsync(id).Result;

            if (deletedData != null)
            {
                response = await _repo.Delete(deletedData);
            }

            if (response != null)
            {
                result = Ok(new ReportLogDto()
                {
                    Id = response.UUID,
                    CreateTime = response.RequestDate,
                    Status = response.StatusID
                });
            }
            else
            {
                result = deletedData == null ? BadRequest("Silinecek veri bulunamadı.") : BadRequest("Veri silinemedi.");
            }

            return result;
        }
    }
}