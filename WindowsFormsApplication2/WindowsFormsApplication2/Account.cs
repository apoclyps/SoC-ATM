using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WindowsFormsApplication2
{
    /*
     *   The Account class encapusulates all features of a simple bank account
     */
    public class Account
    {
        //the attributes for the account
        private Object thisLock = new Object();
        private int accountBalance;
        private int accountPin;
        private int accountNumber;
        private bool threadSafe;
   
        // a constructor that takes initial values for each of the attributes (balance, pin, accountNumber)
        public Account(int balance, int pin, int accountNum)
        {
            this.accountBalance = balance;
            this.accountPin = pin;
            this.accountNumber = accountNum;
            this.threadSafe = false;
        }

        // Balance getters and setters
        public int balance
        {
            get 
            { 
                return accountBalance;
            }
            set 
            { 
                accountBalance = value;
            }
        }

        // Pin getters and setters
        public int pin
        {
            get
            { 
                return accountPin;
            }
            set 
            {
                accountPin = value;
            }
        }

        // Account getter and setters
        public int accountNum
        {
            get 
            { 
                return accountNumber; 
            }
            set 
            {
                accountNumber = value;
            }
        }

        // Threadsafe Setter
        public void setThreadSafe(bool dataSafe) 
        {
            threadSafe = dataSafe;
        }

        // Thread Safe setter
        public bool getThreadSafe() 
        { 
            return threadSafe; 
        }

        /*
         *   This funciton allows us to decrement the balance of an account
         *   it perfomes a simple check to ensure the balance is greater tha
         *   the amount being debeted
         *   
         *   reurns:
         *   true if the transactions if possible
         *   false if there are insufficent funds in the account
         */
        public Boolean decrementBalance(int amount)
        {
            // Carry out transation as long as there is equal or more funds available
            if (this.balance >= amount)
            {
                // If the thread safe option has been enabled
                if (threadSafe)
                {
                    // Lock this piece of the code so only 1 thread can access it at a time
                    lock (thisLock)
                    {
                        //temporarily store balance and wait
                        int temporaryBalance = balance;
                        Thread.Sleep(1500);

                        // reduce the amount from the temporary balance and wait 
                        temporaryBalance = temporaryBalance - amount;
                        Thread.Sleep(1500);

                        // Update balance with the correct balance
                        balance = temporaryBalance;
                    }
                }
                else
                {
                    // Causes a 1.5 seconds sleep inbetween balance being stored in a temp balance, 
                    //temp balance having the amount deducted and the balance being set to temp balance
                    // This allows a data race to occur as two threads will be competing to access the balance variable

                    //temporarily store balance and wait
                    int temporaryBalance = balance;
                    System.Threading.Thread.Sleep(1500);

                    // reduce the amount from the temporary balance and wait 
                    temporaryBalance = temporaryBalance - amount;
                    System.Threading.Thread.Sleep(1500);

                    // Update balance with the correct balance
                    balance = temporaryBalance;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * This funciton check the account pin against the argument passed to it
         *
         * returns:
         * true if they match
         * false if they do not
         */
        public Boolean checkPin(int pinEntered)
        {
            if (pinEntered == pin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
  
    }
}
