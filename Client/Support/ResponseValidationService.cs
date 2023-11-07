using RequestResponse;
using RequestResponse.Enums;


namespace Client.Support;

public static class ResponseValidationService
{
    public static void ValidateResponse(Response response)
    {
        switch(response.StatusCode)
        {
            case StatusCodes.Exception:
                throw new Exception(response.ExceptionMessage);
            
        }
    }
}
