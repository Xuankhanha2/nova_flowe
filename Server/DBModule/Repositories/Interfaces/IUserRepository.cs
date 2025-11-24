using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    /// <summary>
    /// Created date: 20/7/2021
    /// Created by: VXKHANH
    /// </summary>
    public interface IUserRepository:IBaseRepository
    {

        /// <summary>
        /// created date: 17/04/2022
        /// created by: khanhvx
        /// Lấy thông tin user theo username và password
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns>User</returns>
        public User getUser(string username, string password);

    }
}
