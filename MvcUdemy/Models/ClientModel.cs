using MvcUdemy.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MvcUdemy.Models
{
    public class ClientModel
    {
        //TODO: Persist in database and stop saving data in JSON, keep it only temporarily
        private string _jsonPath = @"C:\Users\Fitbank\Desktop\JsonOfClients.json";
        private RegexValidation _regex;

        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }


        public ClientResponse CreateClient(ClientModel newClient)
        {
            try
            {
                if (!VerifyIfClientIsValid(newClient).Success)
                    return VerifyIfClientIsValid(newClient);

                var clients = GetClients();
                clients = UpdateClientIfExists(clients, newClient);

                return SaveClientsOnJson(clients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ClientResponse() { Success = false, Message = ex.Message };
            }
        }

        public ClientResponse VerifyIfClientIsValid(ClientModel client)
        {
            _regex = new RegexValidation();

            if (!_regex.ValidateCnpj(client.Cnpj))
                return new ClientResponse() { Success = false, Message = "Invalid Cnpj format." };

            if (!_regex.ValidatePhone(client.Phone))
                return new ClientResponse() { Success = false, Message = "Phone format invalid." };

            return new ClientResponse() { Success = true, Message = string.Empty };
        }

        public List<ClientModel> UpdateClientIfExists(List<ClientModel> clients, ClientModel newClient)
        {
            var client = clients.SingleOrDefault(x => x.Cnpj.Equals(newClient.Cnpj));

            if (client != null)
            {
                client.Name = newClient.Name;
                client.Phone = newClient.Phone;
            }
            else
                clients.Add(newClient);

            return clients;
        }

        public List<ClientModel> GetClients()
        {
            var clients = new List<ClientModel>();
            if (File.Exists(_jsonPath))
            {
                StreamReader sReader = new StreamReader(_jsonPath);
                string strClientsOfFile = sReader.ReadToEnd();
                sReader.Close();
                clients = JsonConvert.DeserializeObject<List<ClientModel>>(strClientsOfFile);
            }

            return clients;
        }

        public ClientResponse SaveClientsOnJson(List<ClientModel> clients)
        {
            try
            {
                string strClients = JsonConvert.SerializeObject(clients);
                File.WriteAllText(_jsonPath, strClients);

                return new ClientResponse() { Success = true, Message = "Success." };
            }
            catch (Exception)
            {
                return new ClientResponse() { Success = false, Message = "Something went wrong." };
            }
        }

        public ClientResponse RemmoveClientOfJson(ClientModel clientToRemove)
        {
            var clients = GetClients();
            clients.RemoveAll(x => x.Cnpj.Equals(clientToRemove.Cnpj));
            var response = SaveClientsOnJson(clients);
            
            if (response.Success)
                return new ClientResponse() { Success = true, Message = "Client removed." };

            return response;
        }
    }
}
