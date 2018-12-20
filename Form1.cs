using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseGrid
{
    public partial class frmTester : Form
    {

        OleDbConnection conn;

        public frmTester()
        {
            InitializeComponent();
        }

        private void frmTester_Load(object sender, EventArgs e)
        {
            var connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\PROJETOS\UdemyCourse (Alex)\UdemyProject 4\DataBaseGrid\Books.accdb;
                            Persist Security Info=False;";
            conn = new OleDbConnection(connString);
            conn.Open();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            OleDbCommand command = null;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataTable table = new DataTable();
            try
            {
                command = new OleDbCommand(txtCommand.Text, conn);
                adapter.SelectCommand = command;
                adapter.Fill(table);

                grdRecord.DataSource = table;

                lblCount.Text = table.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in SQL Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

            command.Dispose();
            adapter.Dispose();
            table.Dispose();
        }

        private void frmFormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
            conn.Dispose();
        }


        ///
        /// SQL example for testings
        //Select a.Author, t.Title, t.Year_Published
        //    From
        //Titles as t
        //    Inner Join Title_Author as ta ON ta.ISBN = t.ISBN
        //    Inner Join Authors as a On a.Au_ID = ta.Au_ID
        //    Inner Join Publishers as p ON p.PubID = t.PubID
        //    where t.Year_Published = 1989 and p.Name = "WEST"
        //Order by a.Author desc
    }
}
