using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// Created date: 17/07/2021
    /// Created by: VXKHANH
    /// </summary>
    public class ProductController : BaseController<Product>
    {
        IProductService productService;
        public ProductController(IProductService _productService): base(_productService)
        {
            productService = _productService;
        }

        /// <summary>
        /// created date: 12/11/2021
        /// created by: vxkhanh
        /// Hàm lấy danh sách sản phẩm theo danh mục sản phẩm
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>IActionResult</returns>
        [HttpGet("categoryId/{categoryId}")]
        public IActionResult getProductsByCategory(Guid categoryId,[FromQuery]int? order)
        {
            ServiceResult result = productService.getProductsByCategory(categoryId, order);
            if (result.code == statusCode.success)
            {
                return StatusCode(200, result.data);
            }
            else if (result.code == statusCode.noContent)
            {
                return StatusCode(204, result.data);
            }
            else
            {
                return StatusCode(500, result);
            }
        }

        /// <summary>
        /// created date: 15/11/2021
        /// created by: vxkhanh
        /// Hàm tìm kiếm sản phẩm theo từ khóa
        /// </summary>
        /// <param name="key">Từ khóa</param>
        /// <returns>IActionResult</returns>
        [HttpGet("search")]
        public IActionResult searchProduct([FromQuery]string key)
        {
            ServiceResult result = productService.searchProduct(key);
            if(result.code == statusCode.success)
            {
                return StatusCode(200, result.data);
            }
            else if (result.code == statusCode.noContent)
            {
                return StatusCode(204, result.data);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
    }
}
