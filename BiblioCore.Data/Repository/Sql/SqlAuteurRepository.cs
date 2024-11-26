using BiblioCore.Data.Models;
using BiblioCore.Data;
using Microsoft.EntityFrameworkCore;

namespace BiblioCore.Data.Repository.Sql
{
	public class SqlAuteurRepository : IAuteurRepository
	{
		private readonly DataContext context;

		public SqlAuteurRepository(DataContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<AuteurModel>> Get()
		{
			return await context.Set<AuteurModel>().ToListAsync(); // Générique
		}

		public async Task<AuteurModel?> Get(int id)
		{
			return await context.Set<AuteurModel>().FirstOrDefaultAsync(x => x.Id == id); // Pas d'exception générique
		}

		public async Task<AuteurModel?> Create(AuteurModel model)
		{
			context.Set<AuteurModel>().Add(model); // Rend le repository générique
			await context.SaveChangesAsync(); // Execution de la requete
			return model;
		}

		public async Task<AuteurModel?> Update(int id, AuteurModel model)
		{
			var entity = await context.Set<AuteurModel>().FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
				return null;
			context.Entry(entity).CurrentValues.SetValues(model);
			await context.SaveChangesAsync();
			return entity;
		}

		public async Task Delete(int id)
		{
			var model = await context.Set<AuteurModel>().FirstOrDefaultAsync(x => x.Id == id);
			if (model == null)
				return;
			context.Set<AuteurModel>().Remove(model);
			await context.SaveChangesAsync();
		}

        public async Task<IEnumerable<LivreModel>> ListByAuteurId(int auteurId)
        {
            return await context.Set<LivreModel>().Where(x => x.AuteurModelId == auteurId).ToListAsync();
        }
    }
}
