namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Import;
	using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
	public static class Deserializer
	{
		private const string ErrorMessage = "Invalid Data";

		private const string SuccessfullyImportedGame
			= "Added {0} ({1}) with {2} tags";

		private const string SuccessfullyImportedUsers
			= "Imported {0} with {1} cards";
		private const string SuccessfullyImportedPurchases
			= "Imported {0} for {1}";

		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			StringBuilder sb = new StringBuilder();
			
			var jsonSettings = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
				//NullValueHandling = NullValueHandling.Ignore,
			};
			var gamesDtos = JsonConvert.DeserializeObject<List<ImportGameDto>>(jsonString, jsonSettings);
			List<Game> games = new List<Game>();
			List<Genre> genres = new List<Genre>();
			List<Developer> devs = new List<Developer>();
			List<Tag> tags = new List<Tag>();
			foreach (var gameDto in gamesDtos)
			{
				
				if (!IsValid(gameDto))
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}
				Game game = new Game
				{
					Name=gameDto.Name,
					Price = gameDto.Price

				};
				var IsDateValid = DateTime
					.TryParseExact(gameDto.ReleaseDate, "yyyy-MM-dd",
					CultureInfo.InvariantCulture,
					DateTimeStyles.None,
					out DateTime releaseDate);
				if (!IsDateValid)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}
				game.ReleaseDate = releaseDate;

				//devs start
				
				var dev=devs.FirstOrDefault(a=>a.Name== gameDto.Developer);
				;
				if (dev==null)
				{
					Developer newDev = new Developer
					{
						Name = gameDto.Developer
					};
					devs.Add(newDev);
					game.Developer = newDev;
				}
				else
				{
					game.Developer = dev;

				}
				//devs end

				//genres start
				var genre = genres.FirstOrDefault(a => a.Name == gameDto.Genre);
				if (genre == null)
				{
					Genre newgenre = new Genre
					{
						Name = gameDto.Genre
					};
					genres.Add(newgenre);
					game.Genre = newgenre;
				}
				else
				{
					game.Genre = genre;
				}

				//genres end
				foreach (var tag in gameDto.Tags)
				{
					if (String.IsNullOrEmpty(tag))
					{
						continue;
					}
					
					var targettag = tags.FirstOrDefault(a => a.Name== tag);
					if (targettag==null)
					{
						Tag newtargettag = new Tag
						{
							Name = tag
						};
						
						tags.Add(newtargettag);
						game.GameTags.Add(new GameTag { Game = game, Tag = newtargettag });
					}
					else
					{
						game.GameTags.Add(new GameTag { Game = game, Tag = targettag });

					}
				};

				games.Add(game);
				sb.AppendLine(String.Format(SuccessfullyImportedGame, game.Name, game.Genre.Name, game.GameTags.Count));
				
			}
		
			context.Games.AddRange(games);
			context.SaveChanges();
			return sb.ToString().TrimEnd();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			StringBuilder sb = new StringBuilder();
		
			var usersDto = JsonConvert.DeserializeObject<List<ImportUser>>(jsonString);
			List<User> users = new List<User>();
			List<Card> cards = new List<Card>();
			foreach (var userDto in usersDto)
			{
				
				if (!IsValid(userDto))
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}
				User user = new User
				{
					FullName = userDto.FullName,
					Username = userDto.Username,
					Email = userDto.Email,
					Age = userDto.Age
				};
				foreach (var cardDto in userDto.Cards)
				{
					if (!IsValid(cardDto))
					{
						sb.AppendLine(ErrorMessage);
						continue;
					}

					var Type = Enum.TryParse<CardType>(cardDto.Type, out CardType CardType);
					Card card = new Card
					{
						Number = cardDto.Number,
						Cvc = cardDto.CVC,
						Type = CardType
					};
					
					cards.Add(card);
					user.Cards.Add(card);
				}
				users.Add(user);
				sb.AppendLine(String.Format(SuccessfullyImportedUsers, user.Username, user.Cards.Count));
			}
			
			context.Users.AddRange(users);
			context.SaveChanges();
			return sb.ToString().TrimEnd();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			StringBuilder sb = new StringBuilder();
			XmlRootAttribute xmlRoot = new XmlRootAttribute("Purchases");
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPurchaseDto[]), xmlRoot);
			using StringReader sr = new StringReader(xmlString);
			ImportPurchaseDto[] purchasesDtos = (ImportPurchaseDto[])xmlSerializer.Deserialize(sr);
			HashSet<Purchase> purchases = new HashSet<Purchase>();
			foreach (var purdto in purchasesDtos)
			{
				if (!IsValid(purdto))
				{
					if (!IsValid(purdto))
					{
						sb.AppendLine(ErrorMessage);
						continue;
					}
				}
				//Validate =>Type-Date
				//Exist? =>Card(CardNumber)-Game(Title)
				var IsValidCardType = Enum.TryParse<PurchaseType>(purdto.Type, out PurchaseType validCardType);
				var IsValidDate = DateTime.TryParseExact(purdto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,DateTimeStyles.None,out DateTime validDate);
				
				Purchase pusrchDb = new Purchase
				{
					ProductKey = purdto.Key,
					Date= validDate,
					Type= validCardType
				};
				
				var checkCard = context.Cards.FirstOrDefault(a => a.Number == purdto.CardNumber);
				if (checkCard==null)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}
				pusrchDb.Card = checkCard;

				var checkGame = context.Games.FirstOrDefault(a => a.Name== purdto.GameTitle);
				if (checkGame == null)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}
				pusrchDb.Game = checkGame;
				purchases.Add(pusrchDb);
				sb.AppendLine(String.Format(SuccessfullyImportedPurchases, pusrchDb.Game.Name, pusrchDb.Card.User.Username));
			}
			context.Purchases.AddRange(purchases);
			context.SaveChanges();

			return sb.ToString().TrimEnd();
		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}