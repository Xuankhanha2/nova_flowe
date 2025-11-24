using Core.Interfaces;
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
    /// Created date: 20/7/2021
    /// Created by: VXKHANH
    /// </summary>
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Customer customerLogin(string username, string password)
        {
            string procName = "procCustomerLogin";
            
            DynamicParameters dynamicParameters = new DynamicParameters();
            var crytoPassword = base.sha256Hash(password);
            dynamicParameters.Add("username", username);
            dynamicParameters.Add("password", crytoPassword);
            Customer customer = dbConnection.Query<Customer>(procName, param: dynamicParameters, commandType:CommandType.StoredProcedure).FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// created by: khanhvx
        /// created date" 3/5/2022
        /// Hàm đăng ký khách hàng mới
        /// </summary>
        /// <param name="customer">Đối tượng khách hàng</param>
        /// <returns></returns>
        public int customerRegister(Customer customer)
        {
            string procName = $"procInsertCustomer";
            customer.password = base.sha256Hash(customer.password);
            int result = dbConnection.Execute(procName, param: customer, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
