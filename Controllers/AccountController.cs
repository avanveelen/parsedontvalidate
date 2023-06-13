using Microsoft.AspNetCore.Mvc;

namespace ParseDontValidate.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    /// <summary>
    /// 1. Name can not be empty.
    /// 2. Iban must be a valid dutch iban.
    /// 3. Email must be valid email.
    /// 4. Iban must be unique.
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    [HttpPost("add")]
    public IActionResult AddAccount(AddAccountRequestModel requestModel)
    {
        IActionResult AddAccount(AddAccountCommand command)
        {
            return AccountApplicationService.AddAccount(command)
                .Match<IActionResult>(_ => 
                    Ok(), 
                    error => BadRequest(error));
        }
        
        var name = Text.TryParse(requestModel.Name);
        var iban = DutchIban.TryParse(requestModel.Iban);
        var email = EmailAddress.TryParse(requestModel.Email);

        var command = 
            from n in name.ToValidation("Name can not be empty.")
            from i in iban.ToValidation("Iban is not a valid Dutch iban.")
            from e in email.ToValidation("Email is not a valid email address.")
            select new AddAccountCommand(n, i, e);

        return command.Match<IActionResult>(AddAccount, error => BadRequest($"Add account failed. {error}"));
    }
}