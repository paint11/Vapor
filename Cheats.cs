//Full DateTime validation
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
