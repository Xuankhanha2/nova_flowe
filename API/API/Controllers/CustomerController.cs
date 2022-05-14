using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web.Helpers;

namespace API.Controllers
{
    /// <summary>
    /// created date: 29/06/2021
    /// created by: VXKHANH
    /// Lớp điều khiển cho Customer
    /// </summary>
    public class CustomerController : BaseController<Customer>
    {
        ICustomerService customerService;
        public CustomerController(ICustomerService _customerService): base(_customerService)
        {
            customerService = _customerService;
        }

        /// <summary>
        /// created date: 11/08/2021
        /// created by: VXKHANH
        /// Thêm thông tin khách hàng mới với mật khẩu được mã hóa sha256
        /// </summary>
        [HttpPost]
        public override IActionResult post(Customer papram)
        {
            try
            {
                papram.password = Crypto.SHA256(papram.password).ToUpper();
                return base.post(papram);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        /// <summary>
        /// created by: khanhvx
        /// created date: 3/5/2022
        /// Lấy thông tin của khách hàng với tài khoản và mật khẩu 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult customerLogin(Customer customer)
        {
            if (customer != null)
            {
                try
                {
                    if(customer.username != "" && customer.password != "")
                    {
                        ServiceResult serviceResult = customerService.customerLogin(
                            customer.username, customer.password
                        );
                        if(serviceResult.code == statusCode.notValid)
                        {
                            return StatusCode(400, serviceResult.data);
                        }
                        else if(serviceResult.code == statusCode.exception)
                        {
                            return StatusCode(500, serviceResult.data);
                        }
                        else
                        {
                            return Ok(serviceResult.data);
                        }
                    }
                    else
                    {
                        return BadRequest("Username or password is not valid");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return BadRequest("Username or password is not valid");
            }
        }

        [HttpPost("register")]
        public IActionResult customerRegister(Customer customer)
        {
            ServiceResult result = customerService.customerRegister(customer);
            if (result.code == statusCode.exception)
                return StatusCode(500, result);
            else if (result.code == statusCode.success)
                return StatusCode(201, result);
            else if (result.code == statusCode.fail)
                return StatusCode(200, result);
            else
                return StatusCode(400, result);
        }
    }
}
