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
    /// created date: 02/11/21
    /// created by: vxkhanh
    /// </summary>
    public class CartRepository : BaseRepository, ICartRepository
    {
        /// <summary>
        /// Hàm lấy danh sách giỏ hàng theo id khách hàng
        /// </summary>
        /// <param name="customerId">id khách hàng</param>
        /// <returns>List cart</returns>
        public IEnumerable<Cart> getCartsByCustomer(Guid customerId)
        {
            string className = typeof(Cart).Name;
            string procName = $"procGet{className}sByCustomerId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("customerId", customerId);
            var carts = dbConnection.Query<Cart>(procName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return carts;
        }

        public Cart getExistsCart(Guid customerId, Guid productId)
        {
            string procName = $"procGetCartByCustomerIdNProductId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("customerId", customerId);
            dynamicParameters.Add("productId", productId);
            Cart cart = dbConnection.Query<Cart>(procName, param: dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return cart;
        }
    }
}
