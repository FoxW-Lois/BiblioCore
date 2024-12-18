﻿using BiblioCore.Data.Models;

namespace BiblioCore.Data.Repository
{
	public interface ILivreRepository
	{
		Task<IEnumerable<LivreModel>> Get();
		Task<LivreModel?> Get(int id);

		Task<LivreModel?> Create(LivreModel model);
		Task<LivreModel?> Update(int id, LivreModel model);
		Task<LivreModel?> AssignRayon(int id, LivreModel model, int RayonModelId);
		Task<LivreModel?> ListRayon(int id, LivreModel model, int RayonModelId);
		//Task<LivreModel?> SearchByTitre(string titre);
		Task<LivreModel?> BorrowOneLivre(int id, LivreModel model, int MembreModelId, bool isDispo);
        Task<LivreModel?> ReturnOneLivre(int id, LivreModel model, int MembreModelId, bool isDispo);
		Task<IEnumerable<LivreModel>> GetLivreEmpruntes(bool isDispo);
		Task Delete(int id);

	}
}
