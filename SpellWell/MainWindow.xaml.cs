using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace SpellWell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] words;
        private int wordIndex = 0;
        private int streak = 0;
        private int score = 0;
        private int total = 0;
        private string word = "";
        private bool flipWord = false;
        private string wordFilesDir = new DirectoryInfo(".").FullName + "/media/";
        private string logDir = new DirectoryInfo(".").FullName + "/";

        public MainWindow()
        {
            this.Loaded += Window_Loaded;
            InitializeComponent();
            shuffleData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SizeChanged += window_SizeChanged;
            this.PreviewKeyDown += window_KeyDown;
        }
        private void cmdPlay_Click(object sender, RoutedEventArgs e)
        {
            PlayWord();
        }

        private void PlayWord()
        {
            Uri uri = new Uri(wordFilesDir + words[wordIndex]);
            word = words[wordIndex];
            word = word.Substring(0, word.IndexOf('.'));
            this.myMediaElement.Source = uri;
            this.myMediaElement.Play();
            flipWord = true;
            txtWord.SelectAll();
            txtWord.Focus();
        }

        private void cmdPlayNext_Click(object sender, RoutedEventArgs e)
        {
            NextWord();
        }

        private void NextWord()
        {
            if (flipWord)
            {
                // force check word before next word
                MessageBox.Show("Check the spelling first", "Cannot skip check");
                return;
            }
            wordIndex++;
            if (wordIndex == words.Length)
            {
                wordIndex = 0;
            }
            Uri uri = new Uri(wordFilesDir + words[wordIndex]);
            word = words[wordIndex];
            word = word.Substring(0, word.IndexOf('.'));
            this.myMediaElement.Source = uri;
            this.myMediaElement.Play();
            flipWord = true;
            txtResult.Text = "";
            txtWord.SelectAll();
            txtWord.Focus();
        }

        private void cmdCheck_Click(object sender, RoutedEventArgs e)
        {
            CheckWord();
        }

        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            txtWord.Text = "Width:" + this.Width + " Height:" + this.Height;
        }

        private void txtWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                CheckWord();
            }
        }
        private void window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F8)
            {
                NextWord();
            }

            if (e.Key == Key.F7)
            {
                PlayWord();
            }
        }

        private void CheckWord()
        {
            if (!flipWord)
            {
                return;
            }

            if (txtWord.Text.Length == 0)
            {
                MessageBox.Show("No spelling to check", "Spelling Missing");
                return;
            }

            if (txtResult.Text == "Correctamundo!")
            {
                // repeat check of word that was spelled correctly already.
                flipWord = false;
                return;
            }

            total += 1;
            string result = "";
            if (txtWord.Text.ToLower() == word.ToLower())
            {
                streak += 1;
                score += 1;
                txtResult.Foreground = Brushes.Green;
                txtResult.Text = "Correctamundo!";
                result = "Yes";
            }
            else
            {
                streak = 0;
                txtResult.Foreground = Brushes.Red;
                txtResult.Text = "Nope! Try again.";
                result = "No";
            }
            txtStreak.Text = "Streak: " + streak.ToString();
            txtCount.Text = String.Format("Correct/Total: {0}/{1}", score, total);
            flipWord = false;
            logResult(result, word, txtWord.Text, streak, score, total);
        }

        private void logResult(string result, string word, string spelling, int streak, int score, int total)
        {
            string path = logDir + @"\Test.log.txt";
            string logText = $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} {result}:{word}:{spelling}:{streak}:{score}:{total}";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(logText);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(logText);
                }
            }
        }

        private void shuffleData()
        {
            words = getData();
            Random r = new Random();

            for (int i = (words.Length - 1); i > 0; i--)
            {
                int x = r.Next(i + 1);
                string y = words[x];
                words[x] = words[i];
                words[i] = y;
                //Console.WriteLine("{3}: r {0} - {1} <--> {2}", x, y, words[x], i);
            }
        }

        private string[] getData()
        {
            return new string[]
            {"always.mp3",
            "around.mp3",
            "best.mp3",
            "both.mp3",
            "bring.mp3",
            "Christmas.mp3",
            "clean.mp3",
            "cycle.mp3",
            "draw.mp3",
            "drink.mp3",
            "east.mp3",
            "edge.mp3",
            "either.mp3",
            "energy.mp3",
            "every.mp3",
            "family.mp3",
            "full.mp3",
            "gift.mp3",
            "going.mp3",
            "green.mp3",
            "group.mp3",
            "his.mp3",
            "hold.mp3",
            "hot.mp3",
            "how.mp3",
            "identify.mp3",
            "if.mp3",
            "juice.mp3",
            "jump.mp3",
            "jungle.mp3",
            "just.mp3",
            "keep.mp3",
            "length.mp3",
            "lever.mp3",
            "light.mp3",
            "little.mp3",
            "live.mp3",
            "made.mp3",
            "may.mp3",
            "much.mp3",
            "north.mp3",
            "off.mp3",
            "old.mp3",
            "once.mp3",
            "or.mp3",
            "over.mp3",
            "own.mp3",
            "pick.mp3",
            "pull.mp3",
            "pyramid.mp3",
            "quiet.mp3",
            "right.mp3",
            "rise.mp3",
            "said.mp3",
            "side.mp3",
            "sleep.mp3",
            "solid.mp3",
            "soon.mp3",
            "sort.mp3",
            "south.mp3",
            "stop.mp3",
            "take.mp3",
            "tell.mp3",
            "their.mp3",
            "them.mp3",
            "they.mp3",
            "those.mp3",
            "under.mp3",
            "upon.mp3",
            "us.mp3",
            "vertex.mp3",
            "walk.mp3",
            "warm.mp3",
            "when.mp3",
            "work.mp3",
            "would.mp3",
            "write.mp3",
            "xylophone.mp3",
            "young.mp3",
            "your.mp3",
            "zero.mp3"
            };
        }
    }
}
