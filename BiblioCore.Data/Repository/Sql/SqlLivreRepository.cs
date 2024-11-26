using BiblioCore.Data.Models;
using BiblioCore.Data;
using Microsoft.EntityFrameworkCore;

namespace BiblioCore.Data.Repository.Sql
{
	public class SqlLivreRepository : ILivreRepository
    {
        private readonly DataContext context;

        public SqlLivreRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<LivreModel>> Get()
        {
            return await context.Set<LivreModel>().ToListAsync(); //Générique
        }

        public async Task<LivreModel?> Get(int id)
        {
            return await context.Set<LivreModel>().FirstOrDefaultAsync(x => x.Id == id); // Pas d'exception générique
        }

        public async Task<LivreModel?> Create(LivreModel model)
        {
            context.Set<LivreModel>().Add(model); // Rend le repository générique
            await context.SaveChangesAsync(); // Execution de la requete
            return model;
        }

        public async Task<LivreModel?> Update(int id, LivreModel model)
        {
            var entity = await context.Set<LivreModel>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return null;
            context.Entry(entity).CurrentValues.SetValues(model);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var model = await context.Set<LivreModel>().FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
                return;
            context.Set<LivreModel>().Remove(model);
            await context.SaveChangesAsync();
        }

		public async Task<LivreModel?> AssignRayon(int id, LivreModel model, int RayonModelId)
		{
			var entity = await context.Set<LivreModel>().FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
				return null;
			context.Entry(entity).CurrentValues.SetValues(model);
			await context.SaveChangesAsync();
			return entity;
		}

        public async Task<LivreModel?> ListRayon(int id, LivreModel model, int rayonModelId)
        {
            return await context.Set<LivreModel>().FirstOrDefaultAsync(x => x.RayonModelId == rayonModelId);
        }
    }
}
