using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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
            Stopwatch stopWatch = new Stopwatch();
            string messagesDirectory;
            string usersByIdDirectory;
            string usersByLocationDirectory;

            do
            {
                messagesDirectory = getDirectory("messages", "time");
            } while (messagesDirectory == "-1");

            stopWatch.Start();
            messagesTree10 = generateMessagesTree(messagesDirectory, 10);
            stopWatch.Stop();
            Console.WriteLine("Messages Tree with fanout of 10 Generation took: {0}", stopWatch.Elapsed);
            stopWatch.Reset();           
            stopWatch.Start();
            messagesTree200 = generateMessagesTree(messagesDirectory, 200);
            stopWatch.Stop();
            Console.WriteLine("Messages Tree with fanout of 200 Generation took: {0}", stopWatch.Elapsed);
            stopWatch.Reset();

            do
            {
                usersByIdDirectory = getDirectory("users", "Id");
            } while (usersByIdDirectory == "-1");

            stopWatch.Start();
            usersByIdTree10 = generateUserByIdTree(usersByIdDirectory, 10);
            stopWatch.Stop();
            Console.WriteLine("User by Id Tree with fanout of 10 Generation took: {0}", stopWatch.Elapsed);
            stopWatch.Reset();
            stopWatch.Start();
            usersByIdTree200 = generateUserByIdTree(usersByIdDirectory, 200);
            stopWatch.Stop();
            Console.WriteLine("User by Id Tree with fanout of 200 Generation took: {0}", stopWatch.Elapsed);
            stopWatch.Reset();

            do
            {
                usersByLocationDirectory = getDirectory("users", "location");
            } while (usersByLocationDirectory == "-1");

            stopWatch.Start();
            usersByLocationTree10 = generateUserByLocationTree(usersByLocationDirectory, 10);
            stopWatch.Stop();
            Console.WriteLine("User by location Tree with fanout of 10 Generation took: {0}", stopWatch.Elapsed);
            stopWatch.Reset();
            stopWatch.Start();
            usersByLocationTree200 = generateUserByLocationTree(usersByLocationDirectory, 200);
            stopWatch.Stop();
            Console.WriteLine("User by location Tree with fanout of 200 Generation took: {0}", stopWatch.Elapsed);
            stopWatch.Reset();

            string selector;
            do {
                Console.WriteLine("Enter the number corresponding to the function you wish to perform:");

                Console.WriteLine("Get Users from Nebraska, fanout = 10: Enter 1");
                Console.WriteLine("Get Users from Nebraska, fanout = 200: Enter 2");
                Console.WriteLine("Get Users that sent messages from 8am to 9am, fanout = 10: Enter 3");
                Console.WriteLine("Get Users that sent messages from 8am to 9am, fanout = 200: Enter 4");
                Console.WriteLine("Get Users that sent messages from 8am to 9am from Nebraska, fanout = 10: Enter 5");
                Console.WriteLine("Get Users that sent messages from 8am to 9am from Nebraska, fanout = 200: Enter 6");
                Console.WriteLine("Get User from nebraska that sent the most messages from 8am to 9am, fanout = 10: Enter 7");
                Console.WriteLine("Get User from nebraska that sent the most messages from 8am to 9am, fanout = 200: Enter 8");
                Console.WriteLine("Exit Application: Enter 9");
                selector = Console.ReadLine();
                
                List<User> users;
                User user;
                switch (selector)
                {
                    case "1":
                        stopWatch.Start();
                        users = getUsersFromNebraska(10);
                        stopWatch.Stop();
                        Console.WriteLine("Number of Users: {0}, Time elapsed: {1}", users.Count, stopWatch.Elapsed);
                        Console.WriteLine("");
                        Console.WriteLine("");
                        stopWatch.Reset();
        
                        break;
                    case "2":
                        stopWatch.Start();
                        users = getUsersFromNebraska(200);
                        stopWatch.Stop();
                        Console.WriteLine("Number of Users: {0}, Time elapsed: {1}", users.Count, stopWatch.Elapsed);
                        Console.WriteLine("");
                        Console.WriteLine("");
                        stopWatch.Reset();
                        break;
                    case "3":
                        stopWatch.Start();
                        users = getUsersWhoSentMessagesFromEightToNine(10);
                        stopWatch.Stop();
                        Console.WriteLine("Number of Users: {0}, Time elapsed: {1}", users.Count, stopWatch.Elapsed);
                        Console.WriteLine("");
                        Console.WriteLine("");
                        stopWatch.Reset();                       
                        break;
                    case "4":
                        stopWatch.Start();
                        users = getUsersWhoSentMessagesFromEightToNine(200);
                        stopWatch.Stop();
                        Console.WriteLine("Number of Users: {0}, Time elapsed: {1}", users.Count, stopWatch.Elapsed);
                        Console.WriteLine("");
                        Console.WriteLine("");
                        stopWatch.Reset();   
                        break;
                    case "5":
                        stopWatch.Start();
                        users = getUsersWhoSentMessagesFromEightToNineFromNebraska(10);
                        stopWatch.Stop();
                        Console.WriteLine("Number of Users: {0}, Time elapsed: {1}", users.Count, stopWatch.Elapsed);
                        Console.WriteLine("");
                        Console.WriteLine("");
                        stopWatch.Reset();                          
                        break;
                    case "6":
                        stopWatch.Start();
                        users = getUsersWhoSentMessagesFromEightToNineFromNebraska(200);
                        stopWatch.Stop();
                        Console.WriteLine("Number of Users: {0}, Time elapsed: {1}", users.Count, stopWatch.Elapsed);
                        Console.WriteLine("");
                        Console.WriteLine("");
                        stopWatch.Reset();
                        break;
                    case "7":
                        stopWatch.Start();
                        user = getUserWhoSentMostMessagesFromEightToNineInNebraska(10);
                        stopWatch.Stop();
                        Console.WriteLine("User who has sent the most (name, id): {0}, {1}, Time elapsed: {2}", user.name, user.id, stopWatch.Elapsed);
                        Console.WriteLine("");
                        Console.WriteLine("");
                        stopWatch.Reset();                        
                        break;
                    case "8":
                        stopWatch.Start();
                        user = getUserWhoSentMostMessagesFromEightToNineInNebraska(200);
                        stopWatch.Stop();
                        Console.WriteLine("User who has sent the most (name, id): {0}, {1}, Time elapsed: {2}", user.name, user.id, stopWatch.Elapsed);
                        Console.WriteLine("");
                        Console.WriteLine("");
                        stopWatch.Reset();
                        break;
                    default:
                        break;
                }
            } while (selector != "9");
            

        }
        public static string getDirectory(string type, string sortedBy)
        {
            Console.WriteLine("Please enter the directory of the " + type + " you wish to put into a Bplus Tree sorted by " + sortedBy + " (ex: C:/Users/John/Documents/Files): ");
            string filePath = "C:/Users/Cameron/Documents/TestData/data/";
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

        public static List<User> getUsersWhoSentMessagesFromEightToNineFromNebraska(int fanOut)
        {
            List<User> usersFrom8to9 = getUsersWhoSentMessagesFromEightToNine(fanOut);
            List<int> usersIdsFrom8to9 = new List<int>();
            foreach (User user in usersFrom8to9)
            {
                usersIdsFrom8to9.Add(user.id);
            }
            List<User> usersFromNebraska = getUsersFromNebraska(fanOut);
            List<User> users = new List<User>();

            for (int i = 0; i < usersFromNebraska.Count; i++)
            {
                if (usersIdsFrom8to9.Contains(usersFromNebraska[i].id))
                {
                    users.Add(usersFrom8to9[i]);
                }
            }
            return users;

        }

        public static User getUserWhoSentMostMessagesFromEightToNineInNebraska(int fanOut)
        {
            List<User> usersFrom8to9 = new List<User>();
            Dictionary<int, int> messageCount = new Dictionary<int, int>();
            List<User> usersFromNebraska = getUsersFromNebraska(fanOut);
            List<int> userIdsFromNebraska = new List<int>();
            foreach (User user in usersFromNebraska)
            {
                userIdsFromNebraska.Add(user.id);
            }
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
            for (int i = leftIndex; i <= rightIndex; i++)
            {                
                
                User newUser = usersById[messagesByTime[i].user_id];
                if (userIdsFromNebraska.Contains(newUser.id))
                {               
                    if (!usersFrom8to9.Contains(newUser))
                    {
                        usersFrom8to9.Add(newUser);
                        messageCount.Add(newUser.id, 0);
                        messageCount[newUser.id] += 1;
                    }
                    else
                    {
                        messageCount[newUser.id] += 1;
                    }
                }
                
            }

            KeyValuePair<int, int> maxMessages = messageCount.ElementAt(0);
            foreach (KeyValuePair<int, int> userCount in messageCount )
            {
                if (userCount.Value > maxMessages.Value)
                {
                    maxMessages = userCount;
                }
            }

            return usersById[maxMessages.Key];

        }
    }
}