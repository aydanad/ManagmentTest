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
    public partial class Form7: Form
    {
        ManagementTestEntities db = new ManagementTestEntities();
        private int _jobId;
        public Form7(int id)
        {
            InitializeComponent();
            _jobId = id;
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            var item = db.JobPositions.Where(v => v.JobPositionID == _jobId).SingleOrDefault();
            item.Title = txtTitle.Text;
            item.Department = txtDepartment.Text;
            if (checkBox1.Checked)
            {
                item.IsActive = true;
            }
            else
            {
                item.IsActive = false;
            }
            db.SaveChanges();
            this.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            var item = db.JobPositions.Where(v => v.JobPositionID == _jobId).SingleOrDefault();
            txtTitle.Text = item.Title;
            txtDepartment.Text = item.Department;
            if (item.IsActive)
            {

                checkBox1.Checked = true;
            }
        }
    }
}
