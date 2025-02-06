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
    public partial class DashBoard : Form
    {
        private string currentUserCnic;
        
        public DashBoard(string cnic)
        {
            InitializeComponent();
            currentUserCnic = cnic;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int studentId = GetUserIdFromCnic(currentUserCnic);
            if (studentId != -1)
            {
               
                VotingForm f = new VotingForm(studentId);
                f.ShowDialog();  
                      
            }
            else
            {
                MessageBox.Show("Unable to retrieve your ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            
            int userId = GetUserIdFromCnic(currentUserCnic);
            string username=GetUsernameFromCnic(currentUserCnic);
            if (userId != -1)
            {
                label1.Text = "Welcome : " + username.ToString(); // Display the user ID (optional)
            }
            else
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int GetUserIdFromCnic(string cnic)
        {
            try
            {
                using (var context = new StudentDB2DataContext())
                {
                    var user = context.registerations.SingleOrDefault(r => r.cnic == cnic);
                    if (user != null)
                    {
                        return user.Id;  
                    }
                    else
                    {
                        return -1; 
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; 
            }
        }
        private string GetUsernameFromCnic(string cnic)
        {
            try
            {
                using (var context = new StudentDB2DataContext())
                {
                    var user = context.registerations.SingleOrDefault(r => r.cnic == cnic);
                    if (user != null)
                    {
                        return user.name; 
                    }
                    else
                    {
                        return "a";  
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "a";  // Return -1 in case of error
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int studentId = GetUserIdFromCnic(currentUserCnic);
            if (studentId != -1)
            {
                
                Winners f = new Winners(studentId);
                f.ShowDialog(); 
                                
            }
            else
            {
                MessageBox.Show("Unable to retrieve your ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {



            int studentId = GetUserIdFromCnic(currentUserCnic);
            if (studentId != -1)
            {
                Feedback feedback = new Feedback(currentUserCnic);

                feedback.Show();
               
            }
            else
            {
                MessageBox.Show("Unable to retrieve your ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Candidateinfo f=new Candidateinfo();
            f.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            int studentId = GetUserIdFromCnic(currentUserCnic);
            if (studentId != -1)
            {
                
                Updateinfo f = new Updateinfo(studentId);
                f.ShowDialog(); 
                                 
            }
            else
            {
                MessageBox.Show("Unable to retrieve your ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            UpcomingElection u=new UpcomingElection(12);
            u.Show();
            
        }
    }
}
