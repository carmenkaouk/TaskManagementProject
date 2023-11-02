using Presentation.Support;
using RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Middlewares; 

public abstract class Middleware
{
    protected Middleware _next; 
    public Middleware SetNext(Middleware next)
    {
        _next = next;
        return _next; 
    }
    public abstract  Response ProcessRequest(FileContext fileContext); 
}
