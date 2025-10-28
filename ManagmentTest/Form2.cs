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
using Simulsoft.Report;
using Simulsoft.Report.Viewer;

namespace ManagmentTest
{
    public partial class Form2 : Telerik.WinControls.UI.RadForm
    {
        Test_DBEntities1 db = new Test_DBEntities1();
        public int id;
        public Form2()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            Form1 form=new Form1();
            form.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button==MouseButtons.Right&&e.RowIndex>=0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].IsSelected= true;
                contextMenuStrip1.Show(Cursor.Position);
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshGrid();
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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
