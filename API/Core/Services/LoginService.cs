using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{

    /// <summary>
    /// created date: 11/04/2021
    /// created by: vxkhanh
    /// </summary>
    public class LoginService:ILoginService
    {
        ICustomerService customerService;
        public LoginService(ICustomerService _customerService)
        {
            customerService = _customerService;
        }
        /// <summary>
        /// created date: 04/11/2021
        /// Hàm kiểm tra đăng nhập
        /// </summary>
        /// <param name="username">tên tài khoản</param>
        /// <param name="password">mật khẩu</param>
        /// <returns>ServiceResult</returns>
        public ServiceResult login(string username, string password)
        {
            return customerService.customerLogin(username, password);
        }
    }
}
