using Presentation.Support;
using RequestResponse;
using SharedLibrary.Encryption;
using System.Text;


namespace Presentation.Middlewares; 

internal class DecryptionMiddleware : Middleware
{
    public override Response ProcessRequest(FileContext fileContext)
    {
        WrappedRequest wrappedRequest = (WrappedRequest)fileContext.data["WrappedRequest"];
        byte[] symmetricKey = RsaEncryption.Decrypt(wrappedRequest.EncryptedSymmetricKey);
        fileContext.Add("SymmetricKey", symmetricKey);
        fileContext.Add("Iv", wrappedRequest.Iv);
        byte[] requestAsBytes = AesEncryption.Decrypt(wrappedRequest.EncryptedRequest, symmetricKey, wrappedRequest.Iv); 
        string requestAsJson = UTF8Encoding.UTF8.GetString(requestAsBytes);
        fileContext.Add("RequestAsJson", requestAsJson); 
        return _next.ProcessRequest(fileContext);

    }
}
