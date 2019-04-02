// IsValid Method: 

private static bool IsValid(object obj)
{
    var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
    var validationresult = new List<ValidationResult>();

    var isValid = Validator.TryValidateObject(obj, validationContext, validationresult, true);
    return isValid;
}

//MESSAGE EXAMPLES
private const string SuccessMessage = "Successfully imported {0} {1}.";
private const string SuccessFollowerMessage = "Successfully imported Follower {0} to User {1}.";
private const string ErrorMessage = "Error: Invalid data.";

//USE OF MESSAGES
sb.AppendLine(string.Format(SuccessMessage, params))


//JSON IMPORT
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

//XML IMPORT
public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
{
    var sb = new StringBuilder();

    var serializer = new XmlSerializer(typeof(PurchaseDto[]), new XmlRootAttribute("Purchases"));

    var deserialized = (PurchaseDto[]) serializer.Deserialize(new StringReader(xmlString));

    var validPurchases = new List<Purchase>();

    foreach (var purchaseDto in deserialized)
    {
        if (!IsValid(purchaseDto))
        {
            sb.AppendLine("Invalid Data");
            continue;
        }

        var game = context.Games.Single(g => g.Name == purchaseDto.Title);
        var card = context.Cards.Include(c => c.User).Single(c => c.Number == purchaseDto.Card);
        var date = DateTime.ParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

        var purchase = new Purchase(game, purchaseDto.Type, card, purchaseDto.Key, date);

        validPurchases.Add(purchase);
        sb.AppendLine($"Imported {purchase.Game.Name} for {purchase.Card.User.Username}");
    }

    context.Purchases.AddRange(validPurchases);
    context.SaveChanges();

    var result = sb.ToString();
    return result;
}

//JSON EXPORT
public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
{    
    var result = context.Genres
        .Where(g => genreNames.Contains(g.Name))...
        ...........................................

    var json = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
    return json;
}

//XML IMPORT
public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
{
    var storeTypeValue = Enum.Parse<PurchaseType>(storeType);
    var purchases = context.Users...
        ............................
       

    var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("Users"));

    var sb = new StringBuilder();
    var namespaces = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty});
    serializer.Serialize(new StringWriter(sb), purchases, namespaces);

    var result = sb.ToString();
    return result;
}
