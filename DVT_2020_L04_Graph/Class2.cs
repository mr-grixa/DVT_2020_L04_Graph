using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT_2020_L04_Graph
{
    class Frame
    {
        public int time;
        public int data1;
        public int data2;
        public Frame(int time, int data1, int data2)
        {
            this.time = time;
            this.data1 = data1;
            this.data2 = data2;
        }
        public Frame()
        {
        }
    }

}
