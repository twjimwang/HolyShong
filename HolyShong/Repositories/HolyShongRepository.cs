using System.Data.Entity;
using HolyShong.Models.HolyShongModel;
using System.Linq;

namespace HolyShong.Repositories
{
    public class HolyShongRepository
    {
        private readonly HolyShongContext _context;

        public HolyShongRepository()
        {
            //if (context == null)
            //{
            //    throw new ArgumentNullException();
            //}
            _context = new HolyShongContext();
        }

        public void Create<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Added;
        }
        public void Update<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Modified;
        }
        public void Delete<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Deleted;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }
    }
}