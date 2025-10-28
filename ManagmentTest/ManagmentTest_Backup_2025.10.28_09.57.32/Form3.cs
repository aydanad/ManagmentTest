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
    public partial class Form3 : Form
    {
        Form2 frm = new Form2();
        private int _candidateId;
        Test_DBEntities1 db = new Test_DBEntities1();
        string filepath;
        public Form3(int id)
        {
            InitializeComponent();
            _candidateId = id;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            var item= db.Canddidates.Where(v => v.CanddidateID == _candidateId).SingleOrDefault();
            txtFirstName.Text=item.FirstName;
            txtLastName.Text=item.LastName;
            txtPhone.Text=item.Phone;
            txtEmail.Text = item.Email;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "select resume";
            openFileDialog1.Filter = "PDF Files (*.pdf)|*.pdf";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filepath = openFileDialog1.FileName;
            }

        }

        private void OK_Click(object sender, EventArgs e)
        {
            var canddidate = db.Canddidates.Find(_candidateId);
            canddidate.FirstName = txtFirstName.Text;
            canddidate.LastName = txtLastName.Text;
            canddidate.Phone = txtPhone.Text;
            canddidate.Email = txtEmail.Text;
            canddidate.ResumePath = filepath;

            db.SaveChanges();
            this.Close();
        }
    }
}
