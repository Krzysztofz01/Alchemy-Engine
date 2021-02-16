using AlchemyEngine.Utility.Processing;
using AlchemyEngine.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var btmp = new Bitmap(@"D:\Grafika\Przerobione\DSC_04842.jpg");
            //btmp.Grayscale().Save(@"D:\Grafika\Przerobione\domgreenlightTEST.png");

            //picBox.Image = btmp;


            btmp = btmp.Blur(Kernel.Conv3x3);
            btmp.Save(@"D:\Grafika\Przerobione\DSC_0484222.jpg");

            //Palette.GetGridPalleteImage(btmp, 50, 23, @"D:\Grafika\Przerobione");

        }

        private void picBox_Click(object sender, EventArgs e)
        {

        }
    }
}
