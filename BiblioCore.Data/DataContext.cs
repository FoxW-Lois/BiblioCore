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
			//Database.EnsureDeleted();
			//Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<LivreModel>()
			//	.HasOne(l => l.Rayon)
			//	.WithMany(r => r.Livres)
			//	.HasForeignKey(l => l.RayonModelId)
			//	.OnDelete(DeleteBehavior.Cascade);

			//modelBuilder.Entity<RayonModel>()
			//	.HasMany(r => r.Livres)
			//	.WithOne(l => l.Rayon)
			//	.HasForeignKey(l => l.RayonModelId);

			modelBuilder.Entity<LivreModel>().HasData(
				new LivreModel() { Id = 1, Titre = "Les Misérables", RayonModelId = 2 },
				new LivreModel() { Id = 2, Titre = "Notre-Dame de Paris", RayonModelId = 1 },
				new LivreModel() { Id = 3, Titre = "1984", RayonModelId = 3 },
				new LivreModel() { Id = 4, Titre = "La Ferme des animaux", RayonModelId = 1 },
				new LivreModel() { Id = 5, Titre = "Le Petit Prince", RayonModelId = 4 },
				new LivreModel() { Id = 6, Titre = "Vol de Nuit", RayonModelId = 1 }
			);

			modelBuilder.Entity<AuteurModel>().HasData(
				new AuteurModel() { Id = 1, FirstName = "Victor", LastName = "Hugo" },
				new AuteurModel() { Id = 2, FirstName = "George", LastName = "Orwell" },
				new AuteurModel() { Id = 3, FirstName = "Antoine", LastName = "de Saint-Exupéry" }
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
