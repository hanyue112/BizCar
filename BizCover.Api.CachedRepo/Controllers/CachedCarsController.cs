using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BizCover.Api.CachedRepo.Entities;
using BizCover.Api.CachedRepo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BizCover.Api.CachedRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachedCarsController : ControllerBase
    {
        private readonly ICachedRepo _cachedRepo;

        public CachedCarsController(ICachedRepo cachedRepo)
        {
            _cachedRepo = cachedRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CachedCar>>> GetAsync()
        {
            try
            {
                return await _cachedRepo.GetAllCars();
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
        public async Task<ActionResult> Post([FromForm]CachedCar value)
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
        public async Task<ActionResult> Put(int id, [FromForm]CachedCar value)
        {
            try
            {
                value.Id = id;
                await _cachedRepo.UpdateAsync(value);
                return Ok();
            }
            catch (InvalidOperationException ioe)
            {
                return StatusCode(404, ioe.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}