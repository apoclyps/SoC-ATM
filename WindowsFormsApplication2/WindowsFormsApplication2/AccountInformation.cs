using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    /*
   *   This is the root of program and the entry point
   * 
   *   Class programm contains an array of account objects and a singel ATM object  
   * 
    */
   public class AccountInformation
    {
        private Account[] ac = new Account[3];

        /*
         * This function initilises the 3 accounts 
         * and instanciates the ATM class passing a referance to the account information
         * 
         */
        public AccountInformation()
        {
            ac[0] = new Account(300, 1111, 111111);
            ac[1] = new Account(750, 2222, 222222);
            ac[2] = new Account(3000, 3333, 333333);
        }

       // Returns an array of accounts
        public Account[] getAccounts()
        {
            return ac;
        }

    }
}
