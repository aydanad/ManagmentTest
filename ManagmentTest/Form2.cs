
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ManagmentTest
{
    public partial class Form2 : Form
    {
        Test_DBEntities1 db = new Test_DBEntities1();
        public int id;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                var item = dataGridView1.SelectedRows[0];
                int userId = Convert.ToInt32(item.Cells["CanddidateID"].Value);
               var i= db.Canddidates.Find(userId);
                var c=db.Applies.Where(h=>h.CanddidateID==userId).FirstOrDefault();
                db.Applies.Remove(c);
                db.Canddidates.Remove(i);
                db.SaveChanges();
                RefreshGrid();
            }
        }

        public void RefreshGrid()
        {
            var list = db.Canddidates.ToList();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = list;
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = dataGridView1.SelectedRows[0];
            id = Convert.ToInt32(item.Cells["CanddidateID"].Value);
            Form3 form = new Form3(id);
            form.ShowDialog();
        }


        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void print_Click(object sender, EventArgs e)
        {

            try
            {
                string connectionString = @"Data Source=192.168.2.54,51433;Initial Catalog=Test_DB;User Id=sa;Password=Password_123#;Encrypt=False;MultipleActiveResultSets=True;";

                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                   
                }

                StiReport report = new StiReport();
                report.Load("C:\\Users\\mina.ganjizadeh\\Desktop\\Report.mrt");

                report.Dictionary.Databases.Clear();

                StiSqlDatabase sqlDatabase = new StiSqlDatabase("MS SQL", connectionString);
                report.Dictionary.Databases.Add(sqlDatabase);

                report.Compile();
                report.Render();
                report.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ خطا: " + ex.Message);
            }
        }
    }
}
