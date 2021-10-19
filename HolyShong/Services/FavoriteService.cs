using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Services
{
    public class FavoriteService
    {
        private readonly HolyShongRepository _repo;
        public FavoriteService()
        {
            _repo = new HolyShongRepository();
        }
        public FavoriteViewModel GetFavorite(int memberId)
        {
            var result = new FavoriteViewModel();

            var member = _repo.GetAll<Member>().FirstOrDefault(m => m.MemberId == memberId);
            var favorite = _repo.GetAll<Favorite>().Where(f => f.MemberId == memberId);
            var store = _repo.GetAll<Store>().Where(s => favorite.Select(f => f.StoreId).Contains(s.StoreId));
            //store.OrderByDescending(s => favorite.Select(f => f.CreateTime));



            result.favoriteStores = new List<FavoriteStore>();

            foreach (var s in store)
            {
                var sTemp = new FavoriteStore()
                {
                    StoreImg = s.Img,
                    StoreName = s.Name
                };
                result.favoriteStores.Add(sTemp);
            }
            return result;
        }
    }
}