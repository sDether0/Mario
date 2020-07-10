using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MarioHD;


namespace MarioEngine
{
    class Enemy : Object 
    {
        public bool left = true;
        private MainWindow sc;
        private bool alive = true;
        public async void Move()
        {
            
            while (alive)
            {
                await Task.Delay(10);
                if (left)
                {
                    X--;
                }
                else
                {
                    X++;
                }
            }
        }

        public override void Ded()
        {
            alive = false;
            X = 0;
            Y = 0;

        }

        public Rectangle Render(MainWindow sc)
        {
            this.sc = sc;
            Rectangle rect = new Rectangle();
            rect.Height = 30;
            rect.Width = 30;
            X = sc.Progres + (int)sc.Width - 1;
            Y = 123;
            var brs = new ImageBrush();
            brs.ImageSource = new BitmapImage(new Uri("Mario.png", UriKind.Relative));
            rect.Fill = brs;
            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, 540 - Y);
            return rect;

        }
    }
}
