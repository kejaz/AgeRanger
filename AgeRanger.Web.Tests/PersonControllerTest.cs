using System;
using System.Collections.Generic;
using AgeRanger.Data.Infrastructure;
using AgeRanger.Data.Repositories;
using AgeRanger.Model;
using AgeRanger.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AgeRanger.Web.Tests
{
    // In the following Test class I have only check/Test two following methods  
    // 1. TestGetPerson() 
    // 2. TestGetAgeGroup()

    [TestClass]
    public class PersonControllerTest
    {

        //Arrange
        IPersonService _personService;
        IAgeGroupService _ageGroupService;
          

        public PersonControllerTest(IPersonService personService, IAgeGroupService ageGroupService)
        {
            this._personService = personService;
            this._ageGroupService = ageGroupService;
        }


        [TestMethod]
        public void TestGetPerson()
        {
            //Act
            IEnumerable<Person> persons = _personService.GetPersons();

            //Assert
            Assert.IsNotNull(persons);
        }

        [TestMethod]
        public void TestGetAgeGroup()
        {
            //Act
            AgeGroup ageGroup = _ageGroupService.GetAgeGroup(25);

            //Assert
            Assert.IsNotNull(ageGroup);
        }
    }
}
