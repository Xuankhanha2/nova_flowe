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
    /// created by: KhanhVX
    /// created date: 6/5/2022
    /// Lớp điều khiển của hóa đơn
    /// </summary>
    [Route("api/v1.0/[controller]s")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService orderService;
        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }

        /// <summary>
        /// created date: 6/5/2022
        /// created by: VXKHANH
        /// Trả về toàn bộ các bản ghi
        /// </summary>
        /// <typeparam name="Order"></typeparam>
        /// <returns>Danh sách các bản ghi</returns>
        [HttpGet]
        public virtual IActionResult getAll(int? pageNumber, int? items)
        {
            ServiceResult result = orderService.getAll<Order>(pageNumber, items);
            if (result.code == statusCode.exception)
                return StatusCode(500, result);
            if (result.code == statusCode.success)
                return StatusCode(200, result.data);
            else
                return StatusCode(204, result.data);
        }

        /// <summary>
        /// created date: 6/5/2022
        /// created by: VXKHANH
        /// Lấy thôn tin hóa đơn theo id
        /// </summary>
        /// <typeparam name="orderId">orderId</typeparam>
        [HttpGet("{orderId}")]
        public virtual IActionResult getById(Guid orderId)
        {
            ServiceResult result = orderService.getById<Order>(orderId);
            if (result.code == statusCode.exception)
                return StatusCode(500, result);
            if (result.code == statusCode.success)
                return StatusCode(200, result.data);
            else
                return StatusCode(204, result.data);
        }

        /// <summary>
        /// created date: 6/5/2022
        /// created by: VXKHANH
        /// 
        /// </summary>
        /// <typeparam name="orderId">orderId</typeparam>
        /// <typeparam name="customerId">customerId</typeparam>
        [HttpGet("ordersOfCustomer")]
        public virtual IActionResult getOrderOfCustomer(Guid orderId, Guid customerId)
        {
            ServiceResult result = orderService.getOrdersOfCustomer(orderId, customerId);
            if (result.code == statusCode.exception)
                return StatusCode(500, result);
            if (result.code == statusCode.success)
                return StatusCode(200, result.data);
            else
                return StatusCode(204, result.data);
        }

        /// <summary>
        /// created by: KhanhVX
        /// created date: 6/5/2022
        /// Lấy danh sách các hóa đơn thuộc về khách hàng
        /// </summary>
        /// <returns></returns>
        [HttpGet("customerId/{customerId}")]
        public IActionResult getOrderByCustomerId(Guid customerId)
        {
            ServiceResult result = orderService.getByCustomerId(customerId);
            if (result.code == statusCode.exception)
                return StatusCode(500, result);
            if (result.code == statusCode.success)
                return StatusCode(200, result.data);
            else
                return StatusCode(204, result.data);
        }


        /// <summary>
        /// created date: 6/5/2022
        /// created by: VXKHANH
        /// Thêm bản ghi mới với dữ liệu được láy từ bodyRequest
        /// </summary>
        [HttpPost]
        public virtual IActionResult post(Order order)
        {
            ServiceResult result = orderService.insertOrder(order);
            if (result.code == statusCode.exception)
                return StatusCode(500, result);
            if (result.code == statusCode.success)
                return StatusCode(201, result.data);
            if (result.code == statusCode.fail)
                return StatusCode(200, result.data);
            else
                return StatusCode(400, result);
        }

        /// <summary>
        /// created date: 6/5/2022
        /// created by: VXKHANH
        /// Sửa bản ghi với dữ liêu được lấy tử bodyRequest
        /// </summary>
        /// <param name="papram">OrderDetail</param>
        [HttpPut]
        public virtual IActionResult put(OrderDetail papram)
        {
            ServiceResult result = orderService.updateOrder(papram);
            if (result.code == statusCode.exception)
                return StatusCode(500, result);
            if (result.code == statusCode.success)
                return StatusCode(201, result.data);
            if (result.code == statusCode.fail)
                return StatusCode(200, result.data);
            else
                return StatusCode(400, result);
        }

        /// <summary>
        /// created date: 6/5/2022
        /// created by: VXKHANH
        /// Xóa 1 bản ghi theo id được truyền vào
        /// </summary>
        /// <param name="id">Id</param>
        [HttpDelete("{id}")]
        public virtual IActionResult deleteOrder(Guid id)
        {
            ServiceResult result = orderService.deleteOrder(id);
            if (result.code == statusCode.success)
                return StatusCode(200, result.data);
            if (result.code == statusCode.fail)
                return StatusCode(400, result);
            else
                return StatusCode(500, result);
        }

        /// <summary>
        /// created date: 10/5/2022
        /// created by: KhanhVX
        /// Xoá dữ liệu 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("orderDetail/{id}")]
        public IActionResult deleteOrderDetail(Guid id)
        {
            ServiceResult result = orderService.deleteOrderDetail(id);
            if (result.code == statusCode.success)
                return StatusCode(200, result.data);
            if (result.code == statusCode.fail)
                return StatusCode(400, result);
            else
                return StatusCode(500, result);
        }
    }
}
