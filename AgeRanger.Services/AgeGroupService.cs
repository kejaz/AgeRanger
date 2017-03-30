using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeRanger.Data.Infrastructure;
using AgeRanger.Data.Repositories;
using AgeRanger.Model;

namespace AgeRanger.Services
{
    public interface IAgeGroupService
    {
        AgeGroup GetAgeGroup(int age);
        IEnumerable<AgeGroup> GetAgeGroup();
    }

    public class AgeGroupService : IAgeGroupService
    {
        private readonly IAgeGroupRepository ageGroupRepository;
        private readonly IUnitOfWork unitOfWork;

        public AgeGroupService(IAgeGroupRepository ageGroupRepository, IUnitOfWork unitOfWork)
        {
            this.ageGroupRepository = ageGroupRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IPersonService Members

        public IEnumerable<AgeGroup> GetAgeGroup()
        {
            return ageGroupRepository.GetAll();
        }

        public AgeGroup GetAgeGroup(int age)
        {
            var ageGroup = ageGroupRepository.GetById(age);
            return ageGroup;
        }

        #endregion
    }
}
