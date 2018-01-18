using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PratchettQuotes
{
    public class PratchettCoreMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly PratchettQuotesFactory _quotesFactory;
        private readonly Random _random;
        public PratchettCoreMiddleware(RequestDelegate next, PratchettQuotesFactory quotesFactory)
        {
            _next = next;
            _quotesFactory = quotesFactory;
            _random = new Random();
        }

        public async Task Invoke(HttpContext  context)
        {

            context.Response.OnStarting(() =>
            {
                var headers = context.Response.Headers;
                var quotes = _quotesFactory.GetQuotes();
                var quote = quotes[_random.Next(0, quotes.Length)];
                headers.Add("X-Pratchett-Quote", new string[] { quote });
                return Task.CompletedTask;
            });
            await _next.Invoke(context);

        }
    }
}
