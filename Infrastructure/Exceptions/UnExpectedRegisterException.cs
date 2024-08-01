using Core.Models.Manager.Exception;

namespace API.Rest.Example.Infrastructure.Exception;

public class UnExpectedRegisterException : ExceptionArgs
{
    public override int ErrorCode => 500;
    public override string Message => base.Message + "Something unexpected happened when trying to create the user.";
    public override string DescriptiveStringCode => "FAIL_TO_REGISTER";
}
