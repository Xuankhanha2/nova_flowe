using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    /// <summary>
    /// Created date: 20/7/2021
    /// Created by: VXKHANH
    /// </summary>
    public interface IUserService:IBaseService
    {
        /// <summary>
        /// Created date: 17/04/22
        /// created by: Khanhvx
        /// Lấy thông tin user theo username và password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>User</returns>
        public ServiceResult getUser(string username, string password);
    }
}
