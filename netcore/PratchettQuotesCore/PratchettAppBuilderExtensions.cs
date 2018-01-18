using Microsoft.AspNetCore.Builder;
using PratchettQuotes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class PratchettAppBuilderExtensions
    {
        public static void UseTerryPratchett(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(PratchettCoreMiddleware), 
                new PratchettQuotesFactory(
                    new InternalFileQuoteParser(),
                    () =>  Assembly.GetExecutingAssembly().GetManifestResourceStream("PratchettQuotesCore.terry_quotes.txt")));
        }

        public static void UseTerryPratchett(this IApplicationBuilder app, IQuoteParser quoteParser, string filename)
        {
            app.UseMiddleware(typeof(PratchettCoreMiddleware),
                new PratchettQuotesFactory(quoteParser, () => new FileStream(filename, FileMode.Open, FileAccess.Read)));
        }

        public static void UseTerryPratchett(this IApplicationBuilder app, IQuoteParser quoteParser, Func<Stream> quotesProvider)
        {
            app.UseMiddleware(typeof(PratchettCoreMiddleware), new PratchettQuotesFactory(quoteParser, quotesProvider));
        }
    }
}
