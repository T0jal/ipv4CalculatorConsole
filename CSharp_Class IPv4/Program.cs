using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharp_Class_IPv4
{
    class Program
    {
        #region List, ReadValue, Exit & InvalidOption

        static List<IPv4> listFavouriteIPv4 = new List<IPv4>();
        static List<IPv4> listIpInUse = new List<IPv4>();
        static int readValue()
        /* This method allows the program to continue running by "catching" a character with an invalid format (like a char or string, when it sohlud be an int) inserted by the user.*/
        {
            int val = 0;
            bool flag = false;
            do
            {
                try
                {
                    val = int.Parse(Console.ReadLine());
                    flag = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect value, try again.");
                    flag = false;
                }
            } while (!flag);
            return val;
        }
        static void exit()
        {
        /* This method closes the program from the main menu.*/
            Console.Clear();

            StreamWriter wr = new StreamWriter(@"FAVOURITEIPV4.txt", false); // false = rewrites the whole file
            foreach (IPv4 iP in listFavouriteIPv4)
                wr.WriteLine(iP);
            wr.Close();

            Console.WriteLine("See you later alligator!");
            System.Threading.Thread.Sleep(1000);
            Environment.Exit(0);
        }

        static void invalidOption()
        /* This method alerts the user that he selected an invalid option in any of the menus.*/
        {
            Console.WriteLine("Invalid option!");
        }
        #endregion

        #region Menus
        static int mainMenu()
        /* This is the main menu that the user sees when the program starts and allows different options in terms of picking an IP to use in the rest of the program.*/
        {
            Console.Clear();
            Console.WriteLine("****WELCOME TO THE IPv4 CALCULATOR******");
            Console.WriteLine();
            Console.WriteLine("*****************MENU*******************");
            Console.WriteLine(" Which option would you like to choose?");
            Console.WriteLine(" 1 - Manually insert an IPv4 address.");
            Console.WriteLine(" 2 - Select an IPv4 address from my favourite's list.");
            Console.WriteLine(" 3 - Select an IPv4 address from my favourite's list to delete.");
            Console.WriteLine(" 4 - Exit.");
            Console.WriteLine();
            Console.Write("Option: ");
            return readValue();
        }
        static void auxiliaryMenu()
        /* This is the auxiliary menu which is offers various options of what to do with the IPs, after they have been introduced into the program earlier*/
        {
            int flag = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(" Which option would you like to choose?");
                Console.WriteLine(" 1 - Show the class of the IPv4 address.");
                Console.WriteLine(" 2 - Check if the Ipv4 address is public or private.");
                Console.WriteLine(" 3 - Test the connectivity between the added IPv4 address and another IPv4 address.");
                Console.WriteLine(" 4 - Save this IPv4 address as a favourite IPv4 address.");
                Console.WriteLine(" 5 - Go back to the main Menu.");
                Console.WriteLine();
                Console.Write("Option: ");

                int op = int.Parse(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        showClass();
                        break;
                    case 2:
                        showPrivateOrPublic();
                        break;
                    case 3:
                        showConnectivity();
                        break;
                    case 4:
                        saveFavouriteIP();
                        break;
                    case 5:
                        mainMenu();
                        flag = 1;
                        break;
                    default:
                        invalidOption();
                        break;
                }
            } while (flag == 0);
        }
        #endregion

        #region Manually Add an IPv4
        static void newIPv4()
        {
            listIpInUse.Clear();
            int flag = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Please insert the IPv4 Adress: ");
                Console.Write("First Octet: ");
                int num1 = int.Parse(Console.ReadLine());
                Console.Write("Second Octet: ");
                int num2 = int.Parse(Console.ReadLine());
                Console.Write("Third Octet: ");
                int num3 = int.Parse(Console.ReadLine());
                Console.Write("Forth Octet: ");
                int num4 = int.Parse(Console.ReadLine());
                Console.Write("Network Mask: ");
                int netMask = int.Parse(Console.ReadLine());

                IPv4 i = new IPv4(num1, num2, num3, num4, netMask);

                if (i.validateIPv4() == true)
                {
                    flag = 1;
                    listIpInUse.Add(new IPv4(i));
                    Console.WriteLine($"\nYour IPv4 is valid and you can see if here: {i.printToConsole()}");
                }
                else
                {
                    Console.WriteLine("\nInvalid IPv4.");
                    Console.WriteLine("\nPress any key to repeat.");
                    Console.ReadKey();
                }
            } while (flag == 0);

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            auxiliaryMenu();
        }

        static void showClass()
        {
            Console.Clear();
            foreach (IPv4 IP in listIpInUse)
                Console.WriteLine($"The class of this IPv4 is: {IP.checkClassofIPv4()}");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void showPrivateOrPublic()
        {
            Console.Clear();
            foreach (IPv4 IP in listIpInUse)
                Console.WriteLine($"This IPv4 is: {IP.checkPrivateOrPublic()}");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void showConnectivity()
        /* Asks for a new IP and then compares it with the Ip that the user inserted before.*/
        {
            Console.Clear();
            Console.WriteLine("Please insert the new IPv4 Adress: ");
            Console.Write("First Octet: ");
            int num1 = int.Parse(Console.ReadLine());
            Console.Write("Second Octet: ");
            int num2 = int.Parse(Console.ReadLine());
            Console.Write("Third Octet: ");
            int num3 = int.Parse(Console.ReadLine());
            Console.Write("Forth Octet: ");
            int num4 = int.Parse(Console.ReadLine());
            Console.Write("Network Mask: ");
            int netMask = int.Parse(Console.ReadLine());

            IPv4 newIp = new IPv4(num1, num2, num3, num4, netMask);

            if (newIp.validateIPv4() == true)
            {
                Console.WriteLine($"\nYour IPv4 is valid and you can see if here: {newIp.printToConsole()}");
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nInvalid IPv4.");
                Console.WriteLine("\nPress any key to repeat.");
                Console.ReadKey();
            }

            Console.Clear();
            foreach (IPv4 userIp in listIpInUse)
            {
                Console.WriteLine(userIp.printToConsole());
                string matches = userIp.checkConnectivity(newIp);
                Console.WriteLine($"The Connectivity test resulted in: {matches}");
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();

            }
        }
        #endregion

        #region Favourite IPv4 Addresses
        static void pickIPv4FromFavourites()
        /* This method reads the .txt file where the favourite IPs are and pastes it to a list. Then checks the list to see if it is empty, in case the .txt file was empty. If it is empty, it skips and informs the user the list 
        is empty, going back to the main menu. If there are ips inside, it shows the ips, asks which one the user wants to use (checking if the chosen ip exists, if it does not exist, it asks the user to choose again), and if it 
        does exist, it pastes it into another list (IpInUse) to be used later in the program. It ends by going into the auxiliary menu. */
        {
            Console.Clear();

            StreamReader rd = new StreamReader(@"FAVOURITEIPV4.txt");
            while (!rd.EndOfStream)
            {
                string line = rd.ReadLine();
                string[] iPv4 = line.Split('.');
                IPv4 i = new IPv4(int.Parse(iPv4[0]), int.Parse(iPv4[1]), int.Parse(iPv4[2]), int.Parse(iPv4[3]), int.Parse(iPv4[4]));
                listFavouriteIPv4.Add(new IPv4(i));
            }
            rd.Close();

            if (listFavouriteIPv4.Count != 0)
            {
                int counter = 0;
                Console.WriteLine("This is the list of saved favourite IPv4:");
                Console.WriteLine();
                foreach (IPv4 IPs in listFavouriteIPv4)
                    Console.WriteLine(++counter + ")\n" + IPs.ToString());

                int flag = 0;
                do {
                    Console.WriteLine("Which IPv4 would you like to choose?");
                    int favIP = int.Parse(Console.ReadLine());

                    if (favIP <= 0 && favIP > listIpInUse.Count())
                    {
                        Console.Clear();
                        Console.WriteLine("That IPv4 does not exist.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        IPv4 f = new IPv4(listFavouriteIPv4[favIP - 1]);
                        listIpInUse.Add(new IPv4(f));
                        Console.WriteLine($"\nThe following IP was selected:\n{f.printToConsole()}");
                        flag = 1;
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        auxiliaryMenu();
                    }
                }while (flag == 0);
            }
            else
            {
                Console.WriteLine("The favourites list is empty.\n");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }

        static void saveFavouriteIP()
        /* This method saves an IP into the favourites list. It does this by "adding all the IPs in the listIpInUse into the listFavouriteIPv4 which is only one at any given time.*/
        {
            Console.Clear();
            foreach (IPv4 favIP in listIpInUse)
                listFavouriteIPv4.Add(new IPv4(favIP));

            Console.WriteLine("You've just saved the IP in your favourites list.\n");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void deleteFavouriteIp()
        {
            Console.Clear();
            int counter = 0;
            Console.WriteLine("IPs in the list:");
            Console.WriteLine();
            foreach (IPv4 iP in listFavouriteIPv4)
                Console.WriteLine(++counter + ")\n" + iP.printToConsole());

            Console.WriteLine("Which IP would you like to delete?");
            int favIp = int.Parse(Console.ReadLine());
            if (favIp <= 0 || favIp > listFavouriteIPv4.Count())
            {
                Console.WriteLine("That key does not exist.");
            }
            else
            {
                listFavouriteIPv4.RemoveAt(favIp - 1);
                Console.WriteLine("IP removed.");
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        #endregion

        static void Main(string[] args)
        {
            int op;
            do
            {
                op = mainMenu();
                switch (op)
                {
                    case 1:
                        newIPv4();
                        break;
                    case 2:
                        pickIPv4FromFavourites();
                        break;
                    case 3:
                        deleteFavouriteIp();
                        break;
                    case 4:
                        exit();
                        break;
                    default:
                        invalidOption();
                        break;
                }
            } while (op != 4);
        }
    }
}