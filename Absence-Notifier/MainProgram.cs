using System;
using System.IO;
using static GlobalVariables.Variables;

public static class MainProgram
{

    public static void Start()
    {
        bool running = true;
        while (running)
        {
            //Console.Clear();
            VerifyLastLogin();
            Console.WriteLine("Daily login system.");
            Console.WriteLine("Made by ManuEcheveste");
            Console.WriteLine("");
            Console.WriteLine("Current Date: " + today);
            Console.Write("Last login: ");
            if (lastLoginText == "Never")
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(lastLoginText);
            Console.ForegroundColor = ConsoleColor.White;
            if (lastLoginText != "Today" && lastLoginText != "Never")
                Console.WriteLine("How many days since last login: " + daysSinceLastLogin);

            if (!File.Exists(emailAccount))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Email has not been set. Please enter setup to configure it.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (!File.Exists(configFile))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Reminder and absence dates have not been set. Please enter setup to configure it.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (!File.Exists(contactsSubjectFile))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The subject for your email has not been set. Please enter setup to configure it.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (!File.Exists(contactsMessageFile))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The message for your email has not been set. Please enter setup to configure it.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (!File.Exists(specialContactsSubjectFile))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The subject for your special contacts email has not been set. Please enter setup to configure it.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (!File.Exists(specialContactsMessageFile))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The message for your special contacts email has not been set. Please enter setup to configure it.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("");
            Console.Write("Enter command: ");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "login":
                    Login();
                    break;
                case "setup":
                    Setup();
                    break;
                case "help":
                    //Console.Clear();
                    Console.WriteLine("login - Registers today session");
                    Console.WriteLine("setup - Enters setup menu");
                    Console.WriteLine("help - Shows this help");
                    Console.WriteLine("exit - Exits from the program");
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Command not found. Type 'help' to show commands list.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
            //Console.ReadKey();
        }
    }

    static void Login()
    {
        Console.WriteLine("Do you want to register today session with date " + today + "? (y/n)");
        bool running = true;
        while (running)
        {
            string userInput = Console.ReadLine();
            if (userInput == "y" || userInput == "Y" || userInput == "s" || userInput == "S")
            {
                Directory.CreateDirectory(loginDir);
                File.WriteAllText(loginFile, $"{today}");
                Console.WriteLine("The registry was saved with date " + today);
                running = false;
            }
            else if (userInput == "n" || userInput == "N")
            {
                Console.WriteLine("No registry was made.");
                running = false;
            }
            else
            {
                Console.WriteLine("Please try again.");
            }
            Console.ReadKey();
            Console.Clear();
        }
    }


