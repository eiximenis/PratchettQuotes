using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PratchettQuotes
{
    public interface IQuoteParser
    {
        IEnumerable<string> Parse(Stream input);
    }
}
