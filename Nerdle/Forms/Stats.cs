namespace Nerdle_Lopputyö
{
    public partial class Stats : Form
    {
        private List<string> _stats = new();
        private Files files = new Files();

        public Stats()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.SlateGray;
            ForeColor = Color.FloralWhite;
            MaximizeBox = false;
            AutoSize = true;

            _stats = files.ReturnStatsFromFile();
            ShowStats();
        }

        private void ShowStats()
        {
            int gamesPlayed = 0;
            int i = 1;
            int x = 20;
            int y = 20;

            foreach (string s in _stats)
            {
                gamesPlayed += Convert.ToInt32(s);
                Label label = new Label();

                if (i == _stats.Count())
                {
                    label.Text = "Wrong answers: " + s;
                }
                else
                {
                    label.Text = "Wins on row " + i + ": " + s;
                }

                label.Name = "lblRow" + i;
                label.Location = new Point(x, y);
                label.Font = new Font("Arial", 15);
                label.AutoSize = true;
                y += 40;
                i++;
                Controls.Add(label);
            }

            Label totalGames = new Label();
            totalGames.Location = new Point(x, y);
            totalGames.Font = new Font("Arial", 15);
            totalGames.AutoSize = true;
            totalGames.Size = new Size(225, 50);
            totalGames.Text = "Total games played: " + gamesPlayed.ToString();
            totalGames.Name = "lblTotalGames";
            Controls.Add(totalGames);
            Size = new Size(400, 400);

            Button closeBtn = new Button();
            closeBtn.Location = new Point(x + 40, y);
            closeBtn.Text = "Close";
            closeBtn.Name = "btnClose";


        }
    }
}