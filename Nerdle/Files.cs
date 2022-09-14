using System.Reflection;

namespace Nerdle_Lopputyö
{
    public class Files
    {
        private string _answer;
        private List<string> _stats = new();
        private List<string> _lines = new();

        public Files()
        {

        }


        //https://www.codeproject.com/Questions/5274073/Read-text-from-resources-Csharp
        //https://stackoverflow.com/questions/3314140/how-to-read-embedded-resource-text-file/3314213#3314213
        private string GetEmbeddedResource(string filename)
        {
            string line;
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(filename));


            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                if (filename == "Calculations.txt")
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        _lines.Add(line);
                    }
                    return _lines.ToString();
                }

                else
                {
                    return filename + " Doesn't exist";
                }
            }
        }

        internal void ChooseAnswer()
        {
            Random rand = new Random();

            try
            {
                GetEmbeddedResource("Calculations.txt");
                var randomLineNumber = rand.Next(0, _lines.Count - 1);
                _answer = _lines[randomLineNumber];
                //MessageBox.Show(answer_);
            }
            catch
            {
                MessageBox.Show("There was a problem reading the Calculations file");
            }
        }

        internal void CreateFile(int lineAmount, string fileName)
        {
            string filePath = String.Format(fileName, Application.StartupPath);

            if (!File.Exists(filePath))
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        for (int i = 0; i < lineAmount + 1; i++)
                        {
                            writer.WriteLine("0");
                        }
                        writer.Close();
                    }
                }

                catch
                {
                    MessageBox.Show("There was a problem creating a file");
                }
            }
        }

        internal void SaveAnswer(int currentLine)
        {
            ReturnStatsFromFile();
            string filePath = String.Format("Stats.txt", Application.StartupPath);

            int scoreInt = Convert.ToInt32(_stats[currentLine]);
            scoreInt += 1;
            _stats[currentLine] = scoreInt.ToString();
            File.Delete(filePath);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string s in _stats)
                {
                    writer.WriteLine(s);
                }
                writer.Close();
            }
        }

        internal List<string> ReturnStatsFromFile()
        {
            string filePath = String.Format("Stats.txt", Application.StartupPath);

            _stats = File.ReadAllLines(filePath).ToList();
            return _stats;
        }

        internal string ReturnAnswer()
        {
            return _answer;
        }
    }
}