using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IPaymentService
    {
        public Task<BasketDto> CreateOrUpdatePaymentAsync(string basketId);
        public Task UpdateOrderPaymentStatus(string request, string stripHeader);
    }
}
