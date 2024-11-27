using BiblioCore.Data.Models;
using BiblioCore.Data.Repository;
using BiblioCore.Data.Repository.Api;
using System.Text;

namespace BiblioCore.Cmd
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Bienvenue sur l'API BiblioCore !\n");
			Thread.Sleep(1000); // Attente du server
			ILivreRepository repository = new ApiLivreRepository("http://localhost:5049/api/");
			IRayonRepository rayonRepository = new ApiRayonRepository("http://localhost:5049/api/");
            IAuteurRepository auteurRepository = new ApiAuteurRepository("http://localhost:5049/api/");
			//IMembreRepository membreRepository = new ApiMembreRepository("http://localhost:5049/api/");

			while (true)
			{
                Console.WriteLine("+---------------------------------------+\n" +
								"|       Que voulez-vous faire ?         |\n" +
								"+---------------------------------------+\n" +
								"|                                       |\n" +
								"|       Voir tous les livres : 1        |\n" +
								/*"|        Voir un seul livre : 2         |\n" +*/
								"|         Ajouter un livre : 2          |\n" +
								"|         Modifier un livre : 3         |\n" +
								"|        Supprimer un livre : 4         |\n" +
								"|   Assigner un rayon à un livre : 5    |\n" +
								"|    Lister les livres par rayon : 6    |\n" +
								"|   Lister les livres par auteur : 7    |\n" +
								"|   Rechercher un livre par titre : 8   |\n" +
								"|        Emprunter un livre : 9         |\n" +
								"|         Rendre un livre : 10          |\n" +
								"|   Lister les livres empruntés : 11    |\n" +
								"|                                       |\n" +
								"+---------------------------------------+\n"
				);

                if (!int.TryParse(Console.ReadLine(), out int Action) || Action < 1 || Action > 11)
				{
					Console.Write("Cette Action n'est pas valide. Veuillez réessayer\n\n");
					return;
				}

				switch (Action) {
					case 1 :
						Console.WriteLine("\n");
						GetLivres(repository);
						Console.WriteLine("\n");
					break;

					//case 2:
					//	Console.WriteLine("\n");
					//	GetLivre(repository);
					//	Console.WriteLine("\n");
					//break;

					case 2:
						Console.WriteLine("\n");
						CreateLivre(repository);
						Console.WriteLine("\n");
					break;

					case 3:
						Console.WriteLine("\n");
						UpdateLivre(repository);
						Console.WriteLine("\n");
					break;

					case 4:
						Console.WriteLine("\n");
						DeleteLivre(repository);
						Console.WriteLine("\n");
					break;

					case 5:
						Console.WriteLine("\n");
						AssignRayonToLivre(repository);
						Console.WriteLine("\n");
					break;

                    case 6:
                        Console.WriteLine("\n");
                        ListLivreByRayon(rayonRepository);
                        Console.WriteLine("\n");
                    break;

                    case 7:
                        Console.WriteLine("\n");
                        ListLivreByAuteur(auteurRepository);
                        Console.WriteLine("\n");
                    break;

					case 8:
						Console.WriteLine("\n");
						SearchLivreByTitre(rayonRepository);
						Console.WriteLine("\n");
					break;

					case 9:
						Console.WriteLine("\n");
						BorrowLivre(repository);
						Console.WriteLine("\n");
					break;

                    case 10:
                        Console.WriteLine("\n");
                        ReturnLivre(repository);
                        Console.WriteLine("\n");
                    break;

					case 11:
						Console.WriteLine("\n");
						GetLivreEmprunt(repository);
						Console.WriteLine("\n");
					break;
				}
			}
		}

		// ------------------------------ Fonctions CRUD ------------------------------

		private static void GetLivres(ILivreRepository repository)
		{
			var models = repository.Get().Result;
			if (models == null)
			{
				Console.WriteLine("Erreur Api");
				return;
			}

			Console.WriteLine(
							"+----------------------------------------------------+\n" +
							"|\n" +
							string.Join("\n", models.Select(x => $"|    - Livre n°{x.Id} : \t{x.Titre} \n|\t\t\tAuteur : {x.AuteurModelId} \n|\t\t\tRayon : {x.RayonModelId} \n|\t\t\tEmprunteur : {x.MembreModelId} \n|\t\t\tDisponibilité : {x.IsDispo}\n|")) +
							"\n" +
							"+----------------------------------------------------+");
		}

		//private static void GetLivre(ILivreRepository repository)
		//{
		//	while (true)
		//	{
		//		int id = GetId("+----------------------------------------------------+\n" +
		//						"|    - Id du Livre à voir : ");
		//		if (id == 0)
		//			return;
		//		var model = repository.Get(id).Result;
		//		if (model == null)
		//		{
		//			Console.WriteLine("+----------------------------------------------------+\n");
		//			Console.WriteLine("Livre introuvable\n");
		//		}
		//		else
		//			Console.WriteLine(
		//					"+----------------------------------------------------+\n" +
		//					"|\n" +
		//					$"|    Livre n°{model.Id} : \t{model.Titre} \n|\t\t\tAuteur : {model.AuteurModelId} \n|\t\t\tRayon : {model.RayonModelId}\n|" +
		//					"\n" +
		//					"+----------------------------------------------------+\n");

		//		bool continuer = ContinueOrNo();
		//		if (!continuer)
		//		{
		//			break;
		//		}
		//	}
		//}

		private static void CreateLivre(ILivreRepository repository)
		{
			while (true)
			{
				Console.Write("+----------------------------------------------------+\n" +
									"|   Entrez un titre pour le nouveau Livre : ");
				string lTitre = Console.ReadLine();
				Console.WriteLine("+----------------------------------------------------+");

				Console.Write("|   Entrez l'Id de l'auteur du nouveau Livre : ");
				if (!int.TryParse(Console.ReadLine(), out int lAuteurModelId) && lAuteurModelId == 0)
				{
					Console.WriteLine("L'Id doit être exclusivement composé chiffre");
					return;
				}
				if (lAuteurModelId == 0)
				{
					return;
				}
				Console.WriteLine("+----------------------------------------------------+\n");

				var Livre = new LivreModel() { Titre = lTitre, AuteurModelId = lAuteurModelId };
				Livre = repository.Create(Livre).Result;

				if (Livre == null)
					Console.WriteLine("Livre introuvable\n");
				else
					Console.WriteLine($"Le livre n°{Livre.Id} : {Livre.Titre}, Auteur : {Livre.AuteurModelId} a été ajouté\n");

				bool continuer = ContinueOrNo();
				if (!continuer)
				{
					break;
				}
			}
		}

		private static void UpdateLivre(ILivreRepository repository)
		{
			while (true)
			{
				int id = GetId("+----------------------------------------------------+\n" +
								"|   Id du Livre à modifier : ");
				if (id == 0)
					return;

				var model = repository.Get(id).Result;

				if (model == null)
				{
					Console.WriteLine("+----------------------------------------------------+\n");
					Console.WriteLine("Livre introuvable\n");
				}
				else
				{
					Console.Write("+----------------------------------------------------+\n" +
										"|   Entrez le nouveau titre du Livre : ");
					string lNouveauTitre = Console.ReadLine();
					Console.WriteLine("+----------------------------------------------------+");

					model.Titre = lNouveauTitre;
					model = repository.Update(id, model).Result;
					Console.WriteLine($"Mise à jour du livre n°{model.Id} : {model.Titre} \n");
				}

				bool continuer = ContinueOrNo();
				if (!continuer)
				{
					break;
				}
			}
		}

		private static void DeleteLivre(ILivreRepository repository)
		{
			while (true)
			{
				int id = GetId("+----------------------------------------------------+\n" +
								"|   Id du Livre à supprimer : ");
				if (id == 0)
					return;
				Console.WriteLine("+----------------------------------------------------+\n");
				repository.Delete(id).Wait();
				Console.WriteLine("Suppression effectuée\n");

				bool continuer = ContinueOrNo();
				if (!continuer)
				{
					break;
				}
			}
		}


		// ----------------------------- Fonctionnalités ------------------------------

		private static void AssignRayonToLivre(ILivreRepository repository)
		{
			while (true)
			{
				int id = GetId("+----------------------------------------------------+\n" +
								"|   Id du Livre à ranger : ");
				if (id == 0)
					return;

				Console.WriteLine("+----------------------------------------------------+\n");

				var model = repository.Get(id).Result;

				if (model == null)
					Console.WriteLine("Livre introuvable");
				else
				{
					Console.Write("+----------------------------------------------------+\n" +
										"|   Entrez l'Id du Rayon du Livre : ");

					if (!int.TryParse(Console.ReadLine(), out int lRayonModelId))
					{
						Console.WriteLine("L'Id doit être exclusivement composé chiffre");
						return;
					}
					Console.WriteLine("+----------------------------------------------------+");

					model.RayonModelId = lRayonModelId;
					model = repository.AssignRayon(id, model, lRayonModelId).Result;
					Console.WriteLine($"Le livre n°{model.Id} : {model.Titre} a été rangé dans le rayon : {model.RayonModelId} \n");
				}

				bool continuer = ContinueOrNo();
				if (!continuer)
				{
					break;
				}
			}
		}

		private static void ListLivreByRayon(IRayonRepository rayonRepository)
		{
			while (true)
			{
				int id = GetId("+----------------------------------------------------+\n" +
								"|   Id du Rayon à afficher : ");
				if (id == 0)
					return;

				var model = rayonRepository.ListByRayonId(id).Result;
				if (model == null || !model.Any())
				{
					Console.WriteLine("+----------------------------------------------------+\n");
					Console.WriteLine("Rayon introuvable ou aucun livre trouvé\n");
				}
				else
				{
					Console.WriteLine(
							"+----------------------------------------------------+\n" +
							"|\n" +
							string.Join("\n", model.Select(l => $"|    - Livre n°{l.Id} : \t{l.Titre} \n|\t\t\tAuteur : {l.AuteurModelId} \n|\t\t\tRayon : {l.RayonModelId}\n|")) +
							"\n" +
							"+----------------------------------------------------+");
				}

				bool continuer = ContinueOrNo();
				if (!continuer)
				{
					break;
				}
			}
		}

		private static void ListLivreByAuteur(IAuteurRepository auteurRepository)
        {
            while (true)
            {
                int id = GetId("+----------------------------------------------------+\n" +
                                "|   Id de l'Auteur à afficher : ");
                if (id == 0)
                    return;

                var model = auteurRepository.ListByAuteurId(id).Result;
                if (model == null || !model.Any())
                {
                    Console.WriteLine("+----------------------------------------------------+\n");
                    Console.WriteLine("Auteur introuvable ou aucun livre trouvé\n");
                }
                else
                {
                    Console.WriteLine(
							"+----------------------------------------------------+\n" +
							"|\n" +
							string.Join("\n", model.Select(l => $"|    - Livre n°{l.Id} : \t{l.Titre} \n|\t\t\tAuteur : {l.AuteurModelId} \n|\t\t\tRayon : {l.RayonModelId}\n|")) +
							"\n" +
							"+----------------------------------------------------+");
                }

                bool continuer = ContinueOrNo();
                if (!continuer)
                {
                    break;
                }
            }
        }

		private static void SearchLivreByTitre(IRayonRepository rayonRepository)
		{
			while (true)
			{
				Console.Write("+----------------------------------------------------+\n" +
								"|   Titre du livre à rechercher : ");

				string titre = Console.ReadLine();
				int id = 1;
				if (titre == null)
					return;

				var model = rayonRepository.SearchByTitre(titre).Result;
				if (model == null)
				{
					Console.WriteLine("+----------------------------------------------------+\n");
					Console.WriteLine("Livre introuvable\n");
				}
				else
				{
					Console.WriteLine(
							"+----------------------------------------------------+\n" +
							"|\n" +
							$"|    - Livre n°{model.Id} : \t{model.Titre} \n|\t\t\tAuteur : {model.AuteurModelId} \n|\t\t\tRayon : {model.RayonModelId}\n" +
							"|\n" +
							"+----------------------------------------------------+");
				}

				bool continuer = ContinueOrNo();
				if (!continuer)
				{
					break;
				}
			}
		}

		private static void BorrowLivre(ILivreRepository repository)
		{
			while (true)
			{
				int id = GetId("+----------------------------------------------------+\n" +
								"|   Id du livre à emprunter : ");
				if (id == 0)
					return;

				var model = repository.Get(id).Result;

				if (model == null)
					Console.WriteLine("Livre introuvable");
				else
				{
					Console.Write("+----------------------------------------------------+\n" +
								"|   Id du Membre qui emprunte : ");

					if (!int.TryParse(Console.ReadLine(), out int lMembreModelId))
					{
						Console.WriteLine("L'Id doit être exclusivement composé chiffre");
						return;
					}
					Console.WriteLine("+----------------------------------------------------+");

					model.MembreModelId = lMembreModelId;
					model.IsDispo = false;
					bool lIsDispo = model.IsDispo;
					model = repository.BorrowOneLivre(id, model, lMembreModelId, lIsDispo).Result;

					if (model.MembreModelId != 1)
						lIsDispo = false;
					Console.WriteLine($"Le livre n°{model.Id} : {model.Titre} a été emprunté par : {model.MembreModelId}, Disponibilité : {lIsDispo} \n");
				}

				bool continuer = ContinueOrNo();
				if (!continuer)
				{
					break;
				}
			}
		}

        private static void ReturnLivre(ILivreRepository repository)
        {
            while (true)
            {
                int id = GetId("+----------------------------------------------------+\n" +
                                "|   Id du livre à rendre : ");
                if (id == 0)
                    return;

                var model = repository.Get(id).Result;
				Console.WriteLine("+----------------------------------------------------+");
                if (model == null)
                    Console.WriteLine("Livre introuvable");
                else
                {
					int lMembreModelId = 1;
                    model.MembreModelId = lMembreModelId;
                    model.IsDispo = true;
                    bool lIsDispo = model.IsDispo;
                    model = repository.ReturnOneLivre(id, model, lMembreModelId, lIsDispo).Result;

                    if (model.MembreModelId == 1)
                        lIsDispo = true;
                    Console.WriteLine($"Le livre n°{model.Id} : {model.Titre} a été rendu, Disponibilité : {lIsDispo} \n");
                }

                bool continuer = ContinueOrNo();
                if (!continuer)
                {
                    break;
                }
            }
        }

		private static void GetLivreEmprunt(ILivreRepository repository)
		{
			//var models = repository.GetLivreEmpruntes(false).Result;
			//if (models == null)
			//{
			//	Console.WriteLine("Erreur Api");
			//	return;
			//}

			//Console.WriteLine(
			//				"+----------------------------------------------------+\n" +
			//				"|\n" +
			//				string.Join("\n", models.Select(x => $"|    - Livre n°{x.Id} : \t{x.Titre} \n|\t\t\tAuteur : {x.AuteurModelId} \n|\t\t\tRayon : {x.RayonModelId} \n|\t\t\tEmprunteur : {x.MembreModelId} \n|\t\t\tDisponibilité : {x.IsDispo}\n|")) +
			//				"\n" +
			//				"+----------------------------------------------------+");

			var models = repository.Get().Result;
			if (models == null)
			{
				Console.WriteLine("Erreur Api");
				return;
			}

			Console.WriteLine(
							"+----------------------------------------------------+\n" +
							"|\n" +
							string.Join("\n", models
								.Where(x => !x.IsDispo)
								.Select(x => $"|    - Livre n°{x.Id} : \t{x.Titre} \n|\t\t\tAuteur : {x.AuteurModelId} \n|\t\t\tRayon : {x.RayonModelId} \n|\t\t\tEmprunteur : {x.MembreModelId} \n|\t\t\tDisponibilité : {x.IsDispo}\n|")) +
							"\n" +
							"+----------------------------------------------------+");
		}

		// ----------------------------------------------------------------------------

		private static int GetId(string message)
		{
			do
			{
				Console.Write(message);
				if (int.TryParse(Console.ReadLine(), out int id))
					return id;
			} while (true);
		}

		private static bool ContinueOrNo()
		{
			while (true)
			{
				Console.WriteLine("Voulez-vous continuer ? (y/n)\n");
				string reponse = Console.ReadLine();
				if (reponse == "y")
				{
					Console.WriteLine("\n");
					return true;
				}
				else if (reponse == "n")
					return false;
				else
				{
					Console.WriteLine("Erreur veuillez réessayer");
				}
			}
		}
	}
}

