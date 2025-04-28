using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICachService
    {
        public Task<string> GetCachValueAsync(string cachkey);
        public Task SetCachValueAsync(string cachkey, object value, TimeSpan duration);
    }
}
