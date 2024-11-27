using Microsoft.AspNetCore.Mvc;
using BiblioCore.Data.Models;
using BiblioCore.Data.Repository;

namespace BiblioCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MembreController : ControllerBase
	{
		private readonly IMembreRepository repository;

		public MembreController(IMembreRepository repository)
		{
			this.repository = repository;
		}

		[HttpGet] // api/membre
		public async Task<IEnumerable<MembreModel>> Get()
		{
			return await repository.Get();
		}

		[HttpGet("{id}")] // api/membre/{id}
		public ActionResult<MembreModel> Get(int id)
		{
			var model = repository.Get(id).Result;
			if (model == null)
				return NotFound();
			return model;
		}

		//[HttpPut("{id}")] // api/membre/{id}
		//public async Task<ActionResult<LivreModel>> BorrowOneLivre(int id, LivreModel model, int MembreModelId)
		//{
		//	var membre = await repository.BorrowOneLivre(id, model, MembreModelId);
		//	if (model == null)
		//		return NotFound();
		//	return model;
		//}
	}
}
