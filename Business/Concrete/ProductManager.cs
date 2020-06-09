using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal productDal;

        public ProductManager(IProductDal productDal)
        {
            this.productDal = productDal;
        }

        public List<Product> GetAll()
        {
            return productDal.GetList();
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return productDal.GetList(filter: p => p.CategoryId == categoryId);
        }

        public Product GetById(int productId)
        {
            return productDal.Get(p => p.ProductId == productId);
        }
    }
}
