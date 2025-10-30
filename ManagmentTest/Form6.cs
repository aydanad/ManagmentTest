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
    public partial class Form6: Form
    {
        ManagementTestEntities db = new ManagementTestEntities();
        private int _interviewId;
        public Form6(int id)
        {
            InitializeComponent();
            _interviewId = id;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            var interview = db.Interviews.Where(v => v.InterviewID == _interviewId).FirstOrDefault();
            var ss = db.Applies.Where(v => v.CandidateID == (int)txtApply.SelectedValue).FirstOrDefault();
            interview.Status = "برای مصاحبه";
            interview.ApplicationID = ss.ApplicationID;
            interview.InterviewDate = txtInterviewDate.Value;
            interview.InterviewName = txtInterviewName.Text;
            ss.Status = "برای مصاحبه";

            db.SaveChanges();
            this.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            var item = db.Interviews.Where(v => v.InterviewID == _interviewId).SingleOrDefault();
            txtInterviewName.Text = item.InterviewName;
            txtInterviewDate.Value = item.InterviewDate;
            txtApply.Text = item.Apply.Candidate.FirstName + " " + item.Apply.Candidate.LastName;
            item.Apply.Status = "در انتظار";
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
    }
    
}
