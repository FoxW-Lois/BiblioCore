using Microsoft.AspNetCore.Mvc;
using BiblioCore.Data.Models;
using BiblioCore.Data.Repository;

namespace BiblioCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuteurController : ControllerBase
	{
		private readonly IAuteurRepository repository;

		public AuteurController(IAuteurRepository repository)
		{
			this.repository = repository;
		}

		//[HttpGet] // api/auteur
		//public async Task<IEnumerable<AuteurModel>> Get()
		//{
		//	return await repository.Get();
		//}

		[HttpGet("{id}")] // api/auteur/{id}
		public ActionResult<AuteurModel> Get(int id)
		{
			var model = repository.Get(id).Result;
			if (model == null)
				return NotFound();
			return model;
		}

		[HttpDelete("{id}")] // api/auteur/{id}
		public async Task<ActionResult> Delete(int id)
		{
			await repository.Delete(id);
			return Ok();
		}

		[HttpPost]
		public async Task<ActionResult<AuteurModel>> Create(AuteurModel model)
		{
			var auteur = await repository.Create(model);
			if (auteur == null)
				return StatusCode(500);
			return model;
		}

		[HttpPut("{id}")] // api/auteur/{id}
		public async Task<ActionResult<AuteurModel>> Update(int id, AuteurModel model)
		{
			var auteur = await repository.Update(id, model);
			if (model == null)
				return NotFound();
			return model;
		}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivreModel>>> GetLivresByAuteur([FromQuery] int auteurId)
        {
            var livres = await repository.ListByAuteurId(auteurId);
            if (!livres.Any())
                return NotFound();
            return Ok(livres);
        }
    }
}
