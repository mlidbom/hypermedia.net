using Newtonsoft.Json;
using TypeLite;

namespace Composable.Hypermedia
{
    [TsClass]
    public class Link<TResource>
    {
        public Link(string url)
        {
            Url = url;
        }

        [JsonProperty]
        public string Url { get; private set; }
    }
}