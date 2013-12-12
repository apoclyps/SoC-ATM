using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication2
{
    public partial class frmATMLogin : Form
    {
        public Account[] ac;

        public frmATMLogin(Account[] ac)
        {
            InitializeComponent();
            this.ac = ac;
        }

        private void frmATMLogin_Load(object sender, EventArgs e)
        {
            // Set to no text.
            txtPin.Text = "";
            // The password character is an asterisk.
            txtPin.PasswordChar = '*';
            // The control will allow no more than 6 characters.
            txtAccountNumber.MaxLength = 6;
            txtPin.MaxLength = 4;

            Program.incrementActiveATMS();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           bool loginSuccess=false;

           Console.Write("LENGTH + " +ac.Length);
           for(int i=0;i<ac.Length;i++)
           {
               if (txtAccountNumber.Text == ac[i].accountNum.ToString())
               {
                   if (txtPin.Text == ac[i].pin.ToString())
                   {
                       loginSuccess = true;
                       frmATMMenu newATMMenu = new frmATMMenu(ac[i]);
                       this.Hide();
                       newATMMenu.Visible = true;
                   }
                   else
                   {
                       i = ac.Length + 1;
                   }
               }
           }

           if (loginSuccess==false)
           {
               MessageBox.Show("Please enter your details again and insert your card to verify.");
           }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.decrementActiveATMS();
            this.Close();
        }

        public void makeVisible()
        {
            this.Show();
        }

    }
}
