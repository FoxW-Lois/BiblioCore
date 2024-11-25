namespace BiblioCore.Data.Models
{
	public class RayonModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";

		public ICollection<LivreModel> Livres { get; set; }
	}
}
