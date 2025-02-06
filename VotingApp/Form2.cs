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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text ==""||textBox2.Text =="" ||textBox3.Text=="" ||textBox4.Text=="")
            {
                MessageBox.Show("ALl fields are required");
            }
            StudentDB2DataContext db=new StudentDB2DataContext();
            registeration r=new registeration();
            r.name= textBox1.Text;
            r.age=int.Parse(textBox2.Text);
            r.cnic = textBox3.Text;
            r.password = textBox4.Text;
            db.registerations.InsertOnSubmit(r);
           

            db.SubmitChanges();

            MessageBox.Show("Registeration Successfull");

        }

        private void label5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Signin s=new Signin();
            s.ShowDialog();
        }
    }
}
