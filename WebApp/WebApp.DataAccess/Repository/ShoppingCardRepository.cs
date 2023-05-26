using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.DataAccess.Repository.IRepository;
using WebApp.Models;

namespace WebApp.DataAccess.Repository
{
    public class ShoppingCardRepository : Repository<ShoppingCard>, IShoppingCardRepository
    {
        private readonly ApplicationDbContext _db;

        public ShoppingCardRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCard obj)
        {
            _db.ShoppingCards.Update(obj);
        }
    }
}
