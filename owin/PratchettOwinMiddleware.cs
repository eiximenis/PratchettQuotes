using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace PratchettQuotes
{
    public class PratchettOwinMiddleware
    {
        private readonly AppFunc _next;
        private readonly PratchettQuotesFactory _quotesFactory;
        private readonly Random _random;
        public PratchettOwinMiddleware(AppFunc next, PratchettQuotesFactory quotesFactory)
        {
            _next = next;
            _quotesFactory = quotesFactory;
            _random = new Random();
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            await _next.Invoke(environment);
            var headers = environment["owin.ResponseHeaders"] as IDictionary<string, string[]>;
            var quotes = _quotesFactory.GetQuotes();
            var quote = quotes[_random.Next(0, quotes.Length)];
            headers.Add("X-Pratchett-Quote", new string[] { quote});
        }
    }
}
