using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.domain;

namespace TimeView.wpf.Services
{
    internal class OpenDataDataService : IOpenDataDataService
    {
        private readonly IOpenDataRepository _repository;

        public OpenDataDataService(IOpenDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<Company[]> UpdateOpenData()
        {
            return await _repository.UpdateOpenData();
        }
    }
}