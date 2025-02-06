using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VotingApp
{
    public partial class VotingForm : Form
    {
        private int student_Id;
        public VotingForm(int studentid)
        {
            InitializeComponent();
            student_Id = studentid;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var selectedCandidate = comboBox1.SelectedItem;

                
                var candidateId = (int)selectedCandidate.GetType().GetProperty("Value").GetValue(selectedCandidate);
                var candidateName = (string)selectedCandidate.GetType().GetProperty("Text").GetValue(selectedCandidate);

               
                DisplayCandidateInformation(candidateId);
            }
        }

        public void LoadCandidatesIntoComboBox()
        {
            try
            {
                using (var context = new StudentDB2DataContext())
                {
                    
                    var candidates = context.Candidates.ToList();

                   
                    comboBox1.Items.Clear();

                  
                    foreach (var candidate in candidates)
                    {
                        comboBox1.Items.Add(new
                        {
                            Text = candidate.Name,     
                            Value = candidate.CandidateId 
                        });
                    }

                   
                    comboBox1.DisplayMember = "Text";
                    comboBox1.ValueMember = "Value";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VotingForm_Load(object sender, EventArgs e)
        {
            label6.Text= "Voting Deadline: " + votingDeadline.ToString("MMMM dd, yyyy hh:mm tt");
            LoadCandidatesIntoComboBox();
        }

        private void DisplayCandidateInformation(int candidateId)
        {
            try
            {
                using (var context = new StudentDB2DataContext())
                {
                    
                    var candidate = context.Candidates.SingleOrDefault(c => c.CandidateId == candidateId);
                    
                    if (candidate != null)
                    {
                       
                        label2.Text = "Name: " + candidate.Name;
                        label3.Text = "Age: " + candidate.Age;
                        label4.Text = "Party: " + candidate.Party;
                        label5.Text = "Voting Sign: " + candidate.VotingSign;

                        
                        
                    }
                    else
                    {
                        MessageBox.Show("Candidate not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void RecordVote(int studentId, int candidateId, string candidateName)
        {
            try
            {
                using (var context = new StudentDB2DataContext())
                {
                    


                    var existingVote = context.students.SingleOrDefault(v => v.Id == studentId);
                    if (existingVote != null)
                    {
                        
                        MessageBox.Show("You have already voted. Each Person can vote only once!", "Vote Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                   
                    var vote = new student
                    {
                        Id = studentId,                
                        CandidateId = candidateId,    
                        CandidateName = candidateName,          
                        VotingTime = DateTime.Now 
                    };

                   
                    context.students.InsertOnSubmit(vote);
                    context.SubmitChanges();

                    MessageBox.Show("Your vote has been successfully cast!", "Vote Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DateTime votingDeadline = new DateTime(2025, 1, 31, 23, 59, 59);


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                if (DateTime.Now > votingDeadline) //   if current time exceeds to deadline  
                {
                    button1.Enabled = false;
                    MessageBox.Show("Voting is closed! The deadline has passed.", "Voting Closed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



                var selectedCandidate = comboBox1.SelectedItem;
                var candidateId = (int)selectedCandidate.GetType().GetProperty("Value").GetValue(selectedCandidate);
                var candidateName = (string)selectedCandidate.GetType().GetProperty("Text").GetValue(selectedCandidate);

               // Call the RecordVote function with the student ID, candidate ID, and candidate name
                RecordVote(student_Id, candidateId, candidateName);


            }
            else
            {
                MessageBox.Show("Please select a candidate to vote for!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DisplayWinner();
        }
        private void DisplayWinner()
        {
            try
            {
                using (var context = new StudentDB2DataContext())
                {
                   
                    var winner = context.students
                        .GroupBy(v => new { v.CandidateId, v.CandidateName })
                        .Select(g => new
                        {
                            CandidateId = g.Key.CandidateId,
                            CandidateName = g.Key.CandidateName,
                            VoteCount = g.Count()
                        })
                        .OrderByDescending(r => r.VoteCount)
                        .FirstOrDefault();

                    if (winner != null)
                    {
                        
                        label7.Text = $"Winner: {winner.CandidateName} (ID: {winner.CandidateId}) with {winner.VoteCount} votes.";

                      
                        var results = context.students
                            .GroupBy(v => new { v.CandidateId, v.CandidateName })
                            .Select(g => new
                            {
                                CandidateId = g.Key.CandidateId,
                                CandidateName = g.Key.CandidateName,
                                VoteCount = g.Count()
                            })
                            .OrderByDescending(r => r.VoteCount)
                            .ToList();

                        dataGridView1.DataSource = results;

                        
                        var winnerRecord = new Winner
                        {
                            CandidateId = winner.CandidateId,
                            CandidateName = winner.CandidateName,
                            VoteCount = winner.VoteCount,
                            ElectionDate = DateTime.Now
                        };

                       
                        var existingWinner = context.Winners
                            .FirstOrDefault(w => w.ElectionDate.Date == DateTime.Now.Date);

                        if (existingWinner == null)
                        {
                            context.Winners.InsertOnSubmit(winnerRecord);
                            context.SubmitChanges();

                            MessageBox.Show("Winner has been recorded successfully!", "Winner Recorded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("The winner for today has already been recorded.", "Duplicate Winner", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No votes were cast!", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
