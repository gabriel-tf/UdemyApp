using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace UdemyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CallThread();
                //ExtensionMethod();
                //Serialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CallThread()
        {
            Thread t1 = new Thread(ThreadRepeat);
            t1.Start();

            for (int i = 0; i < 100; i++)
                Console.WriteLine("CallThread: " + i);

            Console.ReadKey();
        }

        public static void ThreadRepeat()
        {
            for (int i = 0; i < 100; i++)
                Console.WriteLine("ThreadRepeat: " + i);
        }

        public static void ExtensionMethod()
        {
            var teste = "";
            Console.WriteLine(teste.Teste());
            Console.ReadKey();
        }

        public static void Serialize()
        {
            //JSON
            Usuario usuarioJson = new Usuario() { Nome = "Gabriel", Email = "meuemail@gmail.com", Telefone = "9999-9999" };
            string jPath = @"C:\Users\Fitbank\Desktop\UsuarioJSON.txt";

            if (!File.Exists(jPath))
            {
                //Serializa JSON
                string stringObjUsuario = JsonConvert.SerializeObject(usuarioJson);
                StreamWriter sWriter = new StreamWriter(jPath);
                sWriter.WriteLine(stringObjUsuario);
                sWriter.Close();
                Console.WriteLine("Objeto JSON serializado! Verifique o arquivo.");
                Console.ReadKey();
            }
            else
            {
                //Desserializa JSON
                StreamReader sReader = new StreamReader(jPath);
                string stringObjUsuario = sReader.ReadToEnd();
                Usuario novoUsuario = JsonConvert.DeserializeObject<Usuario>(stringObjUsuario);
                Console.WriteLine("Objeto JSON deserializado!");
                Console.WriteLine("Nome: {0}, Email: {1} e Telefone: {2}", novoUsuario.Nome, novoUsuario.Email, novoUsuario.Telefone);
                Console.ReadKey();
            }

            //XML 
            Usuario usuarioXml = new Usuario() { Nome = "Gabriel", Email = "meuemail@gmail.com", Telefone = "9999-9999" };
            string xPath = @"C:\Users\Fitbank\Desktop\UsuarioXML.txt";

            XmlSerializer xSerializer = new XmlSerializer(typeof(Usuario));

            if (!File.Exists(xPath))
            {
                //Serializa XML
                StreamWriter sWriter = new StreamWriter(xPath);
                xSerializer.Serialize(sWriter, usuarioXml);
                Console.WriteLine("Objeto XML serializado! Verifique o arquivo.");
                Console.ReadKey();
            }
            else
            {
                //Desserializa XML
                StreamReader sReader = new StreamReader(xPath);
                Usuario novoUsuario = (Usuario)xSerializer.Deserialize(sReader);
                Console.WriteLine("Objeto XML deserializado!");
                Console.WriteLine("Nome: {0}, Email: {1} e Telefone: {2}", novoUsuario.Nome, novoUsuario.Email, novoUsuario.Telefone);
                Console.ReadKey();
            }
        }
    }
}
