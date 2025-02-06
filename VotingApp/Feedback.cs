using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace VotingApp
{
    public partial class Feedback : Form
    {
        private string current_cnic;
        public Feedback(string cnic)
        {
            InitializeComponent();
            current_cnic = cnic;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new StudentDB2DataContext())
                {
                  
                    string feedbackText = textBox1.Text;

                    
                    int id = GetUserIdFromCnic(current_cnic);
                    string name = GetUsernameFromCnic(current_cnic);
                    // Create a new feedback record
                    var newFeedback = new Feedback2 
                    {
                        UserId = id,
                        UserName = name,

                        FeedbackText = feedbackText,   
                        TimeSubmitted = DateTime.Now    
                    };

                    context.Feedback2s.InsertOnSubmit(newFeedback);
                    context.SubmitChanges();

                    MessageBox.Show("Thank you for your feedback!", "Feedback Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    textBox1.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                return "a";
            }
        }
        private void LoadFeedbacks()
        {
            try
            {
                using (var context = new StudentDB2DataContext())
                {
                    
                    var feedbacks = context.Feedback2s
                        .OrderByDescending(f => f.FeedbackId)
                        .Select(f => new
                        {
                            f.FeedbackId,
                            f.UserId,
                            f.UserName,
                            f.FeedbackText,
                            f.TimeSubmitted
                        })
                        .ToList(); 

                    
                    dataGridView1.DataSource = feedbacks;

                    // optional
                    dataGridView1.Columns["FeedbackText"].HeaderText = "Feedback";
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading feedbacks: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadFeedbacks();
        }

        private void Feedback_Load(object sender, EventArgs e)
        {
            string username = GetUsernameFromCnic(current_cnic);
            if (username != "null")
            {
                label1.Text = "Welcome : " + username.ToString() + " Please Share Your Feedback"; // Display the user ID (optional)
            }
            else
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }

    

    

