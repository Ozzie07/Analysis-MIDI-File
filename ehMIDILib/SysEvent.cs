using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ehMIDILib
{
    public class SysEvent : EventBase
    {

        public byte? Fdata { get; }
        public byte[] Data { get; }

        public string Description
        {
            get
            {
                switch (EventType)
                {
                    case 0xF0: return "Sytem Exclusive Message:" + StaticFunctions.HexaString(Data);
                    case 0xF1: return string.Format("MTC Quater Frame:", Fdata);
                    case 0xF2: return string.Format("Song position pointer:{0}", (Data[0] << 7) | (Data[1]));
                    case 0xF3: return string.Format("Song request:{0}", Fdata);
                    case 0xF6: return "Tune request";
                    case 0xF8: return "MIDI clock";
                    case 0xFA: return "MIDI Start";
                    case 0xFB: return "MIDI Continue";
                    case 0xFC: return "MIDI Stop";
                    case 0xFE: return "Active Sensing";
                }
                return "Not supported" + string.Format("{0:X2}", EventType);
            }
        }
        public SysEvent(byte msg, int delta, byte? fdata, byte[] data, byte[] orgbuffer) : base (msg, delta, orgbuffer)
        {
            Fdata = fdata;
            Data = data;
        }

        public static EventBase MakeEvent(byte msg, int delta, byte[] buffer, ref int offset, int oldOffset)
        {
            byte? fdata = null;
            byte[] data = null;
            if(msg == 0xF0)
            {
                int nowOffset = offset;
                while(buffer[offset] != 0x7F)
                {
                    offset++;
                }
                offset++;

                int len = offset - nowOffset;
                data = new byte[len];

                Array.Copy(buffer, nowOffset, data, offset, len);
            }
            if((msg == 0xF1)||(msg == 0xF3))
            {
                fdata = buffer[offset++];
            }
            if(msg==0xF2)
            {
                fdata = buffer[offset++];
                data = new byte[1];
                data[0] = buffer[offset++];
            }

            byte[] buffer2 = new byte[offset - oldOffset];
            Array.Copy(buffer, oldOffset, buffer2, 0, buffer2.Length);

            return new SysEvent(msg, delta, fdata, data, buffer2);
        }
    }
}
