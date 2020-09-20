using Application.Interfaces;
using Domain.Models;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
  public  class UnitOfWork:IUnitOfWork
    {
        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        //
        private GRepository<Category> _categories;
        private GRepository<SubCategory> _subCategories;
        private GRepository<SubCategoryItem> _subCaregoryItems;

        //



        //
        public IGRepository<Category> Categories
        {
            get
            {
                return _categories ??
                    (_categories = new GRepository<Category>(_context));
            }
        }

        public IGRepository<SubCategory> SubCategories
        {
            get
            {
                return _subCategories ??
                    (_subCategories = new GRepository<SubCategory>(_context));
            }
        }

        public IGRepository<SubCategoryItem> SubCategoryItems
        {
            get
            {
                return _subCaregoryItems ??
                    (_subCaregoryItems = new GRepository<SubCategoryItem>(_context));
            }
        }

        //

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();

        }
    }
}
