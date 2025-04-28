using Domain.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CachService(ICachRepositiory cachRepositiory) : ICachService
    {
        public async Task<string> GetCachValueAsync(string cachkey)
        {
            return await cachRepositiory.GetAsync(cachkey);
        }

        public Task SetCachValueAsync(string cachkey, object value, TimeSpan duration)
        {
            return cachRepositiory.SetAsync(cachkey, value, duration);
        }
    }
}
