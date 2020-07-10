using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MarioHD;

//using System.Windows.Forms;

namespace MarioEngine
{
    class Player : Object
    {
        private MainWindow sff;
        
        public delegate void DiedHandler(object sender, EventArgs e);
        public event DiedHandler PlayerDied;

        public override void Ded()
        {

            if (MessageBox.Show("Respawn?", "Died", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (PlayerDied != null)
                {
                    EventArgs e = new EventArgs();
                    PlayerDied(this, e);
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
        }

        public async Task MoveLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                if (Map.map[539 - Y, X - 5] < 3 && Map.map[539 - Y + 28, X - 5] < 3 && X - sff.Progres > 3)
                {

                    X--;
                }
                else
                {
                    break;
                }
                await Task.Delay(1);
            }
        }

        public async Task MoveRight()
        {
            for (int i = 0; i < 4; i++)
            {
                if (Map.map[539 - Y , X + 29] < 3 && Map.map[539 - Y + 28, X + 29] < 3)
                {
                    if (X - sff.Progres > sff.CanvasMap.ActualWidth / 2 && Canvas.GetLeft(sff.MapBox) + sff.MapBox.ActualWidth > sff.CanvasMap.ActualWidth)
                    {
                        sff.Progres++;
                        await sff.s.Move(sff.MapBox,sff.mapscale += 1);
                    }
                    X++;
                }
                else
                {
                    break;
                }

            }

        }

        public async Task Jump()
        {
            if (staying)
            {
                staying = false;
                falling = false;
                for (int i = 0; i < 17; i++)
                {
                    if (Map.map[539 - (Y+5) , X+3 ] <3 && Map.map[539 - (Y+5), X + 26] <3)
                    {
                        await Task.Delay(1);
                        Y += 6;
                    }
                }

                await Task.Delay(40);
                falling = true;
            }
        }
        
        public Rectangle Render(MainWindow sff)
        {
            this.sff = sff;
            Rectangle rect = new Rectangle();
            rect.Height = 30;
            rect.Width = 30;
            X = 60;
            Y = 250;
            var brs = new ImageBrush();
            brs.ImageSource = new BitmapImage(new Uri("Mario.png", UriKind.Relative));
            rect.Fill = brs;
            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, 540-Y);
            return rect;
        }
    }
}
