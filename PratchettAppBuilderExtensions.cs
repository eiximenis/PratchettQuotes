using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PratchettQuotes
{
    public static class PratchettAppBuilderExtensions
    {
        public static void UseTerryPratchett(this IAppBuilder app)
        {
            app.Use(typeof(PratchettOwinMiddleware), 
                new PratchettQuotesFactory(
                    new InternalFileQuoteParser(),
                    () =>  Assembly.GetExecutingAssembly().GetManifestResourceStream("PratchettQuotes.terry_quotes.txt")));
        }

        public static void UseTerryPratchett(this IAppBuilder app, IQuoteParser quoteParser, string filename)
        {
            app.Use(typeof(PratchettOwinMiddleware),
                new PratchettQuotesFactory(quoteParser, () => new FileStream(filename, FileMode.Open, FileAccess.Read)));
        }

        public static void UseTerryPratchett(this IAppBuilder app, IQuoteParser quoteParser, Func<Stream> quotesProvider)
        {
            app.Use(typeof(PratchettOwinMiddleware), new PratchettQuotesFactory(quoteParser, quotesProvider));
        }
    }
}