    static void Setup()
    {
        Console.Clear();
        bool running = true;
        while (running)
        {
            Console.WriteLine("Setup");
            Console.WriteLine("");

            Console.Write("Enter command: ");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "email":
                    ConfigEmail();
                    break;
                case "days":
                    SetupDates();
                    break;
                case "reset":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Are you sure that you want to ERASE all of the information of the program?");
                    Console.WriteLine("This action is IRREVERSIBLE and the program will reset to its default settings.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Erase all data? (y/n): ");
                    string delInput = Console.ReadLine();
                    if (delInput == "y" || delInput == "Y" || delInput == "s" || delInput == "S")
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Are you REALLY sure?");
                        Console.WriteLine("Once you confirm, ALL DATA WILL BE DELETED FOREVER.");
                        Console.Write("Really erase all data? (y/n): ");
                        Console.ForegroundColor = ConsoleColor.White;
                        string confirmInput = Console.ReadLine();
                        if (confirmInput == "y" || confirmInput == "Y" || confirmInput == "s" || confirmInput == "S")
                        {
                            Console.WriteLine("");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Erasing all data...");
                            Directory.Delete(loginDir, true);
                            Directory.Delete(emailDir, true);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("All data has been erased. Press any key to exit the program.");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                        else if (confirmInput == "n" || confirmInput == "N")
                        {
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Aborting.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                    else if (delInput == "n" || delInput == "N")
                    {
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Aborting.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    break;
                case "help":
                    Console.WriteLine("email - Enter email setup");
                    Console.WriteLine("days - Change the days when you'll receive a reminder to log in and when you'll notify your contacts of your absence.");
                    Console.WriteLine("reset - Erases all of the program data.");
                    Console.WriteLine("help - Shows this help.");
                    Console.WriteLine("back - Returns to the previous menu.");
                    break;
                case "back":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Command not found. Type 'help' to show the commands list.");
                    break;
            }
        }
        Console.Clear();
    }

    #region Email Config
    static void ConfigEmail()
    {
        Console.Clear();
        bool running = true;
        while (running)
        {
            Console.WriteLine("Email Configuration");
            Console.WriteLine("");

            Console.Write("Enter option: ");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "setup":
                    SetupEmail();
                    break;

                case "test":
                    if (File.Exists(emailAccount))
                    {
                        LoginEmail();
                        Console.Write("Enter the email address you want to send the test email (leave empty to send to yourself): ");
                        string sendTo = Console.ReadLine();
                        if (sendTo == "")
                            sendTo = personalEmail;
                        EmailHandler.SendTestEmail(sendTo);
                        Console.WriteLine("Email sent. Please check your inbox to verify.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("You need to setup your email first with 'setup' before you can test it.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        Console.Clear();
                    }

                    break;

                case "add-contacts":
                    if (File.Exists(emailAccount))
                    {
                        Console.WriteLine("Enter the email addresses.");
                        Console.WriteLine("Type 'done' to exit.");
                        bool adding = true;
                        int added = 0;
                        while (adding)
                        {
                            string addedRecipient = Console.ReadLine();
                            if (addedRecipient != "done")
                            {
                                File.AppendAllText(contactsList, addedRecipient + Environment.NewLine);
                                added++;
                            }
                            else
                            {
                                adding = false;
                            }
                        }
                        Console.WriteLine("You added " + added + " new contacts.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("You need to setup your email first with 'setup' before you can add contacts.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        Console.Clear();
                    }

                    break;

                case "add-special-contacts":
                    if (File.Exists(emailAccount))
                    {
                        Console.Write("Enter the special email address: ");
                        specialContacts = Console.ReadLine();
                        File.WriteAllText(specialContactsList, specialContacts);
                        Console.WriteLine("Added " + specialContacts + " to the Special Recepients list.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("You need to setup your email first with 'setup' before you can add special contacts.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        Console.Clear();
                    }
                    break;

                case "subject":
                    Console.Write("Type the subject for the email sent to your contacts: ");
                    string subject = Console.ReadLine();
                    File.WriteAllText(contactsSubjectFile, subject);
                    Console.WriteLine("Saved your subject.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "message":
                    Console.Write("Type the message for the email sent to your contacts: ");
                    string message = Console.ReadLine();
                    File.WriteAllText(contactsMessageFile, message);
                    Console.WriteLine("Saved your message.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "special-subject":
                    Console.Write("Type the subject for the email sent to your special contacts: ");
                    string specialSubject = Console.ReadLine();
                    File.WriteAllText(specialContactsSubjectFile, specialSubject);
                    Console.WriteLine("Saved your special subject.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "special-message":
                    Console.Write("Type the message for the email sent to your special contacts: ");
                    string specialMessage = Console.ReadLine();
                    File.WriteAllText(specialContactsMessageFile, specialMessage);
                    Console.WriteLine("Saved your special message.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "back":
                    running = false;
                    break;

                case "cancel":
                    running = false;
                    break;
                case "help":
                    Console.Clear();
                    Console.WriteLine("setup - Setup your email account.");
                    Console.WriteLine("test - Test that the program can send emails with your account.");
                    Console.WriteLine("add-contacts - Add email addresses to your contacts list.");
                    Console.WriteLine("subject - Change the subject of the mail sent to your contacts.");
                    Console.WriteLine("message - Change the message of the mail sent to your contacts.");
                    Console.WriteLine("add-special-contacts - Add email addresses to your special contacts list.");
                    Console.WriteLine("special-subject - Change the subject of the mail sent to your special contacts.");
                    Console.WriteLine("special-message - Change the message of the mail sent to your special contacts.");
                    Console.WriteLine("back - Returns to the previous menu.");
                    Console.WriteLine("help - Shows this help");
                    break;
                default:
                    Console.WriteLine("Command not found. Type 'help' to show commands list.");
                    break;
            }
        }
        Console.Clear();
    }

    static void SetupEmail()
    {
        Console.Clear();
        Console.WriteLine("Please enter your email address");
        personalEmail = Console.ReadLine();
        string email = "email=" + personalEmail;
        Console.WriteLine("Please enter your email password");
        emailPassword = Console.ReadLine();
        string psw = "psw=" + emailPassword;

        Directory.CreateDirectory(emailDir);
        File.WriteAllText(emailAccount, email + Environment.NewLine + psw);

        Console.WriteLine("Your email account has been saved.");
        Console.Write("Do you want to send a test email? (y/n): ");

        bool running = true;
        while (running)
        {
            string userInput = Console.ReadLine();
            if (userInput == "y" || userInput == "Y" || userInput == "s" || userInput == "S")
            {
                Console.Write("Enter the email address you want to send the test email (leave empty to send to yourself): ");
                string sendTo = Console.ReadLine();
                if (sendTo == "")
                    sendTo = personalEmail;
                EmailHandler.SendTestEmail(sendTo);
                Console.WriteLine("Email sent. Please check your inbox to verify.");
                running = false;
            }
            else if (userInput == "n" || userInput == "N")
            {
                running = false;
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Please try again.");
            }
        }
    }
    #endregion

    static void SetupDates()
    {
        Console.Clear();
        Console.Write("Enter when you would like to be reminded to log in: ");
        string reminder = "reminder=" + Console.ReadLine();
        Console.Write("Enter when you would like to notify your contacts of your absence: ");
        string absence = "absence=" + Console.ReadLine();
        if (!Directory.Exists(emailDir))
            Directory.CreateDirectory(emailDir);
        File.WriteAllText(configFile, reminder + Environment.NewLine + absence);
    }
}
