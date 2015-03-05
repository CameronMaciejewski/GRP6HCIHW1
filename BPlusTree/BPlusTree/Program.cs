using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace BPlusTree
{
    class Program
    {
        static User[] usersById;
        static User[] usersByLocation;
        static Message[] messagesByTime;
        static Tree<int> messagesTree10;
        static Tree<string> usersByLocationTree10;
        static Tree<int> usersByIdTree10;
        static Tree<int> messagesTree200;
        static Tree<int> usersByIdTree200;
        static Tree<string> usersByLocationTree200;
        static void Main(string[] args)
        {
            string messagesDirectory;
            string usersByIdDirectory;
            string usersByLocationDirectory;

            do
            {
                messagesDirectory = getDirectory("messages", "time");
            } while (messagesDirectory == "-1");

            messagesTree10 = generateMessagesTree(messagesDirectory, 10);
            messagesTree200 = generateMessagesTree(messagesDirectory, 200);

            do
            {
                usersByIdDirectory = getDirectory("users", "Id");
            } while (usersByIdDirectory == "-1");

            usersByIdTree10 = generateUserByIdTree(usersByIdDirectory, 10);
            usersByIdTree200 = generateUserByIdTree(usersByIdDirectory, 200);

            do
            {
                usersByLocationDirectory = getDirectory("users", "location");
            } while (usersByLocationDirectory == "-1");

            usersByLocationTree10 = generateUserByLocationTree(usersByLocationDirectory, 10);
            usersByLocationTree200 = generateUserByLocationTree(usersByLocationDirectory, 200);

            string selector;
            do {
                Console.WriteLine("Enter the number corresponding to the function you wish to perform:");

 
                selector = Console.ReadLine();

                switch (selector)
                {
                    case "1":
                        // leftmost is 1111
                        // rightmost is 1147
                        getUsersFromNebraska(10);
                        break;
                    case "2":
                        // leftmost is 32751
                        // rightmost is 36940
                        getUsersWhoSentMessagesFromEightToNine(10);
                        break;
                    default:
                        break;
                }
            } while (selector != "9");
            

        }
        public static string getDirectory(string type, string sortedBy)
        {
            Console.WriteLine("Please enter the directory of the " + type + " you wish to put into a Bplus Tree sorted by " + sortedBy + " (ex: C:/Users/John/Documents/Files): ");
            string filePath = "C:/Users/Aaron/Downloads/data/data/";
            filePath += Console.ReadLine();
            if (Directory.Exists(filePath) && Directory.GetFiles(filePath).Length != 0)
            {
                return filePath;
            }
            else if (!Directory.Exists(filePath))
            {
                Console.WriteLine("There is no directory at " + filePath);
                return "-1";
            }
            else
            {
                Console.WriteLine("There are no files in the directory " + filePath);
                return "-1";
            }
        }

        public static Tree<string> generateUserByLocationTree(string directory, int fanOut)
        {
            IEnumerable<string> fileNames = Directory.EnumerateFiles(directory);
            string[] fileNamesArray = fileNames.ToArray<string>();
            List<User> users = new List<User>();
            string[] locations = new string[fileNamesArray.Length];
            for (int i = 0; i < fileNamesArray.Count(); i++)
            {
                string userText = File.ReadAllText(fileNamesArray[i]);
                users.Add(JsonConvert.DeserializeObject<User>(userText));
                locations[i] = users[i].state;
            }
            if (usersByLocation == null || usersByLocation.Length == 0)
            {
                usersByLocation = users.ToArray();
            }

            Tree<string> tree = new Tree<string>(fanOut);
            tree.createTree(locations);
            return tree;
        }

        public static Tree<int> generateUserByIdTree(string directory, int fanOut)
        {
            IEnumerable<string> fileNames = Directory.EnumerateFiles(directory);
            string[] fileNamesArray = fileNames.ToArray<string>();
            List<User> users = new List<User>();
            int[] ids = new int[fileNamesArray.Length];
            for (int i = 0; i < fileNamesArray.Count(); i++)
            {
                string userText = File.ReadAllText(fileNamesArray[i]);
                users.Add(JsonConvert.DeserializeObject<User>(userText));
                ids[i] = users[i].id;
            }
            if (usersById == null || usersById.Length == 0)
            {
                usersById = users.ToArray();
            }

            Tree<int> tree = new Tree<int>(fanOut);
            tree.createTree(ids);
            return tree;
        }

        public static Tree<int> generateMessagesTree(string directory, int fanOut)
        {
            IEnumerable<string> fileNames = Directory.EnumerateFiles(directory);
            string[] fileNamesArray = fileNames.ToArray<string>();
            List<Message> messages = new List<Message>();
            int[] times = new int[fileNamesArray.Length];
            for (int i = 0; i < fileNamesArray.Count(); i++)
            {
                string userText = File.ReadAllText(fileNamesArray[i]);
                messages.Add(JsonConvert.DeserializeObject<Message>(userText));
                times[i] = messages[i].hour*100 + messages[i].minute;
            }
            if (messagesByTime == null || messagesByTime.Length == 0 )
            {
                messagesByTime = messages.ToArray();
            }

            Tree<int> tree = new Tree<int>(fanOut);
            tree.createTree(times);
            return tree;

        }

        public static List<User> getUsersFromNebraska(int fanOut)
        {
            Tree<string> userTree;
            if (fanOut == 10)
            {
                userTree = usersByLocationTree10;
            }
            else
            {
                userTree = usersByLocationTree200;
            }
            int rightIndex = userTree.findRightMostItem("Nebraska");
            int leftIndex = userTree.findLeftMostItem("Nebraska");
            List<User> users = new List<User>();
            for (int i = leftIndex; i <= rightIndex; i++)
            {
                users.Add(usersByLocation[i]);
            }
            return users;
        }

        public static List<User> getUsersWhoSentMessagesFromEightToNine(int fanOut)
        {
            Tree<int> messagesTree;
            if (fanOut == 10)
            {
                messagesTree = messagesTree10;
            }
            else 
            {
                messagesTree = messagesTree200;
            }
            int rightIndex = messagesTree.findRightMostItem(900);
            int leftIndex = messagesTree.findLeftMostItem(800);
            int[] userIDs = new int[rightIndex - leftIndex + 1];
            
            List<User> users = new List<User>();
            for (int i = leftIndex; i <= rightIndex; i++)
            {
                User newUser = usersById[messagesByTime[i].user_id];
                if (!users.Contains(newUser)) {
                    users.Add(usersById[messagesByTime[i].user_id]);
                }
            }
            return users;
        }

        public static List<User> getUsersWhoSentMessagesFromEightToNineFromNebraska()
        {
            return new List<User>();
        }

        public static User getUserWhoSentMostMessagesFromEightToNineInNebraska()
        {
            return new User();
        }
    }
}