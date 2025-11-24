using Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    /// <summary>
    /// created date: 11/04/2021
    /// created by: vxkhanh
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// created date: 04/11/2021
        /// Hàm kiểm tra đăng nhập
        /// </summary>
        /// <param name="username">tên tài khoản</param>
        /// <param name="password">mật khẩu</param>
        /// <returns>ServiceResult</returns>
        public ServiceResult login(string username, string password);
    }
}
