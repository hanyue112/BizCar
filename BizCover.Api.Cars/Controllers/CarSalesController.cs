using System;
using System.Threading.Tasks;
using BizCover.Api.Cars.Interfaces;
using BizCover.CarSales.Entities;
using BizCover.CarSales.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BizCover.CarSales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarSalesController : ControllerBase
    {
        private readonly ISalesPoliciesHub _salesPoliciesHub;
        private readonly IBizCarRepository _cachedRepo;

        public CarSalesController(ISalesPoliciesHub salesPoliciesHub, IBizCarRepository cachedRepo)
        {
            _salesPoliciesHub = salesPoliciesHub;
            _cachedRepo = cachedRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                return Ok(_salesPoliciesHub.ApplyAll(await _cachedRepo.GetAllCarsAsync()));
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ioe.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm]BizCoverCar value)
        {
            try
            {
                value.Id = 0;
                return Ok(await _cachedRepo.AddAsync(value));
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ioe.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm]BizCoverCar value)
        {
            try
            {
                value.Id = id;
                if (await _cachedRepo.UpdateAsync(value))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Update failed");
                }
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ioe.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
