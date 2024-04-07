using DAL.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Xml.Linq;

namespace DAL
{
    public class GoogleNewsDAL
    {
        private const string RssUrl = "http://news.google.com/news?pz=1&cf=all&ned=en_il&hl=en&output=rss";
        private readonly IMemoryCache _cache;
        private const string CacheKey = "GoogleNewsFeed";

        /// <summary>
        /// c'tor  
        /// </summary>
        /// <param name="memoryCache"></param>
        public GoogleNewsDAL() {}

        public GoogleNewsDAL(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        public async Task<IEnumerable<Item>?> GetAllNews()
        {
            if (_cache.TryGetValue(CacheKey, out IEnumerable<Item> cachedNews))
            {
                return cachedNews;
            }

            try
            {
                cachedNews = await this.fetchNews();
                return cachedNews;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        private async Task<IEnumerable<Item>> fetchNews()
        {

            using (var httpClient = new HttpClient())
            {
                //Get the data fron RSS API.
                var rssData = await httpClient.GetStringAsync(RssUrl);

                //Parse to XML.
                var xmlDoc = XDocument.Parse(rssData);

                //Read XML and convert to Item Objects list.
                var newsList = new List<Item>();
                foreach (var item in xmlDoc.Descendants("item"))
                {
                    var newsItem = new Item
                    {
                        id = item.Element("guid")?.Value,
                        title = item.Element("title")?.Value,
                        body = item.Element("description")?.Value,
                        link = item.Element("link")?.Value,
                        date = item.Element("pubDate")?.Value.Substring(0, (int)(item.Element("pubDate")?.Value.Length - 7))

                    };
                    newsList.Add(newsItem);
                }

                //The definition of cache.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(CacheKey, newsList, cacheEntryOptions);
                return newsList;
            }
        }

        public async Task<Item>? GetItem(string title)
        {
            try
            {
                // Fetch all news items
                var allNews = await GetAllNews();

                // Find the news item with the specified id
                var item = allNews?.FirstOrDefault(item => item.title == title);

                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
