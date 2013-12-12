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
    public partial class frmCentralBank : Form
    {
        public Account[] accountsArray;
        
        public frmCentralBank(Account [] accountArray)
        {
            InitializeComponent();
            timer1.Start();
            accountsArray = accountArray;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            accountsView.DataSource = accountsArray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Program.newATM();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void accountsView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            refreshGrid();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool state = ((CheckBox)sender).Checked;
            for (int i = 0; i < accountsArray.Length; i++)
            {
                accountsArray[i].setThreadSafe(state);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtActiveAtms.Text = Program.getActiveATMS().ToString();
            accountsView.DataSource = accountsArray;
        }

        public void refreshGrid()
        {
            accountsView.DataSource = accountsArray;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refreshGrid();
            txtActiveAtms.Text = Program.getActiveATMS().ToString();
            txtActiveUsers.Text = Program.getActiveUsers().ToString();
            accountsView.DataSource = accountsArray;
        }

    }
}
