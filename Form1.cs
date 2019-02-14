using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Photoshop__Asciify_Project
{
    public partial class Form1 : Form
    {
        //fields
        BitmapAscii getClass;  //new instance of class
        Bitmap firstPic;

        //methods
        public Form1()
        {
            InitializeComponent();
            getClass = new BitmapAscii(); //initilize class
        }


        private void importBitmapToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog()){

                openFile.InitialDirectory = "C:\\temp\\";
                openFile.Filter = "Bitmap Image (*.bmp)|*.bmp|JPEG|*.jpg|Any File (*.*)|*.*";
                openFile.FilterIndex = 3;
                openFile.RestoreDirectory = true;

                if(openFile.ShowDialog() == DialogResult.OK)
                {
                    firstPic = new Bitmap (openFile.FileName);
                    pictureBox1.Image = firstPic;
                    pictureBox2.Image = getClass.GrayBitmap((Bitmap)firstPic.Clone());
                }
            }
        }//end import bitmap


        private void button1_Click(object sender, EventArgs e)
        {
            //arguments (bitmap; kernel size x & y; ascii symbols)
            richTextBox1.Text = getClass.Asciitize(firstPic, (int)numericUpDownX.Value, (int)numericUpDownY.Value, textBoxBlack.Text, textBoxVDG.Text, textBoxDGray.Text, textBoxGray.Text, textBoxLGray.Text, textBoxWhite.Text);
        }//end aciify button click


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }//class
}//name
