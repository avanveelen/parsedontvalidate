using ParseDontValidate.Controllers;

namespace ParseDontValidate;

public static class AccountApplicationService
{
    public static Validation<string, Unit> AddAccount(AddAccountCommand command)
    {
        var ibanIsUnique = TestIbanUniqueness(command.Iban);
        
        if (!ibanIsUnique)
        {
            return new Validation<string, Unit>("Iban is already in use.");
        }

        var account = new Account(command.Name, command.Iban, command.EmailAddress);
        StoreAccount(account);
        
        return Unit.Default;
    }

    private static bool TestIbanUniqueness(DutchIban iban)
    {
        return false;
    }

    private static void StoreAccount(Account account)
    {
        
    }
}