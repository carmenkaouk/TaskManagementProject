using Presentation.Middlewares;

namespace Presentation.Support;

public class MiddleWareManager
{
    public Middleware ConstructPipeline()
    {
        var middlewareHead = new ErrorHandlingMiddleware();
        middlewareHead.SetNext(new DeserializationWrappedRequestMiddleware())
            .SetNext(new DecryptionMiddleware())
            .SetNext(new DeserializationRequestMiddleware())
            .SetNext(new MethodResolutionMiddleware())
            .SetNext(new ParameterExtractionMiddleware())
            .SetNext(new EndpointMiddleware());
        return middlewareHead;
    }
}
