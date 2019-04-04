//Full DateTime validation  
//If during the if check the parsing is successful, the incarceration variable is assigned the parsed value
DateTime incarceration;
if (!DateTime.TryParseExact(dto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out incarceration))
{
    sb.AppendLine(InvalidData);
    continue;
}

DateTime release;
if (dto.ReleaseDate != null)
{
    if (!DateTime.TryParseExact(dto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out release))
    {
        sb.AppendLine(InvalidData);
        continue;
    }
}


Prisoner prisoner = new Prisoner()
{
    FullName = dto.FullName,
    Nickname = dto.Nickname,
    Age = dto.Age,
    IncarcerationDate = incarceration,
    ReleaseDate = dto.ReleaseDate == null ? (DateTime?) null : DateTime.ParseExact(dto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
    Bail = dto.Bail,
    CellId = dto.CellId
};

//Without DateTime validation, just validation if the variable is null
var incarcerationDate = DateTime.ParseExact(dto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

DateTime? releaseDate = null;
if (dto.ReleaseDate != null)
{
    releaseDate = DateTime.ParseExact(dto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
}

var prisoner = new Prisoner
{
    FullName = dto.FullName,
    Nickname = dto.Nickname,
    Age = dto.Age,
    IncarcerationDate = incarcerationDate,
    ReleaseDate = releaseDate,
    Bail = dto.Bail,
    CellId = dto.CellId,
    Mails = mails
};

//Break method inside a nested foreach causing the outer foreach to continue 
var isValidMail = true;
var mails = new List<Mail>();
foreach (var mailDto in dto.Mails)
{
    if (!IsValid(mailDto))
    {
        isValidMail = false;
        break;
    }

    var mail = new Mail
    {
        Description = mailDto.Description,
        Sender = mailDto.Sender,
        Address = mailDto.Address
    };

    mails.Add(mail);
}

if (!isValidMail)
{
    sb.AppendLine(ErrorMessage);
    continue;
}

var prisoner = new Prisoner
{
    FullName = dto.FullName,
    Nickname = dto.Nickname,
    Age = dto.Age,
    IncarcerationDate = incarcerationDate,
    ReleaseDate = releaseDate,
    Bail = dto.Bail,
    CellId = dto.CellId,
    Mails = mails
};
