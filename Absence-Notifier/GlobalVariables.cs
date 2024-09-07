using System;
using System.IO;

namespace GlobalVariables
{
    public class Variables
    {
        #region Global Variables

        #region Notification Settings
        public static int reminderDay;
        public static int AbsenceDay;
        #endregion

        #region Login Date Variables
        public static string lastLogin;
        public static DateTime lastLoginDate;

        public static int daysSinceLastLogin;
        public static DateTime todayDate = DateTime.Now.Date;

        public static string lastLoginText;
        public static string today = DateTime.Now.ToString("dd-MM-yy");

        public static bool hasLogedInToday;
        #endregion

        #region Files Variables

        #region Logs
        public static string loginDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        public static string loginFile = Path.Combine(loginDir, "login.log");
        #endregion

        #region Email
        public static string emailDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "email");

        public static string emailAccount = Path.Combine(emailDir, "account.cfg");
        public static string contactsList = Path.Combine(emailDir, "contacts.txt");
        public static string specialContactsList = Path.Combine(emailDir, "special-contacts.txt");

        public static string contactsSubjectFile = Path.Combine(emailDir, "contacts-subject.txt");
        public static string specialContactsSubjectFile = Path.Combine(emailDir, "special-contacts-subject.txt");

        public static string contactsMessageFile = Path.Combine(emailDir, "contacts-message.txt");
        public static string specialContactsMessageFile = Path.Combine(emailDir, "special-contacts-message.txt");

        #endregion

        public static string configFile = Path.Combine(emailDir, "config.cfg"); //Para las fechas

        public static string emailServerSettings = Path.Combine(emailDir, "server.cfg"); //Server config (port)

        #endregion

        #region Email Settings
        public static string personalEmail;
        public static string emailPassword;

        public static string[] contacts;
        public static string specialContacts;

        public static string contactsSubject;
        public static string specialContactsSubject;

        public static string contactsMessage;
        public static string specialContactsMessage;

        #endregion

        #region Email Server
        public static int emailPort;
        public static string emailServerAddress;

        #endregion

        #endregion


        public static void LoadSettings()
        {
            string[] dates = File.ReadAllLines(configFile);
            foreach (string line in dates)
            {
                if (line.StartsWith("reminder="))
                {
                    reminderDay = int.Parse(line.Substring("reminder=".Length));
                }
                else if (line.StartsWith("absence="))
                {
                    AbsenceDay = int.Parse(line.Substring("absence=".Length));
                }
            }
        }

        public static void VerifyLastLogin()
        {
            if (File.Exists(loginFile))
            {
                lastLogin = File.ReadAllText(loginFile).Trim();
                lastLoginDate = DateTime.ParseExact(lastLogin, "dd-MM-yy", null);

                if (lastLoginDate != todayDate)
                {
                    daysSinceLastLogin = (todayDate - lastLoginDate).Days;
                    lastLoginText = lastLogin;                    
                }
                else
                {
                    daysSinceLastLogin = 0;
                    hasLogedInToday = true;
                    lastLoginText = "Today";
                }
            }
            else
            {
                daysSinceLastLogin = 0;
                lastLoginText = "Never";
            }
        }

        public static void LoginEmail()
        {
            string[] serverConfig = File.ReadAllLines(emailServerSettings);
            foreach(string line in serverConfig)
            {
                if (line.StartsWith("address="))
                {
                    emailServerAddress = line.Substring("address=".Length);
                }
                else if (line.StartsWith("port="))
                {
                    emailPort = int.Parse(line.Substring("port=".Length));
                }
            }
            string[] accountConfig = File.ReadAllLines(emailAccount);
            foreach(string line in accountConfig)
            {
                if (line.StartsWith("email="))
                {
                    personalEmail = line.Substring("email=".Length);
                }
                else if (line.StartsWith("psw="))
                {
                    emailPassword = line.Substring("psw=".Length);
                }
            }
        }

        public static void LoadMessages()
        {
            contacts = File.ReadAllText(contactsList).Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            specialContacts = File.ReadAllText(specialContactsList).Trim();

            contactsSubject = File.ReadAllText(contactsSubjectFile).Trim();
            contactsMessage = File.ReadAllText(contactsMessageFile).Trim();

            specialContactsSubject = File.ReadAllText(specialContactsSubjectFile).Trim();
            specialContactsMessage = File.ReadAllText(specialContactsMessageFile).Trim();
        }
    }
}
