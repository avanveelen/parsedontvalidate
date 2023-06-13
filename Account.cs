namespace ParseDontValidate;

public class Account
{
    public Account(Text name, DutchIban iban, EmailAddress emailAddress)
    {
        this.name = name;
        Iban = iban;
        EmailAddress = emailAddress;
    }

    public Text name { get; }
    public DutchIban Iban { get; }
    public EmailAddress EmailAddress { get; }
}