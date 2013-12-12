using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication2
{
    static class Program
    {
        // Static variables to be accessed from all class's and forms
        static AccountInformation newAccounts;
        static Account [] activeAccounts;
        static int activeATMS;
        static int activeUsers;
        static Thread frmATM_t;

        /// <summary>
        /// The main entry point for the application.
        /// Creates a bew ubstabce if frmCentral Bank where new ATM's can be created
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Gets all current accounts in the system from AccountInformation
            newAccounts  = new AccountInformation();

            activeAccounts = newAccounts.getAccounts();
            Application.Run(new frmCentralBank(activeAccounts));
        }

        //Creates a new ATM form in a new thread
        public static void newATM()
        {
            frmATM_t = new Thread(ThreadProccess);
            frmATM_t.Start();
        }

        // The thread processes executed
        public static void ThreadProccess()
        {
            Application.Run(new frmATMLogin(activeAccounts));
        }

        // Getters for changing variables in frmCentalBank
        public static int getActiveATMS()
        {
            return activeATMS;
        }

        public static void setActiveATMS(int activeATM)
        {
            activeATMS = activeATM;
        }

        public static int getActiveUsers()
        {
            return activeUsers;
        }

        public static void setActiveUsers(int activeUser)
        {
            activeUsers = activeUser;
        }

        // Getters for changing variables in frmCentalBank
        public static void incrementActiveATMS()
        {
            activeATMS = activeATMS + 1;
        }

        public static void decrementActiveATMS()
        {
            activeATMS = activeATMS - 1;
        }

        public static void incrementActiveUsers()
        {
            activeUsers = activeUsers + 1;
        }

        public static void decrementActiveUsers()
        {
            activeUsers = activeUsers - 1;
        }
    }
}
