using MvcUdemy.Models;
using MvcUdemy.Utils;
using NUnit.Framework;
using System.Collections.Generic;

namespace UdemyTests
{
    public class ClientUnitTest
    {
        private string _validName;
        private string _validCnpj;
        private string _validPhone;
        
        private string _anotherValidCnpj;
        private string _anotherValidPhone;

        private string _invalidCnpj;
        private string _invalidPhone;
        
        private ClientModel _validClient;
        private ClientModel _invalidCnpjClient;
        private ClientModel _invalidPhoneClient;

        private ClientResponse _validResponse;
        private ClientResponse _invalidCnpjResponse;
        private ClientResponse _invalidPhoneResponse;
        private ClientResponse _clientCreatedValidResponse;
        private ClientResponse _clientRemovedValidResponse;

        [SetUp]
        public void Setup()
        {
            _validName = "Minha Empresa LTDA";
            _validCnpj = "13.203.354/0001-85";
            _validPhone = "(85)99999-7777";
            
            _anotherValidCnpj = "13.203.354/0001-84";
            _anotherValidPhone = "(85)99999-8888";

            _invalidCnpj = "13.2033540001-85";
            _invalidPhone = "(85)999997777";

            _validClient = new ClientModel() { Cnpj = _validCnpj, Name = _validName, Phone = _validPhone };
            _invalidCnpjClient = new ClientModel() { Cnpj = _invalidCnpj, Name = _validName, Phone = _validPhone };
            _invalidPhoneClient = new ClientModel() { Cnpj = _validCnpj, Name = _validName, Phone = _invalidPhone };

            _validResponse = new ClientResponse() { Success = true, Message = string.Empty };
            _invalidCnpjResponse =  new ClientResponse() { Success = false, Message = "Invalid Cnpj format." };
            _invalidPhoneResponse =  new ClientResponse() { Success = false, Message = "Phone format invalid." };

            _clientCreatedValidResponse = new ClientResponse() { Success = true, Message = "Success." };

            _clientRemovedValidResponse = new ClientResponse() { Success = true, Message = "Client removed." };
        }

        [Test]
        public void ValidateClient()
        {
            Assert.IsNotNull(_validClient);
            Assert.IsTrue(_validClient.Cnpj == _validCnpj);
            Assert.IsTrue(_validClient.Name == _validName);
            Assert.IsTrue(_validClient.Phone == _validPhone);
        }

        [Test]
        public void ValidateCreateClient()
        {
            Assert.IsNotNull(_validClient.CreateClient(_validClient));
            Assert.AreEqual(_clientCreatedValidResponse.Success, _validClient.CreateClient(_validClient).Success);
            Assert.AreEqual(_clientCreatedValidResponse.Message, _validClient.CreateClient(_validClient).Message);
        }

        [Test]
        public void ValidateVerifyIfClientIsValid()
        {
            Assert.IsNotNull(_validClient.VerifyIfClientIsValid(_validClient));
            Assert.AreEqual(_validResponse.Success, _validClient.VerifyIfClientIsValid(_validClient).Success);
            Assert.AreEqual(_validResponse.Message, _validClient.VerifyIfClientIsValid(_validClient).Message);

            Assert.AreEqual(_invalidCnpjResponse.Success, _invalidCnpjClient.VerifyIfClientIsValid(_invalidCnpjClient).Success);
            Assert.AreEqual(_invalidCnpjResponse.Message, _invalidCnpjClient.VerifyIfClientIsValid(_invalidCnpjClient).Message);

            Assert.AreEqual(_invalidPhoneResponse.Success, _invalidPhoneClient.VerifyIfClientIsValid(_invalidPhoneClient).Success);
            Assert.AreEqual(_invalidPhoneResponse.Message, _invalidPhoneClient.VerifyIfClientIsValid(_invalidPhoneClient).Message);
        }

        [Test]
        public void ValidateGetClient()
        {
            Assert.IsNotNull(new ClientModel().GetClients());
        }

        [Test]
        public void ValidateUpdateClientIfExists()
        {
            var clients = new List<ClientModel>() {
                new ClientModel() { Cnpj = _validCnpj, Name = _validName, Phone = _validPhone },
                new ClientModel() { Cnpj = _anotherValidCnpj, Name = _validName, Phone = _anotherValidPhone }
            };

            var newClient = new ClientModel() { Cnpj = _validCnpj, Name = "New Name For My Comapny", Phone = _validPhone };
            var newClients = newClient.UpdateClientIfExists(clients, newClient);
            
            var oldClients = new List<ClientModel>() {
                new ClientModel() { Cnpj = _validCnpj, Name = _validName, Phone = _validPhone },
                new ClientModel() { Cnpj = _anotherValidCnpj, Name = _validName, Phone = _anotherValidPhone }
            };
                       
            Assert.IsNotNull(clients);
            Assert.IsTrue(!oldClients.Equals(newClients));
        }

        [Test]
        public void ValidateSaveClientOnJson()
        {
            var clients = new List<ClientModel>() {
                new ClientModel() { Cnpj = _validCnpj, Name = _validName, Phone = _validPhone },
                new ClientModel() { Cnpj = _anotherValidCnpj, Name = _validName, Phone = _anotherValidPhone }
            };
            
            Assert.IsNotNull(_validClient.SaveClientsOnJson(clients));
            Assert.AreEqual(_clientCreatedValidResponse.Success, _validClient.SaveClientsOnJson(clients).Success);
            Assert.AreEqual(_clientCreatedValidResponse.Message, _validClient.SaveClientsOnJson(clients).Message);
        }

        [Test]
        public void ValidateRemmoveClientOfJson()
        {
            //Creates valid client for the test
            ValidateSaveClientOnJson();
            var response = _validClient.RemmoveClientOfJson(_validClient);

            Assert.IsNotNull(response);
            Assert.AreEqual(_clientRemovedValidResponse.Success, response.Success);
            Assert.AreEqual(_clientRemovedValidResponse.Message, response.Message);

            //If the client is already removed, the behavior mus be the same
            response = _validClient.RemmoveClientOfJson(_validClient);

            Assert.IsNotNull(response);
            Assert.AreEqual(_clientRemovedValidResponse.Success, response.Success);
            Assert.AreEqual(_clientRemovedValidResponse.Message, response.Message);
        }

        //TODO: Create unit test class for Utils.
        [Test]
        public void ValidateRegex()
        {
            Assert.IsTrue(new RegexValidation().ValidateCnpj(_validCnpj));
            Assert.IsTrue(new RegexValidation().ValidatePhone(_validPhone));
        }
    }
}
