using Microsoft.AspNetCore.Mvc;
using BiblioCore.Data.Models;
using BiblioCore.Data.Repository;

namespace BiblioCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RayonController : ControllerBase
	{
		private readonly IRayonRepository repository;

		public RayonController(IRayonRepository repository)
		{
			this.repository = repository;
		}

		//[HttpGet] // api/rayon
		//public async Task<IEnumerable<RayonModel>> Get()
		//{
		//	return await repository.Get();
		//}

		//[HttpGet("{id}")] // api/rayon/{id}
		//public ActionResult<RayonModel> Get(int id)
		//{
		//	var model = repository.Get(id).Result;
		//	if (model == null)
		//		return NotFound();
		//	return model;
		//}

		[HttpDelete("{id}")] // api/rayon/{id}
		public async Task<ActionResult> Delete(int id)
		{
			await repository.Delete(id);
			return Ok();
		}

		[HttpPost]
		public async Task<ActionResult<RayonModel>> Create(RayonModel model)
		{
			var rayon = await repository.Create(model);
			if (rayon == null)
				return StatusCode(500);
			return model;
		}

		[HttpPut("{id}")] // api/rayon/{id}
		public async Task<ActionResult<RayonModel>> Update(int id, RayonModel model)
		{
			var rayon = await repository.Update(id, model);
			if (model == null)
				return NotFound();
			return model;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<LivreModel>>> GetLivresByRayon([FromQuery] int rayonId)
		{
			var livres = await repository.ListByRayonId(rayonId);
			if (!livres.Any())
				return NotFound();
			return Ok(livres);
		}

		[HttpGet("{titre}")] // api/rayon/{titre}
		public ActionResult<LivreModel> SearchByTitre(string titre) //[FromQuery] string titre
		{
			var model = repository.SearchByTitre(titre).Result;
			if (model == null)
				return NotFound();
			return model;
		}
	}
}
