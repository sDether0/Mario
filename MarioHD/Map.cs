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
using MarioHD;
using Point = System.Drawing.Point;


namespace MarioEngine
{
    public class Map
    {
        public static byte[,] map = new byte[540, 9000];
        private MainWindow main;

        public Map()
        {
            var mapload = File.ReadAllLines("map.txt");
            for (int i = 0; i < 540; i++)
            {
                for (int j = 0; j < 9000; j++)
                {
                    map[i, j] = byte.Parse(mapload[i/30][j/30].ToString());
                }
            }
            
        }

        public Rectangle Render()
        {
            Rectangle rect =new Rectangle();
            rect.Height = 540;
            rect.Width = 9000;
            var brs = new ImageBrush();
            brs.ImageSource = new BitmapImage(new Uri("map.bmp", UriKind.Relative));
            rect.Fill = brs;
            Canvas.SetLeft(rect, 5);
            Canvas.SetTop(rect, 5);
            

            return rect;
        }

        public async Task Move(Rectangle rect, int x)
        {
            Canvas.SetLeft(rect, -x);
        }
    }
}
