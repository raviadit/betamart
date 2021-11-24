using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace betamartnew
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.betamart' table. You can move, or remove it, as needed.
            this.betamartTableAdapter.Fill(this.appData.betamart);
            betamartBindingSource.DataSource = this.appData.betamart;
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtsearch.Text))
                    dataGridView.DataSource = betamartBindingSource;
                else
                {
                    var query = from x in this.appData.betamart
                                where x.NamaBarang.IndexOf(txtsearch.Text, StringComparison.OrdinalIgnoreCase) >= 0
                                || x.NamaBarang == txtsearch.Text
                                || x.KategoriBarang == txtsearch.Text
                                || x.StockBarang == txtsearch.Text
                                || x.HargaBarang.IndexOf(txtsearch.Text, StringComparison.OrdinalIgnoreCase) >= 0
                                select x;
                    dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    betamartBindingSource.RemoveCurrent();
            }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtnamabarang.Focus();
                this.appData.betamart.AddbetamartRow(this.appData.betamart.NewbetamartRow());
                betamartBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                betamartBindingSource.ResetBindings(false);
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtnamabarang.Focus();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                betamartBindingSource.EndEdit();
                betamartTableAdapter.Update(this.appData.betamart);
                panel.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                betamartBindingSource.ResetBindings(false);
            }
        }


        private void btndelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin Ingin Menghapus Produk Ini?","Message",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                betamartBindingSource.RemoveCurrent();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
