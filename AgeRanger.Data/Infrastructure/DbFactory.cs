using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeRanger.Data;

namespace AgeRanger.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        AgeRangerEntities dbContext;

        public AgeRangerEntities Init()
        {
            return dbContext ?? (dbContext = new AgeRangerEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
