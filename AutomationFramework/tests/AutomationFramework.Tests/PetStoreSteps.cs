using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using AutomationFramework.Core;
using AutomationFramework.Models;
using FluentAssertions;

namespace AutomationFramework.Tests
{
    [Binding]
    public class PetStoreSteps
    {
        private readonly ApiClient _apiClient;
        private IRestResponse _response;
        private Pet _createdPet;

        public PetStoreSteps()
        {
            _apiClient = new ApiClient(ConfigReader.GetBaseUrl());
        }

        [Given(@"I create a new pet with name "(.*)" and status "(.*)"")]
        public async Task GivenICreateANewPet(string petName, string petStatus)
        {
            var pet = new Pet { Name = petName, Status = petStatus };
            _response = await _apiClient.PostAsync("/pet", pet);
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            _createdPet = JsonConvert.DeserializeObject<Pet>(_response.Content);
        }

        [When(@"I retrieve the pet by its ID")]
        public async Task WhenIRetrieveThePetById()
        {
            _response = await _apiClient.GetAsync($"/pet/{_createdPet.Id}");
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"I should see the pet name as "(.*)" and status as "(.*)"")]
        public void ThenIShouldSeeThePetNameAsAndStatusAs(string expectedName, string expectedStatus)
        {
            var retrievedPet = JsonConvert.DeserializeObject<Pet>(_response.Content);
            retrievedPet.Name.Should().Be(expectedName);
            retrievedPet.Status.Should().Be(expectedStatus);
        }
    }
}