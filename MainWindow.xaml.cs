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

namespace FileScramblerV1._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Microsoft.Win32.OpenFileDialog rx_dlg;
        private Microsoft.Win32.OpenFileDialog tx_dlg;

        private string rx_FileLocation;
        private string tx_FileLocation;

        public MainWindow()
        {
            //InitializeComponent();
            
        }

        private void BrowserButton1_Click(object sender, RoutedEventArgs e)
        {
            // Create Open FileDialog
            rx_dlg = new Microsoft.Win32.OpenFileDialog();

            rx_dlg.DefaultExt = ".cs";
            rx_dlg.Filter = "Text documents (.cs)|*.cs";

            // Display Open File Dialog
            // result is declared implict 'aive hi'
            // Nullable<bool> result = rx_dlg.ShowDialog();
            var result = rx_dlg.ShowDialog();

            if (result == true)
            {
                rx_FileLocation = rx_dlg.FileName;
                FilenameTextBox1.Text = rx_FileLocation;
            }
        }

        private void BrowseButton2_Click(object sender, RoutedEventArgs e)
        {
            // Create Open FileDialog
            tx_dlg = new Microsoft.Win32.OpenFileDialog();

            tx_dlg.DefaultExt = ".cs";
            tx_dlg.Filter = "Text documents (.cs)|*.cs*";

            // Display Open File Dialog
            // result is declared implict 'aive hi'
            // Nullable<bool> result = rx_dlg.ShowDialog();
            var result = tx_dlg.ShowDialog();

            if(result==true)
            {
                tx_FileLocation = tx_dlg.FileName;
                FilenameTextBox2.Text = tx_FileLocation;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            FilenameTextBox2.Clear();
            FilenameTextBox2.Text = FilenameTextBox1.Text;

            BrowseButton2.Visibility = Visibility.Hidden;
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = System.IO.File.ReadAllText(@rx_FileLocation);

                char[] ar = text.ToCharArray();
                int countNewLineChar = 0;
                for (int i = 0;i<ar.Length;i++)
                {
                    if (ar[i] == '\n')
                        countNewLineChar++;
                }

                Console.WriteLine(countNewLineChar);

                for(int i = 0;i<ar.Length;i++)
                {
                    if (ar[i] != '\n')
                        ar[i] = (char)((int)ar[i] ^ 2);
                }

                string newS = new string(ar);

                Console.WriteLine(newS);

                for(int i = 0;i<ar.Length;i++)
                {
                    if (ar[i] != '\n')
                        ar[i] = (char)((int)ar[i] ^ 2);
                }

                string newSS = new string(ar);

                Console.WriteLine(newSS);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
            

        }
    }
}
