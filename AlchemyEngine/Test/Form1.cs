using AlchemyEngine.Utility.Processing;
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
            try
            {
                Palette.GetGridPalleteImage(new Bitmap(@"D:\Grafika\Przerobione\DSC_0095.png"), 25, 25, @"D:\Grafika\Inne");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
