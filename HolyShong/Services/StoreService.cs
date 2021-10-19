using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.ViewModels;
using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;

namespace HolyShong.Services
{
    public class StoreService
    {
        //初始相依性注入
        private readonly HolyShongRepository _repo;
        public StoreService()
        {
            _repo = new HolyShongRepository();
        }
        //Home_找出所有店家
        public HomeViewModel GetAllStores()
        {
            var result = new HomeViewModel
            {
                StoreCardBlocks = new List<StoreCardBlock>()
            };
            //1.找出所有商店主分類(不用做)
            var storecategories = _repo.GetAll<Models.HolyShongModel.StoreCategory>().ToList();
            //2.找出所有店家
            var stores = _repo.GetAll<Store>().ToList();
            //3.依商店主分類將店家分類後再全部取出
            foreach (var item in storecategories)
            {
                //3.1這個主分類的商店全部挑出來
                var temp = stores.Where(x => x.StoreCategoryId == item.StoreCategoryId);

                //3.2存成StoreCards
                var cards = new List<StoreCard>();
                foreach (var store in temp)
                {
                    var card = new StoreCard
                    {
                        StoreId = store.StoreId,
                        StoreImg = store.Img,
                        StoreName = store.Name
                    };
                    cards.Add(card);
                }

                //3.3存成StoreBlock
                var block = new StoreCardBlock
                {
                    StoreCategoryId = item.StoreCategoryId,
                    StoreCategoryImg = item.Img,
                    StoreCategoryName = item.Name,
                    StoreCards = cards
                };
                result.StoreCardBlocks.Add(block);
            }
            return result;
        }

        //Search_主分類搜尋
        public StoreCategorySearchViewModel GetAllStoresByStoreCategoryId(int storecategoryId)
        {
            var result = new StoreCategorySearchViewModel
            {
                StoreCards = new List<StoreCard>()
            };
            //1.找到特定的StoreCategory
            var storecategory = _repo.GetAll<Models.HolyShongModel.StoreCategory>().FirstOrDefault(x => x.StoreCategoryId == storecategoryId);

            if (storecategory == null)
            {
                //沒找到
            }
            //2.找到這個StoreVategory的所有Store
            var stores = _repo.GetAll<Store>().Where(x => x.StoreCategoryId == storecategoryId).ToList();
            var cards = new List<StoreCard>();
            foreach (var item in stores)
            {
                var temp = new StoreCard()
                {
                    StoreId = item.StoreId,
                    StoreImg = item.Img,
                    StoreName = item.Name
                };
                cards.Add(temp);
            }
            result.StoreCategoryId = storecategory.StoreCategoryId;
            result.StoreCategoryName = storecategory.Name;
            result.StoreCards = cards;
            return result;
        }

        //Todo 使用關鍵字搜尋會有問題(待處理)
        //Search_副分類搜尋
        public SearchViewModel GetAllStoresByRequest(SearchRequest input)
        {
            var result = new SearchViewModel
            {
                StoreCards = new List<StoreCard>()
            };
            //一.關鍵字
            var stores = _repo.GetAll<Store>().Where(x => x.KeyWord.Contains(input.Keyword)).ToList();
            if (stores.Count == 0)
            {
                return result;
            }

            //二.價格範圍
            //1.找出所有商店、所有產品類別及所有產品
            var allProductCategories = _repo.GetAll<ProductCategory>();
            var allProducts = _repo.GetAll<Product>();
            var cards = new List<StoreCard>();
            //2.計算商店價格
            foreach (var item in stores)
            {
                var productCategoryAveragePrice = new List<decimal>();
                var storePrice = "";
                //2.1找出每家店本身的產品類別
                var productCategoryList = allProductCategories.Where(x => x.StoreId == item.StoreId);
                //2.2找出目前選擇的商店每個產品類別下面的所有產品的平均價格
                foreach (var productCategory in productCategoryList)
                {
                    var productAveragePrice = allProducts.Where(x => x.ProductCategoryId == productCategory.ProductCategoryId).Select(x => x.UnitPrice).ToList();
                    var averagePrive = productAveragePrice.Count == 0 ? 0 : productAveragePrice.Average();
                    productCategoryAveragePrice.Add(averagePrive);
                }
                var storeAveragePrice = productCategoryAveragePrice.Count == 0 ? 0 : productCategoryAveragePrice.Average();
                //2.3轉換range
                if (storeAveragePrice >= 0 && storeAveragePrice <= 100)
                {
                    storePrice = "0-100";
                }
                else if (storeAveragePrice > 100 && storeAveragePrice <= 200)
                {
                    storePrice = "100-200";
                }
                else if (storeAveragePrice > 200 && storeAveragePrice <= 500)
                {
                    storePrice = "200-500";
                }
                else
                {
                    storePrice = "500-99999";
                }

                if (storePrice == input.Price)
                {
                    //儲存卡片
                    var card = new StoreCard
                    {
                        StoreId = item.StoreId,
                        StoreImg = item.Img,
                        StoreName = item.Name,
                        StoreAveragePrice = storePrice,
                    };
                    cards.Add(card);
                }
            }
            result.StoreCards = cards;
            return result;
        }

