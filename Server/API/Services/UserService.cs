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
    public class UserService : BaseService, IUserService
    {
        IUserRepository userRepository;

        public UserService(IUserRepository _userRepository) : base(_userRepository)
        {
            userRepository = _userRepository;
        }

        public ServiceResult getUser(string username, string password)
        {
            try
            {
                var data = userRepository.getUser(username, password);
                if (data != null)
                {
                    serviceResult.isValid = true;
                    serviceResult.data = data;
                    serviceResult.code = statusCode.success;
                }
                else
                {
                    serviceResult.isValid = true;
                    serviceResult.data = data;
                    serviceResult.code = statusCode.noContent;
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
