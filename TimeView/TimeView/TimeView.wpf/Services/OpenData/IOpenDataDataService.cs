using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.wpf.Services
{
    public interface IOpenDataDataService
    {
        Task<Company[]> UpdateOpenData();
    }
}