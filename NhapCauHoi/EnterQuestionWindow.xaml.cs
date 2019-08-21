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
using System.Windows.Shapes;

namespace NhapCauHoi
{
    /// <summary>
    /// Interaction logic for EnterQuestionWindow.xaml
    /// </summary>
    public partial class EnterQuestionWindow : Window
    {
        public string QuestionSavePath { get; set; } = "./QuestionImages";

        public string AnswerSavePath { get; set; } = "./AnswerImages";

        public BitmapSource QuestionBitmapSource { get; set; }
        public BitmapSource AnswerBitmapSource { get; set; }


        public EnterQuestionWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            QuestionBitmapSource = Clipboard.GetImage();
            questionImage.Source = QuestionBitmapSource;
        }

        private void EnterAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            AnswerBitmapSource = Clipboard.GetImage();
            answerImage.Source = AnswerBitmapSource;
        }

        private void SaveQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            #region Kiểm tra sự tồn tại của thư mục
            if (Directory.Exists(QuestionSavePath) == false)
            {
                Directory.CreateDirectory(QuestionSavePath);
            }

            if (Directory.Exists(AnswerSavePath) == false)
            {
                Directory.CreateDirectory(AnswerSavePath);
            }
            #endregion

            #region Kiểm tra đã nhập thông tin đầy đủ chưa
            if (questionImage.Source == null)
            {
                MessageBox.Show("Chưa nhập ảnh Câu Hỏi", "Lỗi !", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (answerImage.Source == null)
            {
                MessageBox.Show("Chưa nhập ảnh Câu Trả Lời", "Lỗi !", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } 
            #endregion

            var questionName = Guid.NewGuid();

            var questionImageSavePath = QuestionSavePath + "/" + questionName + ".png";
            var answerImageSavePath = AnswerSavePath + "/" + questionName + ".png";

            SaveClipboardImageToFile(QuestionBitmapSource, questionImageSavePath);
            SaveClipboardImageToFile(AnswerBitmapSource, answerImageSavePath);

            MessageBox.Show("Đã nhập câu hỏi mới", "Thông báo !", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

        public static void SaveClipboardImageToFile(BitmapSource source, string filePath)
        {
            var image = source;
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }
        }
    }
}
