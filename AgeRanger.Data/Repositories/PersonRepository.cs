using AgeRanger.Data.Infrastructure;
using AgeRanger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.Data.Repositories
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Person GetPersonByFirstName(string firstName)
        {
            var person = this.DbContext.Persons.Where(c => c.FirstName == firstName).FirstOrDefault();

            return person;
        }

        public override void Update(Person entity)
        {
            base.Update(entity);
        }
    }

    public interface IPersonRepository : IRepository<Person>
    {
        Person GetPersonByFirstName(string FirstName);
    }
}
