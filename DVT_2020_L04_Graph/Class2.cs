using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;

namespace DVT_2020_L04_Graph
{

    public struct CANDumpData
    {
        public UInt32 TickStamp; // time stamp
        public byte Priority;
        public byte EDP;
        public byte DP;
        public byte PDUFormat;
        public byte PDUSpecific;
        public byte SourceAddress;
        public byte DLC;
        public byte b1;
        public byte b2;
        public byte b3;
        public byte b4;
        public byte b5;
        public byte b6;
        public byte b7;
        public byte b8;

        public CANDumpData(uint tickStamp, byte priority, byte eDP, byte dP, byte pDUFormat, byte pDUSpecific, byte sourceAddress, byte dLC, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7, byte b8)
        {
            TickStamp = tickStamp;
            Priority = priority;
            EDP = eDP;
            DP = dP;
            PDUFormat = pDUFormat;
            PDUSpecific = pDUSpecific;
            SourceAddress = sourceAddress;
            DLC = dLC;
            this.b1 = b1;
            this.b2 = b2;
            this.b3 = b3;
            this.b4 = b4;
            this.b5 = b5;
            this.b6 = b6;
            this.b7 = b7;
            this.b8 = b8;
        }


    }
    

}
