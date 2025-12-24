using Application.Common;


namespace Application.Exceptions
{
    public class ProductException :Exception
    {
        public ErrorCode ErrorCode { get; }
        public ProductException(ErrorCode code, string? message = null) : base(message ?? code.ToString())
        {
            ErrorCode = code;
        }       
    }
}
