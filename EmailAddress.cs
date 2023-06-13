using System.Text.RegularExpressions;

namespace ParseDontValidate;

public class EmailAddress
{
    private readonly string email;
    
    private static readonly Regex EmailPattern = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled);

    public EmailAddress(string email)
    {
        this.email = email;
    }
    
    public static Option<EmailAddress> TryParse(string emailAddress)
    {
        return EmailPattern.IsMatch(emailAddress) 
            ? F.Some(new EmailAddress(emailAddress)) 
            : F.None;
    }

    public override string ToString()
    {
        return email;
    }
}