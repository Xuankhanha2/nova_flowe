using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    /// <summary>
    /// Created date: 20/7/2021
    /// Created by: VXKHANH
    /// </summary>
    public class CustomerService:BaseService , ICustomerService
    {
        ICustomerRepository customerRepository;
        public CustomerService(ICustomerRepository _customerRepository) : base(_customerRepository)
        {
            customerRepository = _customerRepository;
        }

        public ServiceResult customerLogin(string username, string password)
        {
            try
            {
                Customer customer = customerRepository.customerLogin(username, password);
                if (customer != null)
                {
                    serviceResult.data = customer;
                    serviceResult.code = statusCode.success;
                    serviceResult.message = "success";
                }
                else
                {
                    serviceResult.code = statusCode.noContent;
                    serviceResult.data = null;
                    serviceResult.message = "fail";
                }
            }
            catch (Exception e)
            {
                serviceResult.code = statusCode.exception;
                serviceResult.message = e.Message;
                serviceResult.data = null;
            }
            return serviceResult;
        }

        /// <summary>
        /// created by: khanhvx
        /// created date: 3/5/2022
        /// Hàm đăng ký khách hàng
        /// </summary>
        /// <param name="customer">Đối tượng khách hàng</param>
        /// <returns></returns>
        public ServiceResult customerRegister(Customer customer)
        {
            try
            {
                //validate với trạng thái là insert
                base.baseValidate<Customer>(customer, false);
                if (serviceResult.isValid)
                {
                    int check = customerRepository.customerRegister(customer);
                    if (check > 0)
                    {
                        serviceResult.code = statusCode.success;
                        serviceResult.message = Properties.resource.createdSuccess;
                        serviceResult.data = check;
                    }
                    else
                    {
                        serviceResult.code = statusCode.fail;
                        serviceResult.message = Properties.resource.createFail;
                        serviceResult.data = check;
                    }
                }
                else
                {
                    serviceResult.code = statusCode.notValid;
                    //Không cần gán message vì trong hàm validate đã gán rồi
                    serviceResult.data = null;
                }
            }
            catch (Exception e)
            {
                serviceResult.message = e.Message;
                serviceResult.isValid = false;
                serviceResult.data = null;
                serviceResult.code = statusCode.exception;
            }
            return serviceResult;
        }
    }
}
