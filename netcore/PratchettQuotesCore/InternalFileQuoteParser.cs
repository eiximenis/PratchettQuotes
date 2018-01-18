using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PratchettQuotes
{
    class InternalFileQuoteParser : IQuoteParser
    {

        public IEnumerable<string> Parse(System.IO.Stream input)
        {
            var quotes = new List<string>();
            using (var reader = new StreamReader(input))
            {
                var currentQuote = "";
                var line = reader.ReadLine();
                while (line != null) {
                    if (line == string.Empty && !string.IsNullOrWhiteSpace(currentQuote))
                    {
                        quotes.Add(currentQuote.Trim());
                        currentQuote = "";
                    }
                    else
                    {
                        var trimmedline = line.Trim();
                        if (!trimmedline.StartsWith("--") && !trimmedline.StartsWith("(Terry Pratchett,"))
                        {
                            currentQuote += " " + trimmedline;
                        }
                    }
                    line = reader.ReadLine();
                }
            }

            return quotes;
        }
    }
}
