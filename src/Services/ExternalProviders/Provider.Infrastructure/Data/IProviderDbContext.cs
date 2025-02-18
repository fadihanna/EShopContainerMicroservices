using Microsoft.EntityFrameworkCore;
using Provider.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Data
{
    public interface IProviderDbContext
    {
        public DbSet<MasaryService> MasaryServiceslists { get;  }
        public DbSet<MasaryServiceCharge> ServiceCharges { get;   }
        public DbSet<MasaryServiceParameter> ServiceParameters { get;  }
    }
}