        //參考保留關鍵字搜尋
        //public SearchViewModel GetAllStoresByKeyword(string keyword)
        //{
        //    var result = new SearchViewModel
        //    {
        //        StoreCards = new List<StoreCard>()
        //    };
        //    var stores = _repo.GetAll<Store>().Where(x => x.KeyWord.Contains(keyword)).ToList();
        //    if (stores.Count == 0)
        //    {
        //        return result;
        //    }

        //    var cards = new List<StoreCard>();
        //    foreach (var item in stores)
        //    {
        //        var temp = new StoreCard()
        //        {
        //            StoreId = item.StoreId,
        //            StoreImg = item.Img,
        //            StoreName = item.Name
        //        };
        //        cards.Add(temp);
        //    }
        //    result.StoreCards = cards;
        //    return result;
        //}


        ////參考保留店家價格搜尋
        //public SearchViewModel GetAllStoresByPrice(string price)
        //{
        //    //店家卡片初始設定
        //    var result = new SearchViewModel
        //    {
        //        StoreCards = new List<StoreCard>()
        //    };
        //    var stores = _repo.GetAll<Store>().ToList();

        //    if (price == null)
        //    {
        //        return result;
        //    }

        //    //1.找出所有商店、所有產品類別及所有產品
        //    var allProductCategories = _repo.GetAll<ProductCategory>();
        //    var allProducts = _repo.GetAll<Product>();
        //    var cards = new List<StoreCard>();
        //    //2.計算商店價格
        //    foreach (var item in stores)
        //    {
        //        var productCategoryAveragePrice = new List<decimal>();
        //        var storePrice = "";
        //        //2.1找出每家店本身的產品類別
        //        var productCategoryList = allProductCategories.Where(x => x.StoreId == item.StoreId);
        //        //2.2找出目前選擇的商店每個產品類別下面的所有產品的平均價格
        //        foreach (var productCategory in productCategoryList)
        //        {
        //            var productAveragePrice = allProducts.Where(x => x.ProductCategoryId == productCategory.ProductCategoryId).Select(x => x.UnitPrice).ToList();
        //            var averagePrive = productAveragePrice.Count == 0 ? 0 : productAveragePrice.Average();
        //            productCategoryAveragePrice.Add(averagePrive);
        //        }
        //        var storeAveragePrice = productCategoryAveragePrice.Count == 0 ? 0 : productCategoryAveragePrice.Average();
        //        //2.3轉換range
        //        if (storeAveragePrice >= 0 && storeAveragePrice <= 100)
        //        {
        //            storePrice = "0-100";
        //        }
        //        else if (storeAveragePrice > 100 && storeAveragePrice <= 200)
        //        {
        //            storePrice = "100-200";
        //        }
        //        else if (storeAveragePrice > 200 && storeAveragePrice <= 500)
        //        {
        //            storePrice = "200-500";
        //        }
        //        else
        //        {
        //            storePrice = "500-99999";
        //        }

        //        if (storePrice == price)
        //        {
        //            var card = new StoreCard
        //            {
        //                StoreId = item.StoreId,
        //                StoreImg = item.Img,
        //                StoreName = item.Name,
        //                StoreAveragePrice = storePrice,
        //            };
        //            cards.Add(card);
        //        }
        //    }
        //    result.StoreCards = cards;
        //    return result;
        //}
    }
}