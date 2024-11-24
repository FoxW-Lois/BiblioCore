﻿using Microsoft.AspNetCore.Mvc;
using BiblioCore.Data.Models;
using BiblioCore.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace BiblioCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LivreController : ControllerBase
	{
		private readonly ILivreRepository repository;

		public LivreController(ILivreRepository repository) 
		{
			this.repository = repository;
		}

		[HttpGet] // api/livre
		public async Task<IEnumerable<LivreModel>> Get()
		{
			return await repository.Get();
		}

		[HttpGet("{id}")] // api/livre/{id}
		public ActionResult<LivreModel> Get(int id)
		{
			var model = repository.Get(id).Result;
			if (model == null)
				return NotFound();
			return model;
		}

		[HttpDelete("{id}")] // api/livre/{id}
		public async Task<ActionResult> Delete(int id)
		{
			await repository.Delete(id);
			return Ok();
		}

		[HttpPost]
		public async Task<ActionResult<LivreModel>> Create(LivreModel model)
		{
			var livre = await repository.Create(model);
			if (livre == null)
				return StatusCode(500);
			return model;
		}

		[HttpPut("{id}")] // api/livre/{id}
		public async Task<ActionResult<LivreModel>> Update(int id, LivreModel model)
		{
			var livre = await repository.Update(id, model);
			if (model == null)
				return NotFound();
			return model;
		}

		[HttpGet("rayon/{rayonId}/livres")]
		public async Task<ActionResult<IEnumerable<LivreModel>>> GetLivresByRayon(int rayonId)
		{
			var livres = await repository.ListByRayonId(rayonId);
			if (!livres.Any())
				return NotFound();
			return Ok(livres);
		}

	}
}
