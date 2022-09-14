using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nerdle_Lopputyö.Forms
{
    public partial class StartMenu : Form
    {
        private int numOfRows;
        public int NumOfRows
        {
            get { return numOfRows; }
        }
        public StartMenu()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.SlateGray;
            MaximizeBox = false;
        }

        private void btnHC_Click(object sender, EventArgs e)
        {
            numOfRows = 3;
            Close();
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            numOfRows = 6;
            Close();
        }
    }
}
