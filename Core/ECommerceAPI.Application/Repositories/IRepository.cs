using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity //DbSet wants a class constraint. We are using BaseEntity.cs as a marker. So we can say BaseEntity is a class too and we can use this constraint.
    {
        DbSet<T> Table { get; } //don't need to set; 
    }
}
