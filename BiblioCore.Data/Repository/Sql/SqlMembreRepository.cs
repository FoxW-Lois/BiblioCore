using BiblioCore.Data.Models;
using BiblioCore.Data;
using Microsoft.EntityFrameworkCore;

namespace BiblioCore.Data.Repository.Sql
{
	public class SqlMembreRepository : IMembreRepository
	{
		private readonly DataContext context;

		public SqlMembreRepository(DataContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<MembreModel>> Get()
		{
			return await context.Set<MembreModel>().ToListAsync(); // Générique
		}

		public async Task<MembreModel?> Get(int id)
		{
			return await context.Set<MembreModel>().FirstOrDefaultAsync(x => x.Id == id); // Pas d'exception générique
		}

		//public async Task<LivreModel?> BorrowOneLivre(int id, LivreModel model, int MembreModelId)
		//{
		//	var entity = await context.Set<LivreModel>().FirstOrDefaultAsync(x => x.Id == id);
		//	if (entity == null)
		//		return null;
		//	context.Entry(entity).CurrentValues.SetValues(model);
		//	await context.SaveChangesAsync();
		//	return entity;
		//}
	}
}
