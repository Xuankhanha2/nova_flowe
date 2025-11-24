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

        public override IEnumerable<entity> GetAll<entity>(int? pageNumber, int? items)
        {
            List<entity> trueList = new List<entity>();
            string className = typeof(entity).Name;
            string procName = $"procGet{className}s";
            List<entity> list = dbConnection.Query<entity>(procName, commandType: CommandType.StoredProcedure).ToList();
            foreach (var order in list)
            {
                Guid orderId = Guid.Parse(order.GetType().GetProperty("orderId").GetValue(order).ToString());
                order.GetType().GetProperty("orderDetails").SetValue(order, orderDetailRepository.getOrderDetailByOrderId(orderId));
                trueList.Add(order);
            }
            if (pageNumber != null && items != null)
            {
                //Số trang - mặc định số trang đầu tiền
                int page = pageNumber ?? 1;
                //Số lượng phần tử trong 1 trang - mặc định 10 phần tử
                int number = items ?? 10;
                //Chỉ số bắt đầu khi sao chép list
                int startIndex = (page - 1) * number;
                //Kiểm tra số lượng phần tử list > số lượng phần tử của 1 trang thì mới thực hiện 
                if (list.Count > number)
                {

                    if (list.Count - startIndex >= number)
                    {
                        //nếu số phần tử còn lại tính từ vị trí startIndex đến phần tử cuối cùng của mảng lớn hơn số lượng phần tử của 1 trang
                        entity[] subList = new entity[number];
                        list.CopyTo(startIndex, subList, 0, number);
                        return subList;
                    }
                    else
                    {
                        entity[] subList = new entity[list.Count - startIndex];
                        list.CopyTo(startIndex, subList, 0, list.Count - startIndex);
                        return subList;
                    }
                }

            }
            return trueList;
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
        public int updateOrderDetail(OrderDetail orderDetail)
        {
            return orderDetailRepository.updateOrderDetail(orderDetail);
        }

        /// <summary>
        /// created by: KhanhVX
        /// created date: 10/5/2022
        /// Hàm cập nhật hóa đơn
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int updateOrder(Order order)
        {
            string procName = "procUpdateOrder";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("orderId", order.orderId.ToString());
            dynamicParameters.Add("status", order.status);
            int result = dbConnection.Execute(procName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
