using System;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Python 스크립트 실행
            Process pythonProcess = new Process();
            pythonProcess.StartInfo.FileName = @"C:\Users\김영재\AppData\Local\Programs\Python\Python311\python.exe";
            pythonProcess.StartInfo.Arguments = @"C:\pyworkspace1\camera_test\cam1.py";
            pythonProcess.StartInfo.UseShellExecute = false;
            pythonProcess.StartInfo.RedirectStandardOutput = true;
            pythonProcess.StartInfo.CreateNoWindow = true;
            pythonProcess.Start();

            string image_jpg = pythonProcess.StandardOutput.ReadToEnd().Trim();
            pythonProcess.WaitForExit();

            if (File.Exists(image_jpg))
            {
                try
                {
                    // 이미지 파일로부터 Image 객체 생성
                    Image image = Image.FromFile(image_jpg);

                    // PictureBox에 이미지 설정
                    pictureBox1.Image = ResizeImage(image, pictureBox1.Size);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지를 로드하는 동안 오류가 발생했습니다: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("이미지 파일이 존재하지 않습니다.");
            }
        }
        private Image ResizeImage(Image image, Size newSize)
        {
            Bitmap newImage = new Bitmap(newSize.Width, newSize.Height);
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, new Rectangle(Point.Empty, newSize));
            }
            return newImage;
        }
    }
}
