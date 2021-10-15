using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Services
{
    public class ProductService
    {
        private readonly HolyShongRepository _repo;
        public ProductService()
        {
            _repo = new HolyShongRepository();
        }

        public RestaurantViewModel1 GetStore(int storeId)
        {
            #region
            //var result = new RestaurantViewModel1()
            //{
            //    StoreProductCategories = new List<StoreProductCategory>()
            //    {
            //        new StoreProductCategory()
            //        {
            //            StoreProducts = new List<StoreProduct>()
            //            {
            //                new StoreProduct()
            //                {
            //                    StoreProductOptions = new List<StoreProductOption>()
            //                    {
            //                        new StoreProductOption()
            //                        {
            //                            ProductOptionDetails = new List<StoreProductOptionDetail>()
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //};
            #endregion
            var result = new RestaurantViewModel1();
            var store = _repo.GetAll<Store>().FirstOrDefault(s => s.StoreId == storeId);
            var storeCategory = _repo.GetAll<StoreCategory>().FirstOrDefault(sc => sc.StoreCategoryId == store.StoreCategoryId);

            result.StoreId = storeId;
            result.StoreName = store.Name;
            result.StoreImg = store.Img;
            result.StoreAddress = store.Address;
            result.StoreCategoryName = storeCategory.Name;
            result.StoreProductCategories = new List<StoreProductCategory>();
            result.SupplyTimes = new List<SupplyTime>();

            //var supplyTimes = _repo.GetAll<Businesshours>().Where(bh => bh.StoreId == store.StoreId).GroupBy(bh => bh.WeekDay).Select(bh=>bh.Key);
            //var sTimes = _repo.GetAll<Businesshours>().Where(bh => bh.StoreId == store.StoreId);
            //foreach(var st in supplyTimes)
            //{
            //    new SupplyTime()
            //    {
            //        WeekDay=st,
            //        OpenTime = sTimes.Where(s=>s.WeekDay==st)
            //    }
            //}

            var productCategories = _repo.GetAll<ProductCategory>().Where(pc => pc.StoreId == store.StoreId);
            var products = _repo.GetAll<Product>().Where(p => productCategories.Select(pc => pc.ProductCategoryId).Contains(p.ProductCategoryId));
            var productOption = _repo.GetAll<ProductOption>().Where(po => products.Select(p => p.ProductId).Contains(po.ProductId));
            var productOptionDetail = _repo.GetAll<ProductOptionDetail>().Where(pod => productOption.Select(po => po.ProductOptionId).Contains(pod.ProductOptionId)).ToList();
            //錨點區塊
            foreach (var pc in productCategories.ToList())
            {
                var pcTemp = new StoreProductCategory()
                {
                    StoreProductCategoryName = pc.Name,
                    StoreProducts = new List<StoreProduct>()

                };
                foreach (var p in products.ToList())
                {
                    var pTemp = new StoreProduct()
                    {
                        ProductId = p.ProductId,
                        ProductName = p.Name,
                        ProductDescription = p.Description,
                        UnitPrice = p.UnitPrice,
                        ProductImg = p.Img,
                        StoreProductOptions = new List<StoreProductOption>()
                    };
                    foreach (var po in productOption.ToList())
                    {
                        var poTemp = new StoreProductOption()
                        {
                            ProductOptionName = po.Name,
                            ProductOptionDetails = new List<StoreProductOptionDetail>()
                        };

                        foreach (var pod in productOptionDetail.ToList())
                        {
                            var podTemp = new StoreProductOptionDetail()
                            {
                                StoreProductOptioinDetailName = pod.Name
                            };
                            poTemp.ProductOptionDetails.Add(podTemp);
                        }
                        pTemp.StoreProductOptions.Add(poTemp);
                    }
                    pcTemp.StoreProducts.Add(pTemp);
                }
                result.StoreProductCategories.Add(pcTemp);
            }


            return result;
        }

        public StoreProduct GetStoreProductByProductId(int productId)
        {
            var result = new StoreProduct();

            var p = _repo.GetAll<Product>().FirstOrDefault(x => x.ProductId == productId);

            if (p is null)
            {
                return result;
            }

            var productOptionSource = _repo.GetAll<ProductOption>().Where(po => po.ProductId == productId);
            var productOptionDetail = _repo.GetAll<ProductOptionDetail>().Where(pod => productOptionSource.Select(po => po.ProductOptionId).Contains(pod.ProductOptionId)).ToList();
            var productOption = productOptionSource.ToList();


            result.ProductId = p.ProductId;
            result.ProductId = p.ProductId;
            result.ProductName = p.Name;
            result.ProductDescription = p.Description;
            result.UnitPrice = p.UnitPrice;
            result.ProductImg = p.Img;
            result.StoreProductOptions = new List<StoreProductOption>();

            foreach (var po in productOption.ToList())
            {
                var poTemp = new StoreProductOption()
                {
                    ProductOptionName = po.Name,
                    ProductOptionDetails = new List<StoreProductOptionDetail>()
                };

                foreach (var pod in productOptionDetail.Where(x => x.ProductOptionId == po.ProductOptionId).ToList())
                {
                    var podTemp = new StoreProductOptionDetail()
                    {
                        StoreProductOptioinDetailName = pod.Name,
                        StoreProductOptionDetailId = pod.ProductOptionDetailId
                    };
                    poTemp.ProductOptionDetails.Add(podTemp);
                }
                result.StoreProductOptions.Add(poTemp);
            }

            return result;
        }
    }
}