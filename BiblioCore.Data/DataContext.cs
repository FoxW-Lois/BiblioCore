using Microsoft.EntityFrameworkCore;
using BiblioCore.Data.Models;

namespace BiblioCore.Data
{
	public  class DataContext : DbContext
	{
		public DbSet<LivreModel> Livre { get; set; }
		public DbSet<AuteurModel> Auteur { get; set; }

		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
			Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<LivreModel>().HasData(
				new LivreModel() { Id = 1, Titre = "Arbre" },
				new LivreModel() { Id = 2, Titre = "Bob" },
				new LivreModel() { Id = 2, Titre = "Chien" }
			);

			modelBuilder.Entity<AuteurModel>().HasData(
				new AuteurModel() { Id = 1, FirstName = "Aaaa", LastName = "Bbbbb" },
				new AuteurModel() { Id = 2, FirstName = "Ccccc", LastName = "Ddddd" },
				new AuteurModel() { Id = 3, FirstName = "Eeeee", LastName = "Fffff" }
			);

			modelBuilder.Entity<RayonModel>().HasData(
				new RayonModel() { Id = 1, Name = "A" },
				new RayonModel() { Id = 2, Name = "B" },
				new RayonModel() { Id = 2, Name = "C" }
			);

			modelBuilder.Entity<MembreModel>().HasData(
				new MembreModel() { Id = 1, FirstName = "1A", LastName = "1B" },
				new MembreModel() { Id = 2, FirstName = "2C", LastName = "2D" },
				new MembreModel() { Id = 3, FirstName = "3E", LastName = "3F" }
			);

		}
	}
}
