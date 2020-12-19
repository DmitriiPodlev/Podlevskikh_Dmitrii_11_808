using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Autotests;

namespace AddressBookTestDataGenerators
{
    class Program
    {
        static void Main(string[] args)
        {
            string type = args[0];
            int count = Convert.ToInt32(args[1]);
            string filename = args[2];
            string format = args[3];
            if (type == "notes")
            {
                GenerateForGroups(count, filename, format);
            }
            else if (type == "editnotes")
            {
                GenerateForEditNotes(count, filename, format);
            }
            else if (type == "users")
            {
                GenerateForUsers(count, filename, format);
            }
            else
            {
                System.Console.Out.Write("Unrecognized type of data" + type);
            }
        }

        static void GenerateForGroups(int count, string filename, string format)
        {
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < count; i++)
            {
                nodes.Add(new Node(GenerateRandomString()));
            }
            StreamWriter writer = new StreamWriter(filename);
            if (format == "xml")
            {
                WriteGroupsToXmlFile(nodes, writer);
            }
            else
            {
                System.Console.Out.Write("Unrecognized format" + format);
            }
            writer.Close();
        }

        static void GenerateForEditNotes(int count, string filename, string format)
        {
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < count; i++)
            {
                nodes.Add(new Node("Новый текст, редакция!"));
            }
            StreamWriter writer = new StreamWriter(filename);
            if (format == "xml")
            {
                WriteGroupsToXmlFile(nodes, writer);
            }
            else
            {
                System.Console.Out.Write("Unrecognized format" + format);
            }
            writer.Close();
        }

        static void GenerateForUsers(int count, string filename, string format)
        {
            List<User> users = new List<User>();
            for (int i = 0; i < count; i++)
            {
                users.Add(new User("dimasik33", "dimasik33junior"));
            }
            StreamWriter writer = new StreamWriter(filename);
            if (format == "xml")
            {
                WriteUsersToXmlFile(users, writer);
            }
            else
            {
                System.Console.Out.Write("Unrecognized format" + format);
            }
            writer.Close();
        }

        static void WriteGroupsToXmlFile(List<Node> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<Node>)).Serialize(writer, groups);
        }

        static void WriteUsersToXmlFile(List<User> users, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<User>)).Serialize(writer, users);
        }

        private static string GenerateRandomString()
        {
            return "Новый текст";
        }
    }
}
