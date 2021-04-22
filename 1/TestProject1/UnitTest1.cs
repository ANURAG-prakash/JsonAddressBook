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

        
        }




    }
}