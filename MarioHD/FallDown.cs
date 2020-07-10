using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioEngine
{
    public class FallDown
    {
        public delegate void XChangeHandler(object sender, EventArgs e);
        public event XChangeHandler XChange;
        public delegate void YChangeHandler(object sender, EventArgs e);
        public event YChangeHandler YChange;
        
        int x, y;
        public bool falling = true;
        public bool staying = false;

        public int X
        {
            get { return x; }
            set
            {
                x = value;
                if (XChange != null)
                {
                    EventArgs e = new EventArgs();
                    XChange(this, e);
                }
            }
        }

        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                if (YChange != null)
                {
                    EventArgs e = new EventArgs();
                    YChange(this, e);
                }
            }
        }

        public async Task Down()
        {
            
            while (true)
            {
                if (falling)
                {
                    if (y-26 > 1)
                    {
                        if (Map.map[539 - (y - 30), x - 4] < 3 &&
                            Map.map[539 - (y - 30), x + 25] < 3)
                        {
                            staying = false;
                            Y -= 4;
                        }
                        else
                        {
                            if (Map.map[539 - (y - 28), x - 4] > 2 ||
                                Map.map[539 - (y - 28), x + 25] > 2)
                            {
                                Y++;
                            }

                            staying = true;
                        }
                    }
                    else
                    {
                        Ded();
                    }
                }
                
                

                await Task.Delay(1);
            }
        }

        public virtual void Ded()
        {

        }
    }
}
