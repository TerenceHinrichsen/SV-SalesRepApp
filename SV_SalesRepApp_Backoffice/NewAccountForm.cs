using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV_SalesRepApp_Backoffice
{
    public partial class NewAccountForm : Form
    {
        public NewAccountForm()
        {
            InitializeComponent();
        }

        private void NewAccountForm_Load(object sender, EventArgs e)
        {
            this.newAccountApplicationTableAdapter.Fill(this.appDbDataSet.NewAccountApplication);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.newAccountApplicationTableAdapter.Fill(this.appDbDataSet.NewAccountApplication);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (newApplicationDataGridView.SelectedRows.Count > 1)
            {
                MessageBox.Show("Too many rows selected");
            }
            if (newApplicationDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select at least one row");
            }
            else
            {
                var customerDetail = newApplicationDataGridView.SelectedRows[0];
                MessageBox.Show(customerDetail.Cells[0].Value.ToString());
            }
        }

        private bool createCustomerInEvo()
        {
            return false;
        }

        private void RejectLine_Click(object sender, EventArgs e)
        {
            if (newApplicationDataGridView.SelectedRows.Count > 1)
            {
                MessageBox.Show("Too many rows selected");
            }
            if (newApplicationDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select at least one row");
            }
            else
            {
                var customerDetail = newApplicationDataGridView.SelectedRows[0];
                MessageBox.Show(customerDetail.Cells[0].Value.ToString());
            }
        }
    }
}
