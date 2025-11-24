using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    /// <summary>
    /// created date: 02/11/21
    /// created by: vxkhanh
    /// </summary>
    public interface ICartService:IBaseService
    {
        /// <summary>
        /// Hàm lấy danh sách sản phẩm trong giỏ hàng theo mã khách hàng
        /// </summary>
        /// <param name="customerId">id khách hàng</param>
        /// <returns>service result</returns>
        public ServiceResult getCartsByCustomerId(Guid customerId);

        /// <summary>
        /// created by: khanhvx
        /// created date: 8/5/2022
        /// </summary>
        /// <param name="customerId">customerId</param>
        /// <param name="productId">productId</param>
        public ServiceResult getExistsCart(Guid customerId, Guid productId);
    }
}
