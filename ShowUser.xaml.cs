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
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

using userDataBase_ADO;

namespace UserVerify_WPF
{
    /// <summary>
    /// Логика взаимодействия для ShowUser.xaml
    /// </summary>
    public partial class ShowUser : Window
    {
        private int m_userId;
        private string m_login;
        private string m_password;
        public ShowUser(in string login, in string password, in int userID)
        {
            InitializeComponent();
            m_login = login;
            m_password = password;
            userName.Text = m_login;
            userPass.Text = m_password;
            m_userId = userID;

            RenderOptions.SetBitmapScalingMode(ImageUser, BitmapScalingMode.HighQuality);
            RenderOptions.SetCachingHint(ImageUser, CachingHint.Cache);

            ShowPicture();
            
        }

        private readonly Dictionary<string, BitmapImage> _imageCache = new();

        private async void ShowPicture()
        {
            DatabaseConnection db = new DatabaseConnection();
            //Dictionary<string, BitmapImage> _imageCache = new();

            //формирую путь к файлу с изображением
            string picturePath = Directory.GetCurrentDirectory() + "\\"+db.ReadImageFromDatabase(m_userId);

            if(!string.IsNullOrEmpty(picturePath) && File.Exists(picturePath))
            {
                if(_imageCache.TryGetValue(picturePath, out var cachedImage))
                {
                    ImageUser.Source = cachedImage;
                } else
                {
                    var image = await LoadImageAsync(picturePath);
                    if(image != null)
                    {
                        _imageCache[picturePath] = image;
                        ImageUser.Source = image;
                    }
                }
            } else
            {
                ImageUser.Source = null;
            }
        }
        private async Task<BitmapImage?> LoadImageAsync(string path)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.UriSource = new Uri(path);
                    bitmap.EndInit();
                    bitmap.Freeze();
                    return bitmap;
                } catch
                {
                    return null;
                }
            });
        }

        private void PictureLoad_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".jpg|.png"; // Default file extension
            dialog.Filter = "jpg documents (.jpg)|*.jpg|(.png)|*.png|(.bmp)|*.bmp"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if(result == true)
            {
                // Open document
                string filename = dialog.FileName;
                DatabaseConnection db = new DatabaseConnection();
                db.SaveImageToDatabase(filename, m_userId);
                ShowPicture();
            }
        }
    }
}
