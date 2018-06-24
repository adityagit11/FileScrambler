using System;
using System.Windows;
using System.Windows.Forms;

namespace FileScramblerV1._0
{
    public partial class MainWindow : Window
    {
        // OpenFileDialog box for input file and FolderBrowserDialog box for output file
        private Microsoft.Win32.OpenFileDialog input_dlg;
        private FolderBrowserDialog output_dlg;
        
        private string input_FileLocation;
        private string output_FileLocation;

        private string input_FileName;

        public MainWindow()
        {
            //InitializeComponent();
        }

        // Click event function for browse button 1
        private void BrowserButton1_Click(object sender, RoutedEventArgs e)
        {
            // Create Open File Dialog object
            input_dlg = new Microsoft.Win32.OpenFileDialog();
            
            /*
            To specify the file type for reception
            input_dlg.DefaultExt = ".txt";
            input_dlg.Filter = "Text documents (.txt)|*.txt";
            */

            // Display Open File Dialog box
            Nullable<bool> result = input_dlg.ShowDialog();

            // If the user has selected some file
            if (result == true)
            {
                // Collect file FULL PATH location
                input_FileLocation = input_dlg.FileName;
                FilenameTextBox1.Text = input_FileLocation;

                // Collect file NAME
                input_FileName = input_dlg.SafeFileName;
            }
        }

        // Click event function for browser button 2
        private void BrowseButton2_Click(object sender, RoutedEventArgs e)
        {
            // Create an object of FolderBrowserDialog
            output_dlg = new FolderBrowserDialog();

            // Open the dialog box
            DialogResult result = output_dlg.ShowDialog();

            if(result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(output_dlg.SelectedPath))
            {
                // Append the filename to selected path by user
                output_FileLocation = output_dlg.SelectedPath + "\\Conv_" + input_FileName;
                FilenameTextBox2.Text = output_FileLocation;
            }
        }

        // Checked event function for check box
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            FilenameTextBox2.Clear();

            BrowseButton2.Visibility = Visibility.Hidden;

            // input_FileLocation contains the full path to input file
            // next we insert "Conv_" word in between "C:\----Filename.cs"

            char[] temp_ar = input_FileLocation.ToCharArray();
            
            // last occurance of '\' in file path location
            int last_occ = 0;
            for (int i = temp_ar.Length-1; i>=0; i--)
                if(temp_ar[i]=='\\')
                {
                    last_occ = i;
                    break;
                }
            char[] temp_ar2 = new char[last_occ+1];
            for (int i = 0; i <= last_occ; i++)
                temp_ar2[i] = temp_ar[i];
            string s = new string(temp_ar2) + "Conv_" + input_FileName;

            output_FileLocation = s;
            FilenameTextBox2.Text = output_FileLocation;
        }

        // Click event function for convert button
        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Import the whole file contents from input file @input_FileLocation
                string rx_FileContent = System.IO.File.ReadAllText(@input_FileLocation);

                // print the no. of lines on console. (Just fun!)
                int count_NewLines = CountNewLines(rx_FileContent);
                Console.WriteLine("No. of code lines: " + count_NewLines);

                // Encode the contents of file by EX-OR opertions
                char[] rx_FileContentArray = rx_FileContent.ToCharArray();

                for(int i = 0;i<rx_FileContentArray.Length;i++)
                {
                    if (rx_FileContentArray[i] != '\n')
                        rx_FileContentArray[i] = (char)((int)rx_FileContentArray[i] ^ 1);
                    else
                        rx_FileContentArray[i] = Environment.NewLine.ToCharArray()[0];
                }

                // Create a new file by inserting new_FileContent (Encoded) data.
                string new_FileContent = new string(rx_FileContentArray);

                System.IO.File.WriteAllText(@output_FileLocation, new_FileContent);
            }
            catch (Exception err)
            {
                Console.Write(err);
            }

            System.Windows.Application.Current.Shutdown();
        }
        private int CountNewLines(string content)
        {
            int temp_CountNewLine = 0;
            for (int i = 0; i < content.Length; i++)
            {
                if (content.ToCharArray()[i] == '\n')
                    temp_CountNewLine++;
            }
            return temp_CountNewLine;
        }
    }
}
