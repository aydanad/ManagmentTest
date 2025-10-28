using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagmentTest
{
    public partial class Form1 : Form
    {
        Test_DBEntities1 db = new Test_DBEntities1();
        Form2 frm =new Form2();
        string filepath;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var jobs = db.JobPositions.Where(h => h.IsActive == true).ToList();
            Jobs.DataSource = jobs;
            Jobs.DisplayMember = "Title";
            Jobs.ValueMember = "JobPositionID";
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Canddidate canddidate = new Canddidate()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                ResumePath = filepath
            };
            Apply  apply = new Apply()
            {
                CanddidateID = canddidate.CanddidateID,
                PositionID = (int)Jobs.SelectedValue,
                ApplicationDate = DateTime.Now,
                Status = "در انتظار"
            };
            db.Canddidates.Add(canddidate);
            db.Applies.Add(apply);
            db.SaveChanges();
            frm.RefreshGrid();
            this.Close();
            
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Open_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Title = "select resume";
            openFileDialog1.Filter = "PDF Files (*.pdf)|*.pdf";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filepath = openFileDialog1.FileName;
            }
            
        }
    }
}
