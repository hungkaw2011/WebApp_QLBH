using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        CategoryRepository Category { get; }
        CoverTypeRepository CoverType { get; }
        void Save();
    }
}
