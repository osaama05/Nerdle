using Nerdle_Lopputyö.Forms;

namespace Nerdle_Lopputyö
{
    public partial class Nerdle : Form
    {
        private TextBox lastFocused;
        private TextBox currentlyFocused;
        private List<TextBox> textBoxes = new();
        private List<TextBox> currentRowsBoxes = new();
        private List<Button> keyboard = new();
        private string answer;
        private string input;
        private int currentTxtBox = 0;
        private int currentRow = 0;
        private int rowLength = 8;
        private int numberOfRows = 6;
        private Files files = new Files();
        private Help help = null;
        private Stats stats = null;
        StartMenu start = new StartMenu();

        
        public Nerdle()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.SlateGray;
            MaximizeBox = false;

            start.ShowDialog();
            numberOfRows = start.NumOfRows;

            CreateTextBoxes();
            CreateButtons();
            Size = new Size(765, Convert.ToInt32(keyboard.Last().Location.Y) + 125);

            for (int i = 0; i < rowLength; i++)
            {
                currentRowsBoxes.Add(textBoxes[i]);
            }

            files.CreateFile(numberOfRows, "Stats.txt");
            files.ChooseAnswer();
            answer = files.ReturnAnswer();
        }


        private void CreateTextBoxes()
        {
            int x = 100;
            int y = 50;
            int row = 1;
            int col = 1;

            for (int i = 0; i < (rowLength * numberOfRows); i++)
            {
                if (col > rowLength)
                {
                    col = 1;
                    row++;
                    y += 55;
                    x = 100;
                }

                TextBox txtBox = new TextBox();
                x += 55;
                txtBox.Location = new Point(x, y);
                txtBox.Size = new Size(50, 50);
                txtBox.BackColor = Color.FloralWhite;
                txtBox.ReadOnly = true;
                txtBox.Name = "txtBox" + row + "_" + col;
                txtBox.TabIndex = i;
                txtBox.Multiline = true;
                txtBox.TextAlign = HorizontalAlignment.Center;
                txtBox.AutoSize = true;
                txtBox.Font = new Font("Arial", 16);
                txtBox.Enter += new EventHandler(textBoxFocusEnter);
                txtBox.Leave += new EventHandler(textBoxFocusLost);

                col++;
                Controls.Add(txtBox);
                textBoxes.Add(txtBox);
            }
        }

