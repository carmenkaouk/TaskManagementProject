using RequestResponse.Enums;

namespace RequestResponse;

public class Response
{
    public Guid RequestId { get; set; }
    public StatusCodes StatusCode { get; set; }

    public object? Content;
    public string ExceptionMessage { get; set; }
}
