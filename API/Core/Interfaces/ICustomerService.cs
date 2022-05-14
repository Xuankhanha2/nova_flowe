using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    /// <summary>
    /// created date: 29/06/2021
    /// created by: VXKHANH
    /// Interface cho lớp customerService
    /// </summary>
    public interface ICustomerService:IBaseService
    {
        /// <summary>
        /// created date: 11/04/2021
        /// created by: vxkhanh
        /// Hàm kiểm tra đăng nhập tài khoản khách hàng
        /// </summary>
        /// <param name="username">Tên tài khoản</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns>ServiceResult</returns>
        public ServiceResult customerLogin(string username, string password);

        /// <summary>
        /// created date: 3/5/2022
        /// created by: khanhvx
        /// Hàm đăng ký khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public ServiceResult customerRegister(Customer customer);

    }
}
