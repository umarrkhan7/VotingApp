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
    public partial class UpcomingElection : Form
    {
        
        StudentDB2DataContext db=new StudentDB2DataContext();

        public UpcomingElection(int id)
        {
            InitializeComponent();
        }
        private void UpcomingElection_Load(object sender, EventArgs e)
        {
            LoadElections();
            LoadElections2();
        }


        private void LoadElections()
        {
            try
            {
             
                var elections = db.UpcomingElections
                    .Select(e => new
                    {
                        e.Id,
                        e.CandidateName,
                        e.Age,
                        e.VotingTime,
                        e.Party,
                        e.VotingSign,
                        e.Address
                    })
                    .ToList();

                if (elections.Count > 0)
                {
                  
                    dataGridView1.DataSource = elections;
                }
                else
                {
                    MessageBox.Show("No upcoming elections found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("An error occurred while loading elections: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadElections2()
        {
            try
            {
               
                var elections = db.UpcomingElectionspk44s
                    .Select(e => new
                    {
                        e.Id,
                        e.CandidateName,
                        e.Age,
                        e.VotingTime,
                        e.Party,
                        e.VotingSign,
                        e.Address
                    })
                    .ToList();

                if (elections.Count > 0)
                {
                    
                    dataGridView2.DataSource = elections;
                }
                else
                {
                    MessageBox.Show("No upcoming elections found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("An error occurred while loading elections: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
