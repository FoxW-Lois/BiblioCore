namespace BiblioCore.Data.Models
{
	public class LivreModel
	{
		public int Id { get; set; }
		public string Titre { get; set; } = "";
		public bool IsDispo { get; set; } = true;
		public int AuteurModelId { get; set; } = 0;
		public int RayonModelId { get; set; } = 0;
	}
}
