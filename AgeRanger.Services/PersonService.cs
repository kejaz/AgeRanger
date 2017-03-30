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
    public interface IPersonService
    {
        IEnumerable<Person> GetPersons(string firstName = null);
        Person GetPerson(int id);
        Person GetPerson(string firsNname);
        void CreatePerson(Person person);

        void UpdatePerson(Person person);

        void SavePerson();
    }

    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personsRepository;
        private readonly IUnitOfWork unitOfWork;

        public PersonService(IPersonRepository personsRepository, IUnitOfWork unitOfWork)
        {
            this.personsRepository = personsRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IPersonService Members

        public IEnumerable<Person> GetPersons(string firstName = null)
        {
            if (string.IsNullOrEmpty(firstName))
                return personsRepository.GetAll();
            else
                return personsRepository.GetAll().Where(c => c.FirstName == firstName);
        }

        public Person GetPerson(int id)
        {
            var person = personsRepository.GetById(id);
            return person;
        }

        public Person GetPerson(string firstName)
        {
            var person = personsRepository.GetPersonByFirstName(firstName);
            return person;
        }

        public void CreatePerson(Person person)
        {
            personsRepository.Add(person);
        }
        public void UpdatePerson(Person person)
        {
            personsRepository.Update(person);
        }

        public void SavePerson()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}
