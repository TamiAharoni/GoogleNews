using DAL.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions;
using System.Xml.Linq;

namespace DAL
{
    public class GoogleNewsDAL
    {
        private const string RssUrl = "http://news.google.com/news?pz=1&cf=all&ned=en_il&hl=en&output=rss";
        private readonly IMemoryCache _cache;
        private const string CacheKey = "GoogleNewsFeed";

        /// <summary>
        /// c'tor dfinition memoryCache.
        /// </summary>
        /// <param name="memoryCache"></param>
        public GoogleNewsDAL(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        /// <summary>
        /// Function that returned all data from cache.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Item>?> GetAllNews()
        {
            //The data in cache
            if (_cache.TryGetValue(CacheKey, out IEnumerable<Item> cachedNews))
            {
                return cachedNews;
            }

            //The data is'nt cache.
            try
            {
                cachedNews = await this.fetchNews();
                return cachedNews;
            }
            catch (Exception ex)
            {
                //print exception to log file.
                LogUtility.AddToLog("exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Save data in cache.
        /// The function Makes external API calls
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// The function returned one Item acorrding to title.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
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
                //print exception to log file.
                LogUtility.AddToLog("exception: "+ ex.Message);
                return null;
            }
        }
    }
}
