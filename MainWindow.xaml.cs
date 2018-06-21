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

            //rx_dlg.DefaultExt = ".txt";
            //rx_dlg.Filter = "Text documents (.txt)|*.txt";

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

            //tx_dlg.DefaultExt = ".txt";
            //tx_dlg.Filter = "Text documents (.txt)|*.txt*";

            // Display Open File Dialog
            // result is declared implict 'aive hi'
            // Nullable<bool> result = rx_dlg.ShowDialog();
            var result = tx_dlg.ShowDialog();

            if(result==true)
            {
                // Get the filename from dialog box
                FilenameTextBox2.Text = tx_dlg.FileName;

                // store the user updated filename
                tx_FileLocation = FilenameTextBox2.Text;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            FilenameTextBox2.Clear();
            FilenameTextBox2.Text = FilenameTextBox1.Text;
            tx_FileLocation = FilenameTextBox2.Text;

            BrowseButton2.Visibility = Visibility.Hidden;
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            // The below 2 lines of code will work too. 
            // Copy file loc. from textbox when convert button is pressed
            rx_FileLocation = FilenameTextBox1.Text;
            tx_FileLocation = FilenameTextBox2.Text;

            try
            {
                string text = System.IO.File.ReadAllText(@rx_FileLocation);

                char[] ar = text.ToCharArray();
                
                for(int i = 0;i<ar.Length;i++)
                {
                    if (ar[i] != '\n')
                        ar[i] = (char)((int)ar[i] ^ 2);
                    else
                        ar[i] = Environment.NewLine.ToCharArray()[0] ;
                }

                string newS = new string(ar);

                Console.WriteLine(newS);

                System.IO.File.WriteAllText(@tx_FileLocation, newS);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
            

        }
    }
}
