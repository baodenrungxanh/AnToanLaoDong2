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

namespace NhapCauHoi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Đường dẫn lưu câu hỏi
        public string QuestionSavePath { get; set; } = "./QuestionImages";

        public string AnswerSavePath { get; set; } = "./AnswerImages";

        public int CurrentQuestion { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewQuestionButton_Click(object sender, RoutedEventArgs e)
        {

            new EnterQuestionWindow().ShowDialog();
        }

        private void ChangeQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(QuestionSavePath) == false)
            {
                MessageBox.Show("Không tìm thấy CÂU HỎI", "Lỗi !", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var imageLinks = Directory.GetFiles(QuestionSavePath);
            var questionCount = imageLinks.Count();
            var random = new Random();

            CurrentQuestion = random.Next(0, questionCount);

            var imagePath = AppDomain.CurrentDomain.BaseDirectory +  imageLinks[CurrentQuestion];
            questionImage.Source = new BitmapImage(new Uri(imagePath));
            answerImage.Source = null;
        }

        private void ShowAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(AnswerSavePath) == false)
            {
                MessageBox.Show("Không tìm thấy CÂU TRẢ LỜI", "Lỗi !", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var imageLinks = Directory.GetFiles(AnswerSavePath);
            var imagePath = AppDomain.CurrentDomain.BaseDirectory + imageLinks[CurrentQuestion];
            answerImage.Source = new BitmapImage(new Uri(imagePath));
        }
    }
}
