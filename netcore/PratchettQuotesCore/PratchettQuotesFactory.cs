using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PratchettQuotes
{
    public class PratchettQuotesFactory
    {
        private readonly IQuoteParser _parser;
        private string[] _quotes;
        private readonly Func<Stream> _streamProvider;

        public PratchettQuotesFactory(IQuoteParser parser, Func<Stream> streamProvider)
        {
            _parser = parser;
            _streamProvider = streamProvider;
        }

        public string[] GetQuotes()
        {
            return _quotes ?? (_quotes = ReadQuotes().ToArray());
        }

        private IEnumerable<string> ReadQuotes()
        {
            var stream = _streamProvider();
            return _parser.Parse(stream);
        }

    }
}
