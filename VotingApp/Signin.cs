using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VotingApp
{
    public partial class Signin : Form
    {
        public Signin()
        {
            InitializeComponent();
        }

        private void Signin_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StudentDB2DataContext db= new StudentDB2DataContext();
            string userCNIC = textBox1.Text;  
            string userPassword = textBox2.Text;  

           
            var user = db.registerations.SingleOrDefault(x => x.cnic == userCNIC && x.password == userPassword);

            if (user != null)
            {

                DashBoard d = new DashBoard(userCNIC); 
                d.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid CNIC or Password");
            }
        }
    }
}
