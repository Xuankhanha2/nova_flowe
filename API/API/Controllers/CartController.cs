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
    /// created date: 02/11/21
    /// created by: vxkhanh
    /// </summary>
    [Route("api/v1.0/[controller]s")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartService cartService;
        public CartController(ICartService _cartService)
        {
            cartService = _cartService;
        }

        /// <summary>
        /// created date: 02/11/2021
        /// created by: vxkhanh
        /// Hàm điều khiển - lấy danh sách giỏ hàng theo id khách hàng
        /// </summary>
        /// <param name="customerId">id khách hàng</param>
        /// <returns>Service result</returns>
        [HttpGet("customerId/{customerId}")]
        public IActionResult getCartsByCustomerId(Guid customerId)
        {
            ServiceResult result = cartService.getCartsByCustomerId(customerId);
            if (result.code == statusCode.exception)
                return StatusCode(500, result);
            if (result.code == statusCode.success)
                return StatusCode(200, result.data);
            else
                return StatusCode(204, result.data);
        }

        [HttpGet()]
        public IActionResult getExistCart(Guid customerId, Guid productId)
        {
            ServiceResult result = cartService.getExistsCart(customerId, productId);
            if (result.code == statusCode.exception)
                return StatusCode(500, result);
            if (result.code == statusCode.success)
                return StatusCode(200, result.data);
            else
                return StatusCode(204, result.data);
        }

        [HttpPost]
        public IActionResult post(Cart papram)
        {
            ServiceResult result = cartService.insert<Cart>(papram);
            if (result.code == statusCode.exception)
                return StatusCode(500, result.data);
            if (result.code == statusCode.success)
                return StatusCode(201, result.data);
            if (result.code == statusCode.fail)
                return StatusCode(200, result.data);
            else
                return StatusCode(400, result.data);
        }

        /// <summary>
        /// created date: 20/06/2021
        /// created by: VXKHANH
        /// Sửa bản ghi với dữ liêu được lấy tử bodyRequest
        /// </summary>
        /// <param name="id">Id</param>
        [HttpPut]
        public IActionResult put(Cart papram)
        {
            ServiceResult result = cartService.update<Cart>(papram);
            if (result.code == statusCode.exception)
                return StatusCode(500, result.data);
            if (result.code == statusCode.success)
                return StatusCode(201, result.data);
            if (result.code == statusCode.fail)
                return StatusCode(200, result.data);
            else
                return StatusCode(400, result.data);
        }

        /// <summary>
        /// created date: 20/06/2021
        /// created by: VXKHANH
        /// Xóa 1 bản ghi theo id được truyền vào
        /// </summary>
        /// <param name="id">Id</param>
        [HttpDelete("{id}")]
        public IActionResult delete(Guid id)
        {
            ServiceResult result = cartService.delete<Cart>(id);
            if (result.code == statusCode.success)
                return StatusCode(200, result.data);
            if (result.code == statusCode.fail)
                return StatusCode(400, result.data);
            else
                return StatusCode(500, result.data);
        }
    }
}
