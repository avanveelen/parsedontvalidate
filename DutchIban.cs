using System.Text.RegularExpressions;

namespace ParseDontValidate;

public class DutchIban
{
    private readonly string iban;
    
    private static readonly Regex DutchIbanPattern = new("^NL[0-9]{2}[A-z0-9]{4}[0-9]{10}$", RegexOptions.Compiled);

    private DutchIban(string iban)
    {
        this.iban = iban;
    }

    public static Option<DutchIban> TryParse(string ibanCandidate)
    {
        return DutchIbanPattern.IsMatch(ibanCandidate) 
            ? F.Some(new DutchIban(ibanCandidate)) 
            : F.None;
    }
}