using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    /// <summary>
    /// created by: khanhvx
    /// created date: 6/5/2022
    /// Lớp service của hóa đơn
    /// </summary>
    public interface IOrderService: IBaseService
    {
        /// <summary>
        /// created date: 6/5/2022
        /// created by: KhanhVX
        /// Hàm lấy thông tin hóa đơn theo orderId và customerId
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public ServiceResult getOrdersOfCustomer(Guid orderId, Guid customerId);

        /// <summary>
        /// created date: 6/5/2022
        /// created by: KHANHVX
        /// Lấy danh sách hóa đơn theo mã khách hàng
        /// </summary>
        /// <param name="customerId">customerId</param>
        /// <returns>ServiceResult</returns>
        public ServiceResult getByCustomerId(Guid customerId);

        /// <summary>
        /// created by: KhanhVX
        /// created date: 7/5/2022
        /// Hàm thêm mới hóa đơn
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ServiceResult insertOrder(Order order);

        /// <summary>
        /// created by: KhanhVX
        /// created date: 10/5/2022
        /// Hàm cập nhật thông tin hóa đơn
        /// </summary>
        /// <param name="orderDetail">orderDetail</param>
        /// <returns></returns>
        public ServiceResult updateOrder(OrderDetail orderDetail);

        /// <summary>
        /// created date: 10/5/2022
        /// created by: KhanhVX
        /// Hàm xóa hóa đơn 
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <returns></returns>
        public ServiceResult deleteOrder(Guid orderId);

        /// <summary>
        /// created date: 10/5/2022
        /// created by: KhanhVX
        /// Hàm xóa hóa đơn 
        /// </summary>
        /// <param name="orderDeailId">orderDeailId</param>
        /// <returns></returns>
        public ServiceResult deleteOrderDetail(Guid orderDeailId);
    }
}
