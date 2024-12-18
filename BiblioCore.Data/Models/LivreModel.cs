﻿namespace BiblioCore.Data.Models
{
	public class LivreModel
	{
		public int Id { get; set; }
		public string Titre { get; set; } = "";
		public int AuteurModelId { get; set; } = 1;
		public int RayonModelId { get; set; } = 1;
		public int MembreModelId { get; set; } = 1;
		public bool IsDispo { get; set; } = true;
	}
}
