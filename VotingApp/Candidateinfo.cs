using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VotingApp
{
    public partial class Candidateinfo : Form
    {
        public Candidateinfo()
        {
            InitializeComponent();
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


        private void Candidateinfo_Load(object sender, EventArgs e)
        {
           LoadCandidatesIntoComboBox();
        }
        private void DisplayCandidateInformation(int candidateId)
        {
            try
            {
                using (var context = new StudentDB2DataContext())
                {
                    // Fetch the candidate by CandidateId
                    var candidate = context.Candidates.SingleOrDefault(c => c.CandidateId == candidateId);
                    if(candidateId == 1)
                    {
                        label6.Text = "He was educated at the Aitchison College and Cathedral School in Lahore. In September\n 1971, Khan arrived at the Royal Grammar School Worcester in England,\n where he excelled at cricket.";
                        label12.Text = "Imran Khan's political vision focuses on creating a corruption-free,\n just, and merit-based Pakistan. He emphasizes economic reforms,\n welfare for the underprivileged, and strong accountability mechanisms.\n Khan also prioritizes education, healthcare, and fostering a\n self-reliant, sovereign nation.";
                    }
                    if (candidateId == 2)
                    {
                        label6.Text = "He went to Saint Anthony High School. He graduated from the Government College \nUniversity (GCU) with an art and business degree and then received a law degree\n from the Law College of Punjab University in Lahore.";
                        label12.Text = "Nawaz Sharif's political vision focuses on economic development,\n infrastructure modernization, and resolving Pakistan's energy crisis.\n He emphasized projects like the China-Pakistan\n Economic Corridor (CPEC), improved transportation systems,\n and power generation initiatives. His tenure also aimed to\n strengthen democratic institutions and promote\n regional peace through dialogue and trade.";
                    }
                    if(candidateId == 3)
                    {
                        label6.Text = "He followed in the footsteps of both his mother and his grandfather and applied\n to Oxford University, where he was accepted to read Modern History and Politics\n at Christ Church, receiving his Bachelor of Arts degree\n in 2012 (later promoted to a Master of Arts by seniority).";
                        label12.Text = "Bilawal Bhutto's political vision centers\n on empowering youth, promoting social justice,\n and ensuring democratic governance.\n He emphasizes education, healthcare, and workers'\n rights while advocating for equal opportunities and inclusivity\n in Pakistan's political and economic systems.";
                    }
                    if (candidate != null)
                    {
                        
                        label1.Text =  candidate.Name;
                        label2.Text =  candidate.Age;
                        label3.Text =  candidate.Party;
                        label4.Text =  candidate.VotingSign;

                        
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var selectedCandidate = comboBox1.SelectedItem;

                // Extract candidateId and candidateName from the selected item
                var candidateId = (int)selectedCandidate.GetType().GetProperty("Value").GetValue(selectedCandidate);
                var candidateName = (string)selectedCandidate.GetType().GetProperty("Text").GetValue(selectedCandidate);

                
                DisplayCandidateInformation(candidateId);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }


}


