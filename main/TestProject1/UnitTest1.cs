using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RESTSharpTest;
using System.Collections.Generic;

namespace Json_server_addressBookUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }

        private IRestResponse getContactList()
        {
            RestRequest request = new RestRequest("/AddressBook", Method.GET);

            //act

            IRestResponse response = client.Execute(request);
            return response;
        }
        //UC22
        [TestMethod]
        public void onCallingGETApi_ReturnTotalContactList()
        {
            IRestResponse response = getContactList();

            //assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<AddressBook> dataResponse = JsonConvert.DeserializeObject<List<AddressBook>>(response.Content);
            Assert.AreEqual(4, dataResponse.Count);
            foreach (var item in dataResponse)
            {
                System.Console.WriteLine("id: " + item.id + "First Name: " + item.first_name + "last_name: " + item.last_name + " " +
                    "address: " + item.address + "city: " + item.city + "state: " + item.state + " " +
                    "phone_numner: " + item.phone_number + "email: " + item.email);
            }
        }

        //UC23
        [TestMethod]
        public void givenContact_OnPost_ShouldReturnAddedContact()
        {
            RestRequest request = new RestRequest("/AddressBook", Method.POST);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("first_name", "Anurag");
            jObjectbody.Add("last_name", "Prakash");
            jObjectbody.Add("address", "Mango");
            jObjectbody.Add("city", "Jamshedpur");
            jObjectbody.Add("state", "Jharkhand");
            jObjectbody.Add("zip", "831012");
            jObjectbody.Add("phone_number", "9518039211");
            jObjectbody.Add("email", "Prakash.shanu80@gmail.com");

            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            //act
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            AddressBook dataResponse = JsonConvert.DeserializeObject<AddressBook>(response.Content);
            Assert.AreEqual("Anurag", dataResponse.first_name);
            Assert.AreEqual("prakash", dataResponse.last_name);
            Assert.AreEqual("mango", dataResponse.address);
            Assert.AreEqual("cpr", dataResponse.city);
            Assert.AreEqual("jamshedpur", dataResponse.state);
            Assert.AreEqual("831012", dataResponse.zip);
            Assert.AreEqual("9518039211", dataResponse.phone_number);
            Assert.AreEqual("prakash.shanu80@gmail.com", dataResponse.email);


        }

        //UC24
        [TestMethod]
        public void givenContact_OnUpdate_ShouldReturnUpdatedContact()
        {
            RestRequest request = new RestRequest("/AddressBook/4", Method.PUT);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("first_name", "shanu");
            jObjectbody.Add("last_name", "tiwari");
            jObjectbody.Add("address", "mango");
            jObjectbody.Add("city", "jamshedpur");
            jObjectbody.Add("state", "jhr");
            jObjectbody.Add("zip", "831012");
            jObjectbody.Add("phone_number", "9518039211");
            jObjectbody.Add("email", "shanu@gmail.com");

            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            //act
            var response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            AddressBook dataResponse = JsonConvert.DeserializeObject<AddressBook>(response.Content);
            Assert.AreEqual("shanu", dataResponse.first_name);
            Assert.AreEqual("shanu@gmail.com", dataResponse.email);



        }

        //UC25
        [TestMethod]
        public void givenPersonId_OnDelete_ShouldReturnSuccessStatus()
        {
            //arrange
            RestRequest request = new RestRequest("/AddressBook/4", Method.DELETE);

            //act
            IRestResponse response = client.Execute(request);

            //assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            System.Console.WriteLine(response.Content);

        }




    }
}