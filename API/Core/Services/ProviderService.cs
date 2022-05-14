using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    /// <summary>
    /// Created date: 20/7/2021
    /// Created by: VXKHANH
    /// </summary>
    public class ProviderService: BaseService, IStoreService
    {
        IStoreRepository storeRepository;
        public ProviderService(IStoreRepository _storeRepository) : base(_storeRepository)
        {
            storeRepository = _storeRepository;
        }
    }
}
