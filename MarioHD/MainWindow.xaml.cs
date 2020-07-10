using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MarioEngine;

namespace MarioHD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle rc = new Rectangle();

        private int sp = 5;
        public Map s;
        public int mapscale = 0;
        private bool pause = false;
        public Rectangle PlayerBox;
        public Rectangle MapBox;
        private Player pr;
        //private PictureBox pn;
        private Enemy en;
        public Rectangle EnemyBox;
        private int rogress = 0;
        private bool r, l, u;
        Timer clicker = new Timer();
        public delegate void ProgresHandler(object sender, EventArgs e);
        public event ProgresHandler Progresion;
        private void Pr_PlayerDied(object sender, EventArgs e)
        {
            Progres = 0;
            mapscale = 0;
            pr.X = 60;
            pr.Y = 250;
            Canvas.SetLeft(MapBox,0);
        }
        public int Progres
        {
            get { return rogress; }
            set
            {
                rogress = value;
                if (Progresion != null)
                {
                    EventArgs e = new EventArgs();
                    Progresion(this, e);
                }
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Start.Visibility = Visibility.Hidden;
            pr = new Player();
            PlayerBox = pr.Render(this);
            CanvasMap.Children.Insert(0, PlayerBox);
            Canvas.SetZIndex(PlayerBox, 999);
            pr.XChange += Pr_XChange;
            pr.YChange += Pr_YChange;
            pr.PlayerDied += Pr_PlayerDied;
            await pr.Down();
        }

        private void SButton_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(rc,sp-=20);
        }

        public MainWindow()
        {
            
            s = new Map();
            InitializeComponent();
            clicker.Interval = 1;
            clicker.Elapsed += Clicker_Elapsed;
            Progresion += Screen_Progresion;
            
            
        }
        private void Screen_Progresion(object sender, EventArgs e)
        {
            if (Progres == 180)
            {
                en= new Enemy();
                EnemyBox = en.Render(this);
                CanvasMap.Children.Insert(0,EnemyBox);
                Canvas.SetZIndex(EnemyBox,100);
            }
        }

        private void Clicker_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (r && !l && !pause)
                {
                    if (PlayerBox.RenderedGeometry.Bounds.X + PlayerBox.Width > Width)
                    {
                        pause = true;
                        if (pr.X - Progres < Width / 2)
                        {
                            pr.MoveRight();
                        }
                        else
                        {
                            pr.MoveRight();
                        }

                        pause = false;
                    }
                    else
                    {
                        pr.MoveRight();
                    }
                }

                if (l && !r && !pause)
                {
                    pr.MoveLeft();
                }
            }), DispatcherPriority.ContextIdle);
        }

        private void Pr_YChange(object sender, EventArgs e)
        {
            Canvas.SetTop(PlayerBox, 540 - pr.Y);
        }

        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                r = true;
            }

            if (e.Key == Key.Left)
            {
                l = true;
            }
            if (e.Key == Key.Up && !u)
            {
                u = true;
                await pr.Jump();
            }
        }


        private async void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                r = false;
            }
            if (e.Key == Key.Left)
            {
                l = false;
            }

            if (e.Key == Key.Up)
            {
                u = false;
            }
        }

        private void Pr_XChange(object sender, EventArgs e)
        {
            Canvas.SetLeft(PlayerBox, pr.X - Progres);
        }
        private async void Window_Initialized(object sender, EventArgs e)
        {
            clicker.Start();
            MapBox = s.Render();
            
            CanvasMap.Children.Insert(0,MapBox);
            Canvas.SetZIndex(MapBox,0);
        }
    }
}
