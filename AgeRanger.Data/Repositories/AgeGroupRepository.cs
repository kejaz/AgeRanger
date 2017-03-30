using AgeRanger.Data.Infrastructure;
using AgeRanger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.Data.Repositories
{
    public class AgeGroupRepository : RepositoryBase<AgeGroup>, IAgeGroupRepository
    {
        public AgeGroupRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public AgeGroup GetAgeGroupByAge(int age)
        {
            var ageGroup = this.DbContext.AgeGroups.Where(g => g.MinAge <= age && g.MaxAge > age).FirstOrDefault();

            return ageGroup;
        }        
    }

    public interface IAgeGroupRepository : IRepository<AgeGroup>
    {
        AgeGroup GetAgeGroupByAge(int age);
    }
}
