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
    }
}