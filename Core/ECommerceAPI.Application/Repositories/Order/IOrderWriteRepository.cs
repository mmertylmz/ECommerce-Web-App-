using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories //.Order Namespace deleted because it is conflicting with Generic Repository class.
{
    public interface IOrderWriteRepository:IWriteRepository<Order>
    {
    }
}
