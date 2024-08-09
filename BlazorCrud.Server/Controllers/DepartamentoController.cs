using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController(CrudBlazorContext context) : ControllerBase
    {
        private readonly CrudBlazorContext _context = context;

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseApi<List<DepartamentoDTO>>();
            var listaDepartamentoDTO = new List<DepartamentoDTO>();

            try
            {
                foreach (var item in await _context.Departamentos.ToListAsync())
                {
                    listaDepartamentoDTO.Add(new DepartamentoDTO
                    {
                        IdDepartamento = item.IdDepartamento,
                        Nombre = item.Nombre
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaDepartamentoDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }
    }
}
