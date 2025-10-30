using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagmentTest
{
    public partial class Form5 : Form
    {
        ManagementTestEntities db = new ManagementTestEntities();
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            var applies = db.Applies
                .Where(a => a.Status == "در انتظار")
                .Select(a => new
                {
                    a.CandidateID,
                    FullName = db.Candidates
                      .Where(c => c.CandidateID == a.CandidateID)
                      .Select(c => c.FirstName + " " + c.LastName)
                      .FirstOrDefault()
                })
                .ToList();

            txtApply.DataSource = applies;
            txtApply.DisplayMember = "FullName";
            txtApply.ValueMember = "CandidateID";
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            var ss = db.Applies.Where(v => v.CandidateID == (int)txtApply.SelectedValue).FirstOrDefault();
            Interview interview = new Interview();
            interview.Status = "برای مصاحبه";
            interview.ApplicationID = ss.ApplicationID;
            interview.InterviewDate = txtInterviewDate.Value;
            interview.InterviewName = txtInterviewName.Text;
            ss.Status= "برای مصاحبه";
            db.Interviews.Add(interview);
            db.SaveChanges();
            this.Close();

        }
    }
}
