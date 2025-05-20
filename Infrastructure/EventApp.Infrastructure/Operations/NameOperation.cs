using System.Text.RegularExpressions;

namespace EventApp.Infrastructure.Operations;

public class NameOperation
{
    public static string CharacterRegulatory(string name)
    {
        string normalizedName = name.Replace("ç","c").Replace("ğ", "g").Replace("ı", "i")
            .Replace("ö", "o").Replace("ş", "s").Replace("ü", "u");
        
        normalizedName = Regex.Replace(normalizedName.ToLowerInvariant(), @"[^a-z0-9\s-]", "");
        normalizedName = Regex.Replace(normalizedName, @"\s+", "-").Trim('-');
        return normalizedName;
    }
}