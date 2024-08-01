using Core.Models.Manager.Exception;

namespace API.Rest.Example.Infrastructure.Exception;

public class RepeatedEmailException(string email) : ExceptionArgs
{
    private readonly string _email = email;
    public override string Message => base.Message + $"{_email} is repeated.";
    public override string DescriptiveStringCode => "REPEATED_EMAIL";
    public override int ErrorCode => 400;
    public override string FancyError => $"There was a problem! There is an existing user with this email.";
}
