using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeRanger.Data;

namespace AgeRanger.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        AgeRangerEntities Init();
    }
}
