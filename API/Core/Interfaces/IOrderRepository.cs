using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    /// <summary>
    /// created date: 6/5/2022
    /// created by: KhanhVX
    /// Lớp tương tác với bảng hóa đơn trong DB
    /// </summary>
    public interface IOrderRepository: IBaseRepository
    {
        /// <summary>
        /// created date: 6/5/2022
        /// created by: KhanhVX
        /// Hàm lấy thông tin hóa đơn theo orderId và customerId
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Order getOrdersOfCustomer(Guid orderId, Guid customerId);

        /// <summary>
        /// created date: 6/5/2022
        /// created by: KHANHVX
        /// Lấy danh sách hóa đơn theo mã khách hàng
        /// </summary>
        /// <param name="customerId">customerId</param>
        /// <returns>ServiceResult</returns>
        public List<Order> getByCustomerId(Guid customerId);

        /// <summary>
        /// created date: 7/5/2022
        /// created by: KHANHVX
        /// Thêm mới hóa đơn
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns></returns>
        public int insertOrder(Order order);

        /// <summary>
        /// created date: 10/5/2022
        /// created by: KhanhVX
        /// Hàm cập nhật hóa đơn
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        public int updateOrder(OrderDetail orderDetail);

        /// <summary>
        /// created date: 10/5/2022
        /// created by: KhanhVX
        /// Hàm xóa hóa đơn 
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <returns></returns>
        public int deleteOrder(Guid orderId);

        /// <summary>
        /// created date: 10/5/2022
        /// created by: KhanhVX
        /// Hàm xóa hóa đơn 
        /// </summary>
        /// <param name="orderDeailId">orderDeailId</param>
        public int deleteOrderDetail(Guid orderDeailId);

    }
}
