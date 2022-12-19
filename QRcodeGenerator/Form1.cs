using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using ZXing;

namespace QRcodeGenerator
{
    public partial class Form1 : Form
    {
        BarcodeWriter BarcodeWriter;

        public Form1()
        {
            InitializeComponent();
            Init();
        }
        //=======================================================================
        // 초기화
        //=======================================================================
        public void Init()
        {
            BarcodeWriter = new BarcodeWriter();
            BarcodeWriter.Format = BarcodeFormat.QR_CODE;
        }
        //=======================================================================
        // URL => QR code 
        //=======================================================================
        public void MakeQr(string url)
        {
           PicBox.Image =  BarcodeWriter.Write(url);
        }
        //=======================================================================
        // URL Input Box 최초 클릭시 내용 지워지도록
        //=======================================================================
        private void TBox_Input_MouseClick(object sender, MouseEventArgs e)
        {
            string tet = TBox_Input.Text.Trim().ToUpper();
            if (TBox_Input.Text.Trim().ToUpper() == "\"URL\"")
            {
                TBox_Input.Text = string.Empty;
            }
        }
        //=======================================================================
        // QR Code 생성 버튼 클릭
        //=======================================================================
        private void Btn_Gen_Click(object sender, EventArgs e)
        {
            if(TBox_Input.Text == string.Empty)
            {
                MessageBox.Show("Check URL");
                return;
            }

            MakeQr(TBox_Input.Text.Trim());
        }
        //=======================================================================
        // 파일 저장 버튼 클릭
        //=======================================================================
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPEG File(*.jpg)|*.jpg|PNG File(*.png)|*.png";
            saveFileDialog1.DefaultExt = "JPG";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                MakeJpg(fileName);
            }
        }
        //=======================================================================
        // 형식에 따른 이미지 파일 저장
        //=======================================================================
        public void MakeJpg(string filename)
        {
            switch(filename.Substring(filename.Length - 3, 3).ToUpper())
            {
                case "JPG":
                    PicBox.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                    MessageBox.Show("JPG 저장 완료");
                    break;

                case "PNG":
                    PicBox.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                    MessageBox.Show("PNG 저장 완료");
                    break;

                default:
                    MessageBox.Show("저장 형식을 확인하세요.");
                    break;
            }
        }
        //=======================================================================
    }
}
