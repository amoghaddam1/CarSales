using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RRCAGApp
{
    public partial class LauncherForm : Form
    {
        public LauncherForm()
        {
            InitializeComponent();

            this.tsFileExit.Click += TsFileExit_Click;
            this.tsHelpAbout.Click += TsHelpAbout_Click;
            this.tsFileOpenSalesQuote.Click += TsFileOpenSalesQuote_Click;
        }

        private void TsFileOpenSalesQuote_Click(object sender, EventArgs e)
        {
            var SalesQuoteForm = new SalesQuoteForm();
            SalesQuoteForm.ShowDialog();
        }

        private void TsHelpAbout_Click(object sender, EventArgs e)
        {
            var AboutForm = new AboutForm();
            AboutForm.Show();
        }

        private void TsFileExit_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
