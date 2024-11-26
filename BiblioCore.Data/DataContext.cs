using Microsoft.EntityFrameworkCore;
using BiblioCore.Data.Models;

namespace BiblioCore.Data
{
	public class DataContext : DbContext
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
				new LivreModel() { Id = 1, Titre = "Les Misérables", AuteurModelId = 2, RayonModelId = 2 },
				new LivreModel() { Id = 2, Titre = "Notre-Dame de Paris", AuteurModelId = 2, RayonModelId = 1 },
				new LivreModel() { Id = 3, Titre = "1984", AuteurModelId = 3, RayonModelId = 3 },
				new LivreModel() { Id = 4, Titre = "La Ferme des animaux", AuteurModelId = 3, RayonModelId = 1 },
				new LivreModel() { Id = 5, Titre = "Le Petit Prince", AuteurModelId = 4, RayonModelId = 4 },
				new LivreModel() { Id = 6, Titre = "Vol de Nuit", AuteurModelId = 4, RayonModelId = 1 }
			);

			modelBuilder.Entity<AuteurModel>().HasData(
				new AuteurModel() { Id = 1, FirstName = "Sans", LastName = "Auteur" },
				new AuteurModel() { Id = 2, FirstName = "Victor", LastName = "Hugo" },
				new AuteurModel() { Id = 3, FirstName = "George", LastName = "Orwell" },
				new AuteurModel() { Id = 4, FirstName = "Antoine", LastName = "de Saint-Exupéry" }
			);

			modelBuilder.Entity<RayonModel>().HasData(
				new RayonModel() { Id = 1, Name = "Sans rayon" },
				new RayonModel() { Id = 2, Name = "Classiques Français" },
				new RayonModel() { Id = 3, Name = "Dystopies" },
				new RayonModel() { Id = 4, Name = "Littérature" }
			);

			modelBuilder.Entity<MembreModel>().HasData(
				new MembreModel() { Id = 1, FirstName = "Alice", LastName = "Dupont" },
				new MembreModel() { Id = 2, FirstName = "Jean", LastName = "Martin" },
				new MembreModel() { Id = 3, FirstName = "Sophie", LastName = "Durand" }
			);

		}
	}
}
