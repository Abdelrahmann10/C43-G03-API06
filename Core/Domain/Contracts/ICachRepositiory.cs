using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICachRepositiory
    {
        public Task SetAsync(string Key, object value, TimeSpan duration);
        public Task<string?> GetAsync(string Key);


    }
}
