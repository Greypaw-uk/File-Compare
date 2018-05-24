using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Win32;


namespace File_Compare
{
    /// <summary>
    /// Compares two files to tell if they are the same or not
    /// </summary>

    public partial class MainWindow : Window
    {
        public FileInfo file1 = null;
        public FileInfo file2 = null;
        public TextRange textrange = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_imp1_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                file1 = new FileInfo(openFileDialog.FileName);

                textBox1.Text = Convert.ToString(file1.Name);
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a file to continue");
            }

        }

        private void Btn_imp2_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                file2 = new FileInfo(openFileDialog.FileName);

                textBox2.Text = Convert.ToString(file2.Name);
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a file to continue");
            }

        }

        private void Btn_compare_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                CompareName();
                CompareDate();
                CompareSize();
                DisplayContents();
                CompareFiles();
            }
            catch (Exception)
            {
                MessageBox.Show("Please select two files to compare");
            }

        }

        private void CompareName()
        {
            if (file1.Name == file2.Name)
            {
                rtb_name.Background = Brushes.GreenYellow;
            }
            else
            {
                rtb_name.Background = Brushes.Red;
            }
        }

        private void CompareDate()
        {
            if (file1.CreationTime == file2.CreationTime)
            {
                rtb_date.Background = Brushes.GreenYellow;
            }
            else
            {
                rtb_date.Background = Brushes.Red;
            }
        }

        private void CompareSize()
        {
            if (file1.Length == file2.Length)
            {
                rtb_size.Background = Brushes.GreenYellow;
            }
            else
            {
                rtb_size.Background = Brushes.Red;
            }
        }

        private void DisplayContents()
        {
            if (File.Exists(file1.FullName))
            {
                rtb_cont1.Document.Blocks.Clear();

                rtb_cont1.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(file1.ToString()))));
            }
        }

        private void CompareFiles()
        {

            string[] Lines1 = File.ReadAllLines(file1.FullName);
            string[] Lines2 = File.ReadAllLines(file2.FullName);

            rtb_cont2.Document.Blocks.Clear();

            for (int line = 0; line < Lines1.Length; line++)
            {
                if (line < Lines2.Length)
                {
                    if (Lines1[line] == (Lines2[line]))
                    {
                        highlighter(rtb_cont2, Lines2[line], null);
                    }
                    else
                    {
                        highlighter(rtb_cont2, Lines2[line], Brushes.Gold );
                    }
                }  
            }
        }

        private void highlighter(RichTextBox richTextBox, String word, SolidColorBrush color)
        {
            TextRange textrange = new TextRange(rtb_cont2.Document.ContentEnd,rtb_cont2.Document.ContentEnd);
            textrange.Text = word;
            textrange.ApplyPropertyValue(TextElement.BackgroundProperty, color);
        }
    }
}
