//using HolyShong.Models.HolyShongModel;
//using HolyShong.Repositories;
//using HolyShong.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace HolyShong.Services
//{
//    public class RestaurantService
//    {
//        //初始
//        private readonly HolyShongRepository _storecategoryRespository;
//        private readonly HolyShongRepository _storeRespository;
//        private readonly HolyShongRepository _productcategoryRespository;
//        private readonly HolyShongRepository _productRespository;
//        private readonly HolyShongRepository _ScoreRespository;
//        private readonly HolyShongRepository _BusinesshoursRespository;

//        public RestaurantService()
//        {
//            _storecategoryRespository = new HolyShongRepository();
//            _storeRespository = new HolyShongRepository();
//            _productcategoryRespository = new HolyShongRepository();
//            _productRespository = new HolyShongRepository();
//            _ScoreRespository = new HolyShongRepository();
//            _BusinesshoursRespository = new HolyShongRepository();
//        }
//        public RestaurantViewModel GetRestaurant(int? id)
//        {
//            var result = new RestaurantViewModel
//            {
//                ProductAreas = new List<ProductArea>()
//            };
//            //店家
//            var store = _storeRespository.GetAll<Store>().FirstOrDefault((x) => x.StoreId == id);
//            if (store == null)
//            {
//                return result;
//            }

//            result.StoreId = store.StoreId;
//            result.StoreName = store.Name;
//            result.StorePicture = store.Img;
//            result.StoreAddress = store.Address;

//            //商店分類
//            result.StoreCategoryName = _storecategoryRespository.GetAll<StoreCategory>().First(x => x.StoreCategoryId == store.StoreCategoryId).Name;

//            //評分
//            result.Score = _ScoreRespository.GetAll<Score>().Where(x => x.ScoreId == store.StoreId).Average(x => (decimal?)x.ScorePoint) == null ? 0 : _ScoreRespository.GetAll<Score>().Where(x => x.ScoreId == store.StoreId).Average(x => x.ScorePoint);


//            //開店時間&營業時間
//            List<Businesshour> BusinesshourList = new List<Businesshour>();
//            var businessHours = _BusinesshoursRespository.GetAll<Businesshours>().Where(x => x.StoreId == store.StoreId);

//            DateTime dt = DateTime.Now;
//            int week = dt.DayOfWeek.GetHashCode();
//            //把7筆抓出來(一~日)
//            TimeSpan todayOpening = new TimeSpan();

//            foreach (var item in businessHours)
//            {
//                Businesshour Businesshour = new Businesshour();

//                Businesshour.WeekDay = convertToChinese(item.WeekDay.ToString()) ;
//                Businesshour.OpenTime = item.OpenTime.ToString(@"hh\:mm");
//                Businesshour.CloseTime = item.CloseTime.ToString(@"hh\:mm");
//                BusinesshourList.Add(Businesshour);
//                if (week == item.WeekDay)
//                {
//                    todayOpening = item.OpenTime;
//                }

//            }

//            result.BusinesshourList = BusinesshourList;//所有營業時間&日期
//            result.todayOpening = todayOpening.ToString(@"hh\:mm");//去秒數

//            //專區
//            //1.找出所有產品類別(不用做)
//            var productcategories = _productcategoryRespository.GetAll<ProductCategory>().Where(x => x.StoreId == id).ToList();
//            //2.找出所有類別下面的所有產品
//            var products = _productRespository.GetAll<Product>().ToList();
//            //3.依產品類別將產品分類後再全部取出
//            foreach (var item in productcategories)
//            {
//                //這個產品類別(ProductsCategory)的產品(Product)全部挑出來
//                var temp = products.Where(x => x.ProductCategoryId == item.ProductCategoryId);
//                //存成ProductCards
//                var cards = new List<ProductCard>();
//                foreach (var product in temp)
//                {
//                    var card = new ProductCard
//                    {
//                        ProductName = product.Name,
//                        Img = product.Img,
//                        UnitPrice = product.UnitPrice,
//                        Description = product.Description                        
//                    };
//                    cards.Add(card);
//                }
//                var area = new ProductArea
//                {
//                    ProductCategoryName = item.Name,
//                    ProductCards = cards
//                };
//                result.ProductAreas.Add(area);
//            }

    



//            ////產品類別(錨點)
//            //ProductArea ProductArea = new ProductArea();
//            //var productCategories = _repo.GetAll<ProductCategory>().Where((x) => x.StoreId == store.StoreId);
//            ////2類
//            //foreach (var item in productCategories)
//            //{
//            //    ProductArea.ProductCategoryName = item.Name;
//            //}

//            //var products = _repo.GetAll<Product>().Where(x => productCategories.Select(y => y.ProductCategoryId).Contains(x.ProductCategoryId)).GroupBy(x => x.ProductCategoryId);




//            //foreach (var item in products)
//            //{
//            //    List<ProductCard> ProductsList = new List<ProductCard>();
//            //    foreach (var item1 in item)
//            //    {
//            //        ProductCard ProductCard = new ProductCard();
//            //        ProductCard.ProductName = item1.Name;
//            //        ProductCard.UnitPrice = item1.UnitPrice;
//            //        ProductCard.Img = item1.Img;
//            //        ProductsList.Add(ProductCard);
//            //    }
//            //    ProductAreaList
//            //}

//            //產品(productCategories是集合)












//            return result;
//        }
        
//        public string convertToChinese(string week)
//        {
//            string chinese = week.ToString().Replace("1", "一").Replace("2", "二").Replace("3", "三")
//                .Replace("4", "四").Replace("5", "五").Replace("6", "六").Replace("7", "七").Replace("0", "日");
//            return chinese;
//        }
//    }
//}