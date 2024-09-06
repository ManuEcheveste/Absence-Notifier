using System;

namespace LastFallback
{
    class Program
    {
        static void Main(string[] args) //Start
        {
            if (args.Length > 0)
            {
                CheckArguments(args);
            }
            else
            {
                MainProgram.Start();
            }
        }

        static void CheckArguments(string[] args)
        {
            foreach (string arg in args)
            {
                switch (arg.ToLower())
                {
                    case "--verifyLastLogin": //Se ejecuta diario a las 00:00, comprueba la última vez que se hizo login.
                        DailyCheck.Start();
                        break;
                }
            }
        }
    }
}
