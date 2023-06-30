using CarTrab.Entities;
using CarTrab.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CarTrab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarcaController : Controller
    {
        public readonly MarcaService _marcaService;

        public MarcaController(MarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Marca marca)
        {
            try
            {
                var result = await _marcaService.Create(marca);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var marca = await _marcaService.GetById(id);
                return Ok(marca);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var marcaList = await _marcaService.GetAll();
                return Ok(marcaList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }


        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateCategory([FromBody] Marca marca)
        {
            try
            {
                var result = await _marcaService.Update(marca);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var marca = await _marcaService.Delete(id);
                return Ok(marca);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }

    }
}

