using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    /// <summary>
    /// Created date: 20/7/2021
    /// Created by: VXKHANH
    /// </summary>
    public class ProductService:BaseService, IProductService
    {
        IProductRepository productRepository;
        public ProductService(IProductRepository _productRepository) : base(_productRepository)
        {
            productRepository = _productRepository;
        }

        /// <summary>
        /// created by: vxkhanh
        /// created date 12/11/2011
        /// Hàm lấy danh sách sản phẩm theo danh mục sản phẩm
        /// </summary>
        /// <param name="categoryId">categoryId</param>
        /// <returns></returns>
        public ServiceResult getProductsByCategory(Guid categoryId, int? orderVal)
        {
            try
            {
                //gọi hàm lấy danh sách sản phẩm
                var list = productRepository.getProductsByCategory(categoryId, orderVal);
                if (list != null)
                {
                    serviceResult.isValid = true;
                    serviceResult.data = list;
                    serviceResult.code = statusCode.success;
                }
                else
                {
                    serviceResult.isValid = true;
                    serviceResult.data = list;
                    serviceResult.code = statusCode.noContent;
                }
            }
            catch (Exception e)
            {
                serviceResult.isValid = false;
                serviceResult.data = null;
                serviceResult.message = e.Message;
                serviceResult.code = statusCode.exception;
            }
            return serviceResult;
        }

        public ServiceResult searchProduct(string key)
        {
            try
            {
                //gọi hàm lấy danh sách sản phẩm
                var list = productRepository.searchProduct(key);
                if (list != null)
                {
                    serviceResult.isValid = true;
                    serviceResult.data = list;
                    serviceResult.code = statusCode.success;
                }
                else
                {
                    serviceResult.isValid = true;
                    serviceResult.data = list;
                    serviceResult.code = statusCode.noContent;
                }
            }
            catch (Exception e)
            {
                serviceResult.isValid = false;
                serviceResult.data = null;
                serviceResult.message = e.Message;
                serviceResult.code = statusCode.exception;
            }
            return serviceResult;
        }
    }
}
