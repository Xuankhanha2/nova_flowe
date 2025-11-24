using Core.Interfaces;
using Core.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
namespace Infastructure.Repositories
{
    /// <summary>
    /// Created date: 20/7/2021
    /// Created by: VXKHANH
    /// </summary>
    public class UserRepository : BaseRepository, IUserRepository
    {
        /// <summary>
        /// created by: khanhvx
        /// created date: 3/5/2022
        /// Hàm lấy thông tin user theo username và password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User getUser(string username, string password)
        {
            string procName = $"procGetUser";
            SHA256Managed crypto = new SHA256Managed();
            string cryptoPassword = base.sha256Hash(password);
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@username", username);
            dynamicParameters.Add($"@password", cryptoPassword);
            User result = dbConnection.Query<User>(procName, param: dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return result;
        }

        
    }
}
