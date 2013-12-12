using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class frmATMMenu : Form
    {
        Account currentAccount;
        bool displayMenu = true;
        bool displayWithdraw = false;
        bool displayBalance = false;
        bool completeRun = false;
        bool exitSystem = false;

        public int countdown = 4;
        
        
        public frmATMMenu(Account currentAccount)
        {
            InitializeComponent();
            this.currentAccount = currentAccount;
        }

        


        private void button3_Click(object sender, EventArgs e)
        {
            returnCard();
            this.Close();
        }

        public void returnCard()
        {
                 lblConsoleText.Text = " Your card is being returned\n\n"
            + " Thank you for using Citi Bank ATMS";

                 exitSystem = true;
          
        }


        private void button2_Click(object sender, EventArgs e)
        {
            displayAccountBalance();
        }

        public void displayAccountBalance()
        {
            txtBalance.Text = currentAccount.balance.ToString();
            lblConsoleText.Text = " Your account balance is : \n\n  £" + currentAccount.balance.ToString() + ".00\n\n"
            + " Press 'Enter' to continue\n";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            resetATM();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            withdraw(1);
        }

        private void frmATMMenu_Load(object sender, EventArgs e)
        {
            txtAccountNumber.Text = currentAccount.accountNum.ToString();
            txtAccessTime.Text = DateTime.Now.ToShortTimeString();
            txtKeyed.MaxLength = 4;

            Program.incrementActiveUsers();
        }

       
        private void button15_Click(object sender, EventArgs e)
        {
            resetATM();
        }

        public void resetATM()
        {
            displayMenu = true;
            displayWithdraw = false;
            displayBalance = false;
            completeRun = false;
            txtKeyed.Text = "";

            lblConsoleText.Text = ""
                     + " Please choose from the following Menu Items\n\n"
                     + "  1> Withdraw       -  Remove funds from your account\n"
                     + "  2> Balance         -  Display your current account balance\n"
                     + "  3> Return Card   -  Exit the ATM system securly.";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtKeyed.Text += "1";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtKeyed.Text += "2";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txtKeyed.Text += "3";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            txtKeyed.Text += "4";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            txtKeyed.Text += "5";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            txtKeyed.Text += "6";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            txtKeyed.Text += "7";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            txtKeyed.Text += "8";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            txtKeyed.Text += "9";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            txtKeyed.Text += "0";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (exitSystem == false)
            {
                int keyedValue = getKeyedValue();
                txtKeyed.Text = "";

                if (displayMenu == true && completeRun == true)
                {
                    resetATM();
                }
                else if (displayMenu == true && completeRun == false)
                {
                    // switch 1/2/3
                    switch (keyedValue)
                    {
                        case 1:
                            displayMenu = false;
                            displayBalance = false;
                            displayWithdraw = true;
                            displayWithdrawOptions();
                            break;
                        case 2:
                            displayMenu = false;
                            displayBalance = true;
                            displayWithdraw = false;
                            //displayAccountBalance();
                            completeRun = true;

                            break;
                        case 3:
                            returnCard();
                            break;
                        default:
                            lblConsoleText.Text += "\n\n\n Invalid selection. Please select a value from 1 - 3";
                            txtKeyed.Text = "";
                            break;
                    }
                }
                else if (displayWithdraw == true && keyedValue > 0 && completeRun == false)
                {
                    withdraw(keyedValue);
                    completeRun = true;
                    displayMenu = true;
                    displayBalance = false;
                    displayWithdraw = false;
                }

                if (displayBalance == true && completeRun == true)
                {
                    Console.WriteLine("display balance else if");
                    displayAccountBalance();
                    completeRun = true;
                    displayMenu = true;
                    displayBalance = false;
                    displayWithdraw = false;
                }
            }
        }

        public int getKeyedValue()
        {
            int keyedValue;
            if (txtKeyed.Text == "")
            {
                keyedValue = 0;
                
            }
            else
            {
                keyedValue = Convert.ToInt32(txtKeyed.Text);
            }

            return keyedValue;
        }

        private void displayWithdrawOptions()
        {
            lblConsoleText.Text = "  How much would you like to withdraw ?\n\n"
                   + " 1> £10\n"
                   + " 2> £20\n"
                   + " 3> £40\n"
                   + " 4> £100\n"
                   + " 5> £500";
        }

        private void withdraw(int keyedValue)
        {
            int withdrawAmount = 0;

            if (keyedValue > 0 && keyedValue <= 5)
            {
                    switch (keyedValue)
                    {
                        case 1:
                            withdrawAmount = 10;
                            break;
                        case 2:
                            withdrawAmount = 20;
                            break;
                        case 3:
                            withdrawAmount = 40;
                            break;
                        case 4:
                            withdrawAmount = 100;
                            break;
                        case 5:
                            withdrawAmount = 500;
                            break;
                        default:
                            Console.WriteLine("Invalid selection. Please select a value from 1 - 3");
                            break;
                      }
        
                if (withdrawAmount <= currentAccount.balance)
                {
                    // Withdraw from account
                    bool status = currentAccount.decrementBalance(withdrawAmount);
                    if (status)
                    {
                        Console.WriteLine("money withdrawn");
                        lblConsoleText.Text = " You have withdrawn the following funds : \n"
                         + " £" + withdrawAmount + ".00 \n\n"
                         + " Your balance is now : \n\n"
                         + " £" + currentAccount.balance + ".00\n\n"
                         + " Press enter to return to menu";
                    }
                    else 
                    { 
                        MessageBox.Show("Failed"); 
                    }
                }
                else
                {
                    // Insufficent funds
                    lblConsoleText.Text =  "Sorry you have insufficent funds in your account\n\n"
                             + " Please choose from the following Menu Items\n\n"
                             + "  1> Withdraw       -  Remove funds from your account\n"
                             + "  2> Balance         -  Display your current account balance\n"
                             + "  3> Return Card   -  Exit the ATM system securly.";
                }
            }
                // Invalid Value entered
            else
            {
                lblConsoleText.Text += "\n\n Invalid selection. Please 'Enter' to return to menu";
                txtKeyed.Text = "";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (exitSystem == true)
            {
                countdown = countdown - 1;
                if (countdown == 0)
                {
                    Program.decrementActiveATMS();
                    Program.decrementActiveUsers();
                    Program.newATM();
                    this.Close();
                }
            }
        }

    }
}
