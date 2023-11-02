using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Support
{
    public class FileContext
    {
        public Dictionary<string, object> data = new Dictionary<string, object>();

        public void Add( string key, object value)
        { 
            data[key] = value;
        }
    }
}
