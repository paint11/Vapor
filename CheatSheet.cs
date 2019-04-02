// IsValid Method: 

private static bool IsValid(object obj)
{
    var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
    var validationresult = new List<ValidationResult>();

    var isValid = Validator.TryValidateObject(obj, validationContext, validationresult, true);
    return isValid;
}

//Message examples
private const string SuccessMessage = "Successfully imported {0} {1}.";
private const string SuccessFollowerMessage = "Successfully imported Follower {0} to User {1}.";
private const string ErrorMessage = "Error: Invalid data.";

//Use of messages
sb.AppendLine(string.Format(SuccessMessage, params))


//Json Import
public static string ImportGames(VaporStoreDbContext context, string jsonString)
{
  var sb = new StringBuilder();

  var deserializedGames = JsonConvert.DeserializeObject<GameDto[]>(jsonString);

  var games = new List<Game>();
 

  foreach (var gameDto in deserializedGames)
  {
    if (!IsValid(gameDto))
    {
      sb.AppendLine("Invalid Data");
      continue;
    }     
   
    games.Add(game);
    .......................
    sb.AppendLine($"Added {gameDto.Name} ({gameDto.Genre}) with {gameDto.Tags.Length} tags");
  }

  context.Games.AddRange(games);

  context.SaveChanges();

  var result = sb.ToString();

  return result;
}
