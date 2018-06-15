using System;
using System.Collections.Generic;
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

namespace FileReader.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnOpenSelectedFile(object sender, RoutedEventArgs e)
        {
            /// Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            /// Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            /// Get the selected file name and display in a TextBox 
            if (result == true)
            {
                /// Open document 
                var file_path = dlg.FileName;
                OpenFile(file_path);             
            }
        }

        /// <summary>
        /// Open file pointed by the supplied path
        /// </summary>
        /// <param name="filePath"></param>
        private void OpenFile(string filePath)
        {
            var fileReader = GetFileReaderInstance();

            string text;
            ///Check if decryption feature is needed
            if (Encryption.IsChecked.Value)
            {
                var decrypter = new ReverseDecrypter();
                fileReader = new DecryptionDecorator(fileReader, decrypter);
            }
            ///Check if role-based security system feature is needed
            if (RoleBasedSecurity.IsChecked.Value)
            {
                var securitySystem = new DummySecuritySystem();

                if (!Enum.TryParse(UserRole.Text, out UserRoles role))
                    throw new ArgumentException("Unable to parse selected role: " + UserRole.Text);

                fileReader = new RoleBasedSecurityDecorator(fileReader, securitySystem, role);
            }

            fileReader.TryReadFile(filePath, out text);
            MessageBox.Show(text);
        }

        private IFileReader GetFileReaderInstance()
        {
            if (!Enum.TryParse(FileType.Text, out SupportedTypes selectedType))
                throw new ArgumentException("Unable to parse selected type: "+ FileType.Text);         

            if (selectedType == SupportedTypes.Text)
                ///Text
                return new TextReader();
            else if (selectedType == SupportedTypes.XML)
                ///XML
                return new XMLReader();
            else if (selectedType == SupportedTypes.JSON)
                ///JSON
                return new JSONReader();
            else
                throw new ArgumentException("Unexpected selected file type: " + selectedType);
        }

        private void OnRoleBasedSecurityChecked(object sender, RoutedEventArgs e)
        {
            UserRole.IsEnabled = true;
        }

        private void OnRoleBasedSecurityUnChecked(object sender, RoutedEventArgs e)
        {
            UserRole.IsEnabled = false;
        }

        private void UserRole_Loaded(object sender, RoutedEventArgs e)
        {
            ///Populate UserRoles list dynamically
            List<string> data = new List<string>();
            foreach (UserRoles role in Enum.GetValues(typeof(UserRoles)))
            {
                data.Add(role.ToString());
            }

            //Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            //Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            //Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private void FileType_Loaded(object sender, RoutedEventArgs e)
        {
            ///Populate FileTypes list dynamically
            List<string> data = new List<string>();
            foreach (SupportedTypes type in Enum.GetValues(typeof(SupportedTypes)))
            {
                data.Add(type.ToString());
            }

            ///Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            ///Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            ///Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private void FileType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /// Get the ComboBox.
            var comboBox = sender as ComboBox;

            /// Set SelectedItem as Window Title.
            string value = comboBox.SelectedItem as string;
            this.Title = "Selected: " + value;
        }
    }
}
