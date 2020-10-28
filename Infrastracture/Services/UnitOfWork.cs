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
    public class UnitOfWork : IUnitOfWork
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

        private GRepository<Product> _products;
        private GRepository<ProductOptions> _productOptions;
        private GRepository<ProductDetails> _productDetails;
        private GRepository<ProductImages> _productImages;
        private GRepository<ProductPrices> _productPrices;
        private GRepository<Brand> _brands;
        private GRepository<Inventory> _inventories;
        private GRepository<ProductSpecification> _productSpecification;
        private GRepository<ProductStrengths> _productStrengths;
        private GRepository<ProductWeaknesses> _productWeaknesses;
        private GRepository<Warranty> _warranties;
        private GRepository<SpecialReview> _specialReview;

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
          


        public IGRepository<Product> Products
        {
            get
            {
                return _products ??
                    (_products = new GRepository<Product>(_context));
            }
        }
        public IGRepository<ProductDetails> ProductDetails
        {
            get
            {
                return _productDetails ??
                    (_productDetails = new GRepository<ProductDetails>(_context));
            }
        }
        public IGRepository<ProductImages> ProductImages
        {
            get
            {
                return _productImages ??
                    (_productImages = new GRepository<ProductImages>(_context));
            }
        }
        public IGRepository<ProductOptions> ProductOptions
        {
            get
            {
                return _productOptions ??
                    (_productOptions = new GRepository<ProductOptions>(_context));
            }
        }
        public IGRepository<ProductPrices> ProductPrices
        {
            get
            {
                return _productPrices ??
                    (_productPrices = new GRepository<ProductPrices>(_context));
            }
        }
        public IGRepository<ProductSpecification> ProductSpecification
        {
            get
            {
                return _productSpecification ??
                    (_productSpecification = new GRepository<ProductSpecification>(_context));
            }
        }
        public IGRepository<ProductStrengths> ProductStrengths
        {
            get
            {
                return _productStrengths ??
                    (_productStrengths = new GRepository<ProductStrengths>(_context));
            }
        }
        public IGRepository<ProductWeaknesses> ProductWeaknesses
        {
            get
            {
                return _productWeaknesses ??
                    (_productWeaknesses = new GRepository<ProductWeaknesses>(_context));
            }
        }
        public IGRepository<Brand> Brands
        {
            get
            {
                return _brands ??
                    (_brands = new GRepository<Brand>(_context));
            }
        }
        public IGRepository<Warranty> Warranties
        {
            get
            {
                return _warranties ??
                    (_warranties = new GRepository<Warranty>(_context));
            }
        }
        public IGRepository<Inventory> Inventories
        {
            get
            {
                return _inventories ??
                    (_inventories = new GRepository<Inventory>(_context));
            }
        }
        public IGRepository<SpecialReview> SpecialReviews
        {
            get
            {
                return _specialReview ??
                    (_specialReview = new GRepository<SpecialReview>(_context));
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
