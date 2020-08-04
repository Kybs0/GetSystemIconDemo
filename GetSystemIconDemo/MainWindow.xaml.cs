using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Image = System.Windows.Controls.Image;
using Size = System.Drawing.Size;

namespace GetSystemIconDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            var text = InputTextBox.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            text = text.Replace(@"""", string.Empty);
            if (File.Exists(text))
            {
                var filePath = text;
                //方案1 只能获取小图标
                //var fileIcon = SystemIconHelper.GetFileIcon(filePath);
                //var imageSource = fileIcon.ToImageSource();
                var image = new Image();
                //方案2 获取大图标
                image.Source = ThumbnailUtil.GetLargeIcon(filePath);
                ImagesPanel.Children.Add(image);
            }
            else if (Directory.Exists(text))
            {
                var folderPath = text;
                var folderIcon = SystemIconHelper.GetFileIcon(folderPath);
                var imageSource = folderIcon.ToImageSource();
                var image = new Image();
                image.Source = imageSource;
                ImagesPanel.Children.Add(image);
            }
        }
    }
}
