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
    public partial class Winners : Form
    {
        private int currentid;
        public Winners(int id)
        {
            InitializeComponent();
            currentid = id; 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadWinnerHistory();
        }

        private void LoadWinnerHistory()
        {
            try
            {
                using (var context = new StudentDB2DataContext())
                {
                    var winners = context.Winners
                        .Select(w => new
                        {
                            w.WinnerId,      
                            w.CandidateId,    
                            w.CandidateName, 
                            w.VoteCount,     
                            w.ElectionDate 
                        })
                        .OrderByDescending(w => w.ElectionDate)
                        .ToList();

                    if (winners.Count > 0)
                    {
                        dataGridView1.DataSource = winners;
                    }
                    else
                    {
                        MessageBox.Show("No winner history available.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading winner history: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
