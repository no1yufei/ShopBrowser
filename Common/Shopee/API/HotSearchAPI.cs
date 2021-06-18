using Common.Shopee.API.Data;
using CsharpHttpHelper;
using Newtonsoft.Json;
using ShopeeChat.Shopee.API.Data;
using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
    public partial class ShopeeAPI
    {
        /// <summary>
        /// /api/v2/recommendation/hot_search_words?limit=8&offset=0
        /// </summary>
        /// <param name="region"></param>
        /// <param name="store"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public HotSearchWords GetHotSearchWords(StoreRegion region,int limit,int offset)
        {
            HotSearchWords hsw = null;
            HtmlHttpHelper hhh = new HtmlHttpHelper();
            string msgQuestStr = region.GetBuyerUrl() + String.Format("/api/v2/recommendation/hot_search_words?limit={0}&offset={1}",limit, offset);

            HttpResult result = hhh.Get(msgQuestStr);
            if (result.Header != null)
            {
                Console.Write(result.Html);
                hsw = HotSearchWords.FromJson(result.Html);
            }
            return hsw;
        }
        /// <summary>
        /// /api/v2/recommendation/trending_searches_v2?limit=20&offset=0
        /// </summary>
        /// <param name="region"></param>
        /// <param name="store"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public TrendingSearches GetTrendingSearches(StoreRegion region, int limit, int offset)
        {
            TrendingSearches ts = null;
            HtmlHttpHelper hhh = new HtmlHttpHelper();
            string msgQuestStr = region.GetBuyerUrl() + String.Format("/api/v2/recommendation/trending_searches_v2?limit={0}&offset={1}", limit, offset);
            HttpResult result = hhh.Get(msgQuestStr);
            if (result.Header != null)
            {
                Console.Write(result.Html);
                ts = TrendingSearches.FromJson(result.Html);
            }
            return ts;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="catid"></param>
        /// <param name="itemid"></param>
        /// <param name="shopid"></param>
        /// <param name="recommendType">9,27-similar_products,16-you_may_also_like</param>
        public RecommendProductInfoReponse GetRecommendItems(Store store, int catid,long itemid, long shopid,int recommendType = 27)
        {
            string recommendItemUrl = StoreRegionMap.GetBuyerURL(store.RegionID)
                + String.Format("/api/v2/recommend_items/get?catid={0}&itemid={1}&limit=48&offset=0&recommend_type={2}&shopid={3}", catid, itemid, recommendType, shopid);
            HtmlHttpHelper hhh = new HtmlHttpHelper();
            HttpResult recommend_items_hr = hhh.Get(recommendItemUrl);
            string recommend_items_json = recommend_items_hr.Html;
            RecommendProductInfoReponse dataTemplate = null;
            try
            {
                dataTemplate = JsonConvert.DeserializeObject<RecommendProductInfoReponse>(recommend_items_json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //try
            //{
            //    RecommendProductInfoReponse repose = RecommendProductInfoReponse.FromJson(recommend_items_json);
            //    return repose;
            //}
            //catch (Exception e)
            //{
            //    Console.Write("推荐产品信息收集异常:" + e.Message);
            //}
            return dataTemplate;
        }

        public SearchHintData GetSearchHint(Store store, string keyword)
        {
            //https://xiapi.xiapibuy.com/api/v2/search_hint/get?keyword=%E8%A1%A3%E6%9C%8D&platform=5&search_type=0

            string searchHintUrl = StoreRegionMap.GetBuyerURL(store.RegionID)
                + String.Format("/api/v2/search_hint/get?keyword={0}&platform=5&search_type=0", keyword);
            HtmlHttpHelper hhh = new HtmlHttpHelper();
            HttpResult searchHint_hr = hhh.Get(searchHintUrl);
            string searchHint_json = searchHint_hr.Html;
            try
            {
                SearchHintData data = JsonConvert.DeserializeObject<SearchHintData>(searchHint_json);
                return data;
            }
            catch (Exception e)
            {
                Console.Write("搜索关键词收集异常:" + e.Message);
            }
            return null;
        }
    }
}
