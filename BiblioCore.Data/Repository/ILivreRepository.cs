﻿using BiblioCore.Data.Models;

namespace BiblioCore.Data.Repository
{
	public interface ILivreRepository
	{
		Task<IEnumerable<LivreModel>> Get();
		Task<LivreModel?> Get(int id);

		Task<LivreModel?> Create(LivreModel model);
		Task<LivreModel?> Update(int id, LivreModel model);
		Task Delete(int id);
	}
}