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
    public partial class Updateinfo : Form
    {
        private int currentUserid;
        private StudentDB2DataContext db;
        public Updateinfo(int id)
        {
            InitializeComponent();
            currentUserid = id;
            db=new StudentDB2DataContext();
        }

        private void LoadUserDetails()
        {
            try
            {
                // Fetch the user's details based on their ID
                var userDetails = db.registerations
                    .Where(r => r.Id == currentUserid)
                    .Select(r => new
                    {
                        r.Id,
                        r.name,
                        r.age,
                        r.cnic,
                        r.password
                    })
                    .ToList();

                if (userDetails.Count > 0)
                {
                    //  user details to the DataGridView
                    dataGridView1.DataSource = userDetails;
                }
                else
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
           
            if (dataGridView1.Rows.Count > 0)
            {
                var user = db.registerations.SingleOrDefault(r => r.Id == currentUserid);

                if (user != null)
                {
                    
                    user.name = textBox1.Text.Trim();
                    user.age = int.Parse(textBox2.Text.Trim());
                    user.cnic = textBox3.Text.Trim();
                    user.password = textBox4.Text.Trim();

                    try
                    {
                        db.SubmitChanges(); 
                        MessageBox.Show("Information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUserDetails(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while updating the information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
        }

        private void Updateinfo_Load(object sender, EventArgs e)
        {
            LoadUserDetails();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                var user = db.registerations.SingleOrDefault(r => r.Id == currentUserid);

                if (user != null)
                {
                    
                    user.name = textBox1.Text.Trim();
                    user.age = int.Parse(textBox2.Text.Trim());
                    user.cnic = textBox3.Text.Trim();
                    user.password = textBox4.Text.Trim();

                    try
                    {
                        db.SubmitChanges(); 
                        MessageBox.Show("Information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUserDetails(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while updating the information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text="";
        }
    }
}
