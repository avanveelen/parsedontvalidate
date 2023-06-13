namespace ParseDontValidate;

public class AddAccountCommand
{
    public AddAccountCommand(Text name, DutchIban iban, EmailAddress emailAddress)
    {
        Name = name;
        Iban = iban;
        EmailAddress = emailAddress;
    }

    public Text Name { get; }

    public DutchIban Iban { get; }
    
    public EmailAddress EmailAddress { get; }
}