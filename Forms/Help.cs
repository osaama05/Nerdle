namespace Nerdle_Lopputyö
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();

            Size = new Size(645, 530);

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            AutoSize = true;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}