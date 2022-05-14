using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using System.Linq;

namespace Infastructure.Repositories
{
    /// <summary>
    /// created date: 6/5/2022
    /// created by: KhanhVX\
    /// Lớp tương tác với bảng chi tiết hóa đơn trong DB
    /// </summary>
    class OrderDetailRepository:BaseRepository
    {
        /// <summary>
        /// created by: khanhvx
        /// created date: 6/5/2022
        /// Hàm lấy danh sách chi tiết đơn hàng theo id đơn hàng
        /// </summary>
        /// <param name="orderId"></param>
        public List<OrderDetail> getOrderDetailByOrderId(Guid orderId)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            string procName = "procGetOrderDetailByOrderId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("orderId", orderId.ToString());
            orderDetails = dbConnection.Query<OrderDetail>(procName, param: dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
            if (orderDetails.Count <= 0)
                return null;
            else
                return orderDetails;
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
            string procName = "procUpdateOrderDetail";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("orderDetailId", orderDetail.orderDetailId.ToString());
            dynamicParameters.Add("quantity", orderDetail.quantity);
            var result = dbConnection.Execute(procName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// created date: 10/5/2022
        /// created by: KhanhVX
        /// Hàm xóa hóa đơn 
        /// </summary>
        /// <param name="orderDeailId">orderDeailId</param>
        public int deleteOrderDetail(Guid orderDeailId)
        {
            string procName = "procDeleteOrderDetail";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("orderDetailId", orderDeailId.ToString());
            int result = dbConnection.Execute(procName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return result;
        }

    }
}
