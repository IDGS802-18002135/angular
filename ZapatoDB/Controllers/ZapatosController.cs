using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZapatoDB.Models;

namespace ZapatoDB.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ZapatosController : ControllerBase
	{
		//Creamos nuestra variable de contexto de BD
		private readonly ZapateriaContext _baseDatos;
		public ZapatosController(ZapateriaContext baseDatos)
		{
			_baseDatos = baseDatos;
		}
		//Método GET ListaTareas que devuelve la lista de todas las tareas en la BD.
		[HttpGet]
		[Route("ListaZapatos")]
		public async Task<IActionResult> Lista()
		{
			var listaZapatos = await _baseDatos.Zapatos.ToListAsync();
			return Ok(listaZapatos);
		}
		[HttpGet]
		[Route("BuscarZapatos")]
		public async Task<IActionResult> Buscar(
			[FromQuery] string palabraClave)
		{
			IQueryable<Zapato> query = _baseDatos.Zapatos
				.Where(z => z.Nombre.Contains(palabraClave) ||
							z.Descripcion.Contains(palabraClave) ||
							z.Precio.ToString().Contains(palabraClave));

			var resultados = await query.ToListAsync();

			return Ok(resultados);
		}
		//Método POST AgregarTarea que permite agregar una nueva tarea a la BD.
		[HttpPost]
		[Route("AgregarZapatos")]
		public async Task<IActionResult> Agregar([FromBody] Zapato request)
		{
			await _baseDatos.Zapatos.AddAsync(request);
			await _baseDatos.SaveChangesAsync();
			return Ok(request);
		}

		//Método PUT ModificarTarea que permite modificar una tarea de la BD.
		[HttpPut]
		[Route("ModificarZapato/{id:int}")]
		public async Task<IActionResult> Modificar(int id, [FromBody] Zapato request)
		{
			var zapatoModificar = await _baseDatos.Zapatos.FindAsync(id);
			if (zapatoModificar == null)
			{
				return BadRequest("No existe el zapato");
			}
			zapatoModificar.Nombre = request.Nombre;
			zapatoModificar.Imagen = request.Imagen;
			zapatoModificar.Descripcion = request.Descripcion;
			zapatoModificar.Precio = request.Precio;
			try
			{
				await _baseDatos.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return NotFound();
			}

			return Ok();
		}
		//Método DELETE EliminarTarea que permite eliminar una tarea de la BD.
		[HttpDelete]
		[Route("EliminarZapato/{id:int}")]
		public async Task<IActionResult> Eliminar(int id)
		{
			var zapatoEliminar = await _baseDatos.Zapatos.FindAsync(id);
			if (zapatoEliminar == null)
			{
				return BadRequest("No existe la zapato");
			}
			_baseDatos.Zapatos.Remove(zapatoEliminar);
			await _baseDatos.SaveChangesAsync();
			return Ok();
		}

	}
}
