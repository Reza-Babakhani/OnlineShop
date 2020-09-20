using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
   public interface IUnitOfWork:IDisposable
    {
        IGRepository<Category> Categories { get; }
        IGRepository<SubCategory> SubCategories { get; }
        IGRepository<SubCategoryItem> SubCategoryItems { get; }

        Task Commit();
    }
}
