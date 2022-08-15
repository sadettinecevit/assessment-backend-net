using Microsoft.AspNetCore.Mvc;
using Report.Application.Dto;
using Report.Application.Interfaces.Repositories;
using Report.Domain.Entities;

namespace Report.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportLogStatusController : ControllerBase
    {
        public IReportLogStatusRepository _repo { get; set; }
        public ReportLogStatusController(IReportLogStatusRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            IActionResult result = null;

            List<ReportLogStatus> data = _repo.GetAsync().Result.ToList();

            if (data.Any())
            {
                List<ReportLogStatusDto> resultList = new List<ReportLogStatusDto>();

                foreach (ReportLogStatus log in data)
                {
                    resultList.Add(new ReportLogStatusDto
                    {
                        Id = log.UUID,
                        Statu = log.Status
                    });
                }

                result = Ok(resultList);
            }

            return result;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            IActionResult result = null;

            ReportLogStatusDto resultDto = null;

            ReportLogStatus log = _repo.GetByIdAsync(id).Result;

            if (log != null)
            {
                resultDto = new ReportLogStatusDto
                {
                    Id = log.UUID,
                    Statu = log.Status
                };

                result = Ok(resultDto);
            }
            else
                result = BadRequest("veri bulunamadı.");

            return result;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(string request)
        {
            IActionResult result = null;

            ReportLogStatus response = await _repo.Add(new ReportLogStatus()
            {
                Status = request
            });

            if (response != null)
                result = Ok(new ReportLogStatusDto()
                {
                    Statu = request
                });
            else
                result = BadRequest("veri eklenemedi.");

            return result;
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ReportLogStatusDto request)
        {
            IActionResult result = null;
            ReportLogStatus response = null;

            if (request != null && _repo.GetByIdAsync(request.Id).Result != null)
            {
                response = await _repo.Update(new ReportLogStatus()
                {
                    UUID = request.Id,
                    Status = request.Statu
                });
            }
            if (response != null)
            {
                result = Ok(new ReportLogStatusDto()
                {
                    Id = response.UUID,
                    Statu = request.Statu
                });
            }
            else
            {
                result = BadRequest("güncelleme başarısız.");
            }

            return result;
        }

        [HttpDelete("update/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            IActionResult result = null;
            ReportLogStatus response = null;
            ReportLogStatus deletedData = _repo.GetByIdAsync(id).Result;

            if (deletedData != null)
            {
                response = await _repo.Delete(deletedData);
            }

            if (response != null)
            {
                result = Ok(new ReportLogStatusDto()
                {
                    Id = response.UUID,
                    Statu = response.Status
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