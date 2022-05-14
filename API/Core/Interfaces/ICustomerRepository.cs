using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    /// <summary>
    /// created date: 29/06/2021
    /// created by: VXKHANH
    /// Interface cho lớp CustomerRepository
    /// </summary>
    public interface ICustomerRepository:IBaseRepository
    {
        /// <summary>
        /// created date: 11/04/2021
        /// created by: vxkhanh
        /// Hàm kiểm tra đăng nhập tài khoản khách hàng
        /// </summary>
        /// <param name="username">Tên tài khoản</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns>Thông tin khách hàng</returns>
        public Customer customerLogin(string username, string password);

        /// <summary>
        /// created by: khanhvx
        /// created date: 3/5/2022
        /// Hàm đăng ký khách hàng mới
        /// </summary>
        /// <param name="customer">Đối tượng khách hàng</param>
        /// <returns></returns>
        public int customerRegister(Customer customer);
       
    }
}
