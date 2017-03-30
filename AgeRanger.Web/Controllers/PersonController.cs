using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgeRanger.Model;
using AgeRanger.Services;
using AgeRanger.Web.Infrastrcture;
using AgeRanger.Web.scripts.spa.ViewModel;

namespace AgeRanger.Web.Controllers
{
    [RoutePrefix("api/person")]
    public class PersonController : ApiControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IAgeGroupService _ageGroupService;

        public PersonController(IPersonService personService, IAgeGroupService ageGroupService)
        {
            this._personService = personService;
            this._ageGroupService = ageGroupService;
        }

        [AllowAnonymous]
        [Route("getpersons")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //var persons = _personService.GetPersons().OrderBy(p => p.FirstName).ToList();

                var persons = from p in _personService.GetPersons()
                           from ag in _ageGroupService.GetAgeGroup()
                           where p.Age >= ag.MinAge && p.Age < ag.MaxAge
                           select new PersonViewModel { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, Age = p.Age, AgeGroup = ag.Description };
                
                response = request.CreateResponse<IEnumerable<PersonViewModel>>(HttpStatusCode.OK, persons);
                return response;
            });
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request, string filter)
        {
            filter = filter.ToLower().Trim();
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var persons = _personService.GetPersons()
                    .Where(p => p.FirstName.ToLower().Contains(filter) ||
                    p.LastName.ToLower().Contains(filter)).OrderBy(o => o.FirstName).ToList();

                response = request.CreateResponse<IEnumerable<Person>>(HttpStatusCode.OK, persons);
                return response;
            });
        }


        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, Person person)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    _personService.CreatePerson(person);

                    _personService.SavePerson();
                    
                    response = request.CreateResponse<Person>(HttpStatusCode.Created, person);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, Person person)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    _personService.UpdatePerson(person);
                    _personService.SavePerson();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

         [HttpGet]
        [Route("search/{page:int=0}/{pageSize=100}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<PersonViewModel> persons = null;
                int totalPersons = new int();

                var per = from p in _personService.GetPersons()
                          from ag in _ageGroupService.GetAgeGroup()
                          where p.Age >= ag.MinAge && p.Age < ag.MaxAge
                          select new PersonViewModel { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, Age = p.Age, AgeGroup = ag.Description };

                int acc = per.Count();
                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    persons = per.Where(p => p.FirstName.ToLower().Contains(filter) ||
                         p.LastName.ToLower().Contains(filter))
                        .OrderBy(p => p.FirstName)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalPersons = per.ToList()
                        .Where(p => p.FirstName.ToLower().Contains(filter) ||
                            p.LastName.ToLower().Contains(filter))
                        .Count();
                }
                else
                {
                    persons = per.ToList()
                        .OrderBy(p => p.FirstName)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totalPersons = _personService.GetPersons().Count();
                }

                PaginationSet<PersonViewModel> pagedSet = new PaginationSet<PersonViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalPersons,
                    TotalPages = (int)Math.Ceiling((decimal) totalPersons / currentPageSize),
                    Items = persons
                };

                response = request.CreateResponse<PaginationSet<PersonViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
    }
}
