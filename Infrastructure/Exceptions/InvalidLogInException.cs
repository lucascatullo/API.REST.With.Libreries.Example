using Core.Models.Manager.Exception;

namespace API.Rest.Example.Infrastructure.Exception;

public class InvalidLogInException : ExceptionArgs
{
    public override int ErrorCode => 404;
    public override string Message => base.Message + "Invalid Email or password";
    public override string FancyError => "Invalid email or password.";

    public override string DescriptiveStringCode => "INVALID_EMAIL_OR_PASSWORD";
}