        private void CreateButtons()
        {
            int x = 100;
            int y = Convert.ToInt32(textBoxes.Last().Location.Y) + 100;

            List<string> texts = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "Plus", "Minus", "Times", "Divide", "Equals", "Enter", "Delete" };
            for (int i = 0; i < texts.Count(); i++)
            {
                Button btn = new Button();
                btn.Name = "btn" + texts[i];
                btn.Click += new EventHandler(ButtonClick);

                if (texts[i] == "Enter")
                {
                    btn.Size = new Size(77, 50);
                    btn.Font = new Font("Arial", 11);
                    btn.Text = texts[i];
                }

                else if (texts[i] == "Delete")
                {
                    btn.Size = new Size(77, 50);
                    btn.Font = new Font("Arial", 11);
                    btn.Text = texts[i];
                    x += 28;
                }

                else if (texts[i] == "Equals")
                {
                    btn.Size = new Size(50, 50);
                    btn.Font = new Font("Arial", 20);
                    btn.Text = "=";
                    keyboard.Add(btn);
                }

                else if (texts[i] == "Times")
                {
                    btn.Size = new Size(50, 50);
                    btn.Font = new Font("Arial", 20);
                    btn.Text = "*";
                    keyboard.Add(btn);
                }

                else if (texts[i] == "Divide")
                {
                    btn.Size = new Size(50, 50);
                    btn.Font = new Font("Arial", 20);
                    btn.Text = "/";
                    keyboard.Add(btn);
                }

                else if (texts[i] == "Minus")
                {
                    btn.Size = new Size(50, 50);
                    btn.Font = new Font("Arial", 20);
                    btn.Text = "-";
                    keyboard.Add(btn);
                }

                else if (texts[i] == "Plus")
                {
                    btn.Size = new Size(50, 50);
                    btn.Font = new Font("Arial", 20);
                    x = 155;
                    y += 55;
                    btn.Text = "+";
                    keyboard.Add(btn);
                }

                else
                {
                    btn.Size = new Size(50, 50);
                    btn.Font = new Font("Arial", 15);
                    btn.Text = texts[i];
                    keyboard.Add(btn);
                }

                btn.Location = new Point(x, y);
                x += 55;
                btn.BackColor = Color.FloralWhite;
                Controls.Add(btn);
            }
        }


        //Source: https://stackoverflow.com/questions/12005082/textbox-focus-check
        private void load(object sender, EventArgs e)
        {
            foreach (TextBox box in textBoxes)
            {
                box.LostFocus += textBoxFocusLost;
            }

            foreach (TextBox box in textBoxes)
            {
                box.GotFocus += textBoxFocusEnter;
            }
        }

        private void textBoxFocusLost(object sender, EventArgs e)
        {
            lastFocused = (TextBox)sender;
        }

        /// <summary>
        /// If the textbox that got focus is on the current row, lets the textbox get the focus.
        /// If the textbox that got focus is not on the current row, focuses back to the previous textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFocusEnter(object sender, EventArgs e)
        {
            currentlyFocused = (TextBox)sender;

            if (lastFocused == null)
            {
                textBoxes.First().Focus();
                currentTxtBox = currentlyFocused.TabIndex;
            }

            else if (currentRowsBoxes.Contains(textBoxes[currentTxtBox]))
            {
                currentTxtBox = currentlyFocused.TabIndex;
            }

            else
            {
                lastFocused.Focus();
                currentTxtBox = currentlyFocused.TabIndex;
            }
        }


        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolStripMenuItemHelp_Click(object sender, EventArgs e)
        {
            if (help == null || help.Text == "")
            {
                help = new Help();
                help.Show();
            }

            if (CheckOpened(help.Text))
            {
                help.WindowState = FormWindowState.Normal;
                help.Focus();
            }

        }

        private void ToolStripMenuItemScore_Click(object sender, EventArgs e)
        {
            if (stats == null || stats.Text == "")
            {
                stats = new Stats();
                stats.Show();
            }

            if (CheckOpened(stats.Text))
            {
                stats.WindowState = FormWindowState.Normal;
                stats.Focus();
            }
        }


        private void ButtonClick(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            switch (pressedButton.Name)
            {
                case "btn1":
                    lastFocused.Text = "1";
                    SwitchFocus();
                    break;

                case "btn2":
                    lastFocused.Text = "2";
                    SwitchFocus();
                    break;

                case "btn3":
                    lastFocused.Text = "3";
                    SwitchFocus();
                    break;

                case "btn4":
                    lastFocused.Text = "4";
                    SwitchFocus();
                    break;

                case "btn5":
                    lastFocused.Text = "5";
                    SwitchFocus();
                    break;

                case "btn6":
                    lastFocused.Text = "6";
                    SwitchFocus();
                    break;

                case "btn7":
                    lastFocused.Text = "7";
                    SwitchFocus();
                    break;

                case "btn8":
                    lastFocused.Text = "8";
                    SwitchFocus();
                    break;

                case "btn9":
                    lastFocused.Text = "9";
                    SwitchFocus();
                    break;

                case "btn0":
                    lastFocused.Text = "0";
                    SwitchFocus();
                    break;

                case "btnPlus":
                    lastFocused.Text = "+";
                    SwitchFocus();
                    break;

                case "btnMinus":
                    lastFocused.Text = "-";
                    SwitchFocus();
                    break;

                case "btnTimes":
                    lastFocused.Text = "*";
                    SwitchFocus();
                    break;

                case "btnDivide":
                    lastFocused.Text = "/";
                    SwitchFocus();
                    break;

                case "btnEquals":
                    lastFocused.Text = "=";
                    SwitchFocus();
                    break;

                case "btnDelete":
                    btnDelete_Click();
                    break;

                case "btnEnter":
                    btnEnter_Click();
                    break;
            }
        }


        private void btnEnter_Click()
        {
            foreach (TextBox t in currentRowsBoxes)
            {
                input += t.Text;
            }

            if (input.Length == rowLength)
            {
                CheckAnswer();
                currentRow++;

                if (currentRowsBoxes[0].Name == "txtBox" + numberOfRows + "_1")
                {
                    if (input == answer)
                    {
                        WinEnding();
                    }

                    else
                    {
                        LoseEnding();
                    }
                }

                else if (currentRowsBoxes[0].Name == "txtBox" + currentRow + "_1")
                {
                    SwitchRow(currentRow);

                    SwitchFocus();
                }
            }

            else
            {
                MessageBox.Show("Row must be full");
                input = "";
            }

        }

        private void btnDelete_Click()
        {
            if (currentlyFocused == currentRowsBoxes.First())
            {
                currentlyFocused.Text = "";
                textBoxes[currentTxtBox].Focus();
            }

            else if (textBoxes[currentTxtBox].Text.Length == 0)
            {
                currentTxtBox--;
                textBoxes[currentTxtBox].Focus();
                currentlyFocused.Text = "";
            }

            else
            {
                currentlyFocused.Text = "";
                currentlyFocused.Focus();
            }
        }


        /// <summary>
        /// Switches focus to the next textbox on the current row
        /// </summary>
        private void SwitchFocus()
        {
            if (currentlyFocused == currentRowsBoxes.Last())
            {
                textBoxes[currentTxtBox].Focus();
            }

            else
            {
                currentTxtBox++;
                textBoxes[currentTxtBox].Focus();
            }
        }


        /// <summary>
        /// Checks if the users input was correct and changes the textboxes and buttons colors according to that
        /// </summary>
        private void CheckAnswer()
        {
            for (int i = 0; i < rowLength; i++)
            {
                if (input == answer)
                {
                    foreach (TextBox t in currentRowsBoxes)
                    {
                        t.BackColor = Color.LimeGreen;
                    }

                    WinEnding();
                }

                else if (answer.Contains(input[i]))
                {
                    if (input[i] == answer[i])
                    {
                        currentRowsBoxes[i].BackColor = Color.LimeGreen;
                        currentRowsBoxes[i].ForeColor = Color.WhiteSmoke;

                        foreach (Button b in keyboard)
                        {
                            if (Convert.ToChar(b.Text) == input[i])
                            {
                                b.BackColor = Color.LimeGreen;
                                b.ForeColor = Color.WhiteSmoke;
                                break;
                            }
                        }

                    }

                    else
                    {
                        currentRowsBoxes[i].BackColor = Color.DarkOrange;
                        currentRowsBoxes[i].ForeColor = Color.WhiteSmoke;

                        foreach (Button b in keyboard)
                        {
                            if (Convert.ToChar(b.Text) == input[i])
                            {
                                if (b.BackColor != Color.LimeGreen)
                                {
                                    b.BackColor = Color.DarkOrange;
                                    b.ForeColor = Color.WhiteSmoke;
                                    break;
                                }
                            }
                        }
                    }
                }

                else
                {
                    currentRowsBoxes[i].BackColor = Color.DimGray;
                    currentRowsBoxes[i].ForeColor = Color.WhiteSmoke;

                    foreach (Button b in keyboard)
                    {
                        if (Convert.ToChar(b.Text) == input[i])
                        {
                            b.BackColor = Color.DimGray;
                            b.ForeColor = Color.WhiteSmoke;
                            break;
                        }
                    }

                }
            }
        }

        //https://stackoverflow.com/questions/3861602/how-to-check-if-a-windows-form-is-already-open-and-close-it-if-it-is
        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void SwitchRow(int curRow)
        {
            currentRowsBoxes.Clear();

            for (int i = rowLength * curRow; i < rowLength * (curRow + 1); i++)
            {
                currentRowsBoxes.Add(textBoxes[i]);
            }

            input = "";
        }
        private void WinEnding()
        {
            files.SaveAnswer(currentRow);
            DialogResult result = MessageBox.Show("You were right!\nThe correct answer was: " + answer + "\nWould you like to try again?", "The End", MessageBoxButtons.YesNo);
            CloseProgram(result);
        }

        private void LoseEnding()
        {
            files.SaveAnswer(numberOfRows);
            DialogResult result = MessageBox.Show("You didn't guess the calculation :(\nThe correct answer was: " + answer + "\nWould you like to try again?", "The End", MessageBoxButtons.YesNo);
            CloseProgram(result);
        }

        private void CloseProgram(DialogResult result)
        {
            if (result == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
            else
            {
                Environment.Exit(0);

            }
        }

        private void ToolStripMenuItemHC_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to enable hardcore mode?", "Hardcore Mode", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                Environment.Exit(0);

            }
        }
    }
    
}