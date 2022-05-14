using Core.Interfaces;
using Core.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
namespace Infastructure.Repositories
{
    /// <summary>
    /// created date: 6/5/2022
    /// created by: KhanhVX
    /// Lớp tương tác với bảng hóa đơn trong DB
    /// </summary>
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        OrderDetailRepository orderDetailRepository;
        public OrderRepository()
        {
            orderDetailRepository = new OrderDetailRepository();
        }

        /// <summary>
        /// created date: 10/5/2022
        /// created by: KhanhVX
        /// Hàm xóa hóa đơn 
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <returns>int</returns>
        public int deleteOrder(Guid orderId)
        {
            string procName = "procDeleteOrder";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("orderId", orderId.ToString());
            int result = dbConnection.Execute(procName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// created date: 10/5/2022
        /// created by: KhanhVX
        /// Hàm xóa hóa đơn 
        /// </summary>
        /// <param name="orderDeailId">orderDeailId</param>
        /// <returns>int</returns>
        public int deleteOrderDetail(Guid orderDeailId)
        {
            return orderDetailRepository.deleteOrderDetail(orderDeailId);
        }

        public List<Order> getByCustomerId(Guid customerId)
        {
            List<Order> trueList = new List<Order>();
            string procName = "procGetOrdersByCustomerId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("customerId", customerId.ToString());
            List<Order> result = dbConnection.Query<Order>(procName, param: dynamicParameters, commandType:CommandType.StoredProcedure).ToList();
            foreach(Order order in result)
            {
                order.orderDetails = orderDetailRepository.getOrderDetailByOrderId(order.orderId);
                trueList.Add(order);
            }
            return trueList;
        }

        /// <summary>
        /// created date: 6/5/2022
        /// created by: KhanhVX
        /// Hàm lấy thông tin hóa đơn theo orderId và customerId
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <param name="customerId">customerId</param>
        /// <returns></returns>
        public Order getOrdersOfCustomer(Guid orderId, Guid customerId)
        {
            string procName = "procGetOrderById";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("orderId", orderId);
            dynamicParameters.Add("customerId", customerId);
            Order order = dbConnection.Query<Order>(procName, param: dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            List<OrderDetail> orderDetail = orderDetailRepository.getOrderDetailByOrderId(orderId);
            order.orderDetails = orderDetail;
            return order;
        }

        /// <summary>
        /// created by: khanhvx 
        /// created date: 7/5/2022
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns></returns>
        public int insertOrder(Order order)
        {
            string procName = "procInsertOrder";
            //Tạo các dynamicParameter
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("customerId", order.customerId);
            dynamicParameters.Add("createdBy", order.createdBy);
            var result = dbConnection.Query<string>(procName, param:dynamicParameters, commandType:CommandType.StoredProcedure).FirstOrDefault();
            if(result != "" && result != null && order.orderDetails != null)
            {
                foreach (OrderDetail orderDetail in order.orderDetails)
                {
                    orderDetail.orderId = Guid.Parse(result);
                    orderDetailRepository.insertEntity<OrderDetail>(orderDetail);
                }
            }
            return 1;
        }

        /// <summary>
        /// created date: 10/5/2022
        /// created by: KhanhVX
        /// Hàm cập nhật hóa đơn
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        public int updateOrder(OrderDetail orderDetail)
        {
            return orderDetailRepository.updateOrderDetail(orderDetail);
        }
    }
}
