using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFWebsiteGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal StyledWebsiteGenerator website { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_LoadHtmlSite_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Websitegenerator";
            dlg.DefaultExt = ".html";
            dlg.Filter = "HTML file (.html)|*.html";
            Nullable<bool> result = dlg.ShowDialog();

            if(result == true)
            {
                string fileName = dlg.FileName;
                string read = "";
                using (StreamReader sr = new StreamReader(fileName))
                {
                    read = sr.ReadToEnd();
                }
                TBox_LoadetHtmlSite.Text = read;
            }

        }
            
        private void Btn_SaveHtmlSite_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Websitegenerator";
            dlg.DefaultExt = ".html";
            dlg.Filter = "HTML file (.html)|*.html";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string fileName = dlg.FileName;
                using (StreamWriter sw = new StreamWriter($"{fileName}.html"))
                {
                    sw.WriteLine(TBox_LoadetHtmlSite.Text);
                }
                MessageBox.Show($"Saved as {fileName}");
                TBox_LoadetHtmlSite.Text = "väntar...";
            }
        }

        private void Btn_SaveInställningar_Click(object sender, RoutedEventArgs e)
        {
            string inputKlass = TBox_klassInput.Text;
            string[] inputTechniques = TBox_techniquesInput.Text.Split(",");
            string[] inputMeddelande = TBox_meddelandeInput.Text.Split(",");
            string färg = "";
            if(myLabel.Content.ToString() == "röd")
            {
                färg = "red";
            }else if(myLabel.Content.ToString() == "mörkblå")
            {
                färg = "darkblue";
            }else if(myLabel.Content.ToString() == "grön")
            {
                färg = "green";
            }
            StyledWebsiteGenerator perfectWebsite = new StyledWebsiteGenerator(inputKlass, färg, inputMeddelande, inputTechniques);
            perfectWebsite.Print();
        }

        private void Action(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            myLabel.Content = rb.Content.ToString();
        }
    }
}
