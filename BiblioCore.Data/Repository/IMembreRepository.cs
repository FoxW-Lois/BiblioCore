using BiblioCore.Data.Models;

namespace BiblioCore.Data.Repository
{
	public interface IMembreRepository
	{
		Task<IEnumerable<MembreModel>> Get();
		Task<MembreModel?> Get(int id);

		//Task<LivreModel?> BorrowOneLivre(int id, LivreModel model, int MembreModelId
	}
}
