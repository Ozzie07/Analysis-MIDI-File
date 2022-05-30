using System;

namespace ehMIDILib
{
    public class MetaEvent : EventBase
    {
        public MetaEvent(int delta, byte msg, byte len, byte[] data, byte[] orgbuffer) : base(0xFF, delta, orgbuffer)
        {
            Msg = msg;
            Length = len;
            Data = data;
        }

        public byte Msg
        {
            get;
        }

        public byte Length
        {
            get;
        }

        public byte[] Data
        {
            get;
        }
        public string DataString
        {
            get
            {
                if (Data == null)
                    return string.Empty;

                return StaticFunctions.GetString(Data);
            }
        }

        public string MetaDescription
        {
            get
            {
                switch(Msg)
                {

                    case 0x00: return string.Format("SeqNo: {0}" + BitConverter.ToInt16(Data, 0));
                    case 0x01: return DataString;
                    case 0x02: return "Copyright: " + DataString;
                    case 0x03: return "TrackName: " + DataString;                   
                    case 0x04: return "Instument: " + DataString;
                    case 0x05: return "Lyric: " + DataString;
                    case 0x06: return "Marker: " + DataString;
                    case 0x07: return "CuePoint: " + DataString;
                    case 0x08: return "ProgramName: " + DataString;
                    case 0x09: return "DeviceName: " + DataString;
                    case 0x20: return "Channel: " + Data[0].ToString();
                    case 0x21: return "MidiPort: " + Data[0].ToString();
                    case 0x2F: return "End of Track";                    
                    case 0x51: return "Tempo: " + MakeTempo();
                    case 0x54: return "SMPTE Offset: ";
                    case 0x58: return "TimeSignature: " + MakeTimeSig(); ;
                    case 0x59: return "KeySignature: " + MakeKeySignature();
                    case 0x7F: return "Specific Message";
                    default: return "ETC";
                }
            }
        }
        static string[] keystr = new string[] { 
            "C Flat", "G Flat", "D Flat", "A Flat", "E Flat", "B Flat", "F Flat", 
            "C", 
            "G", "D", "A", "E", "B", "F Sharp", "Sharp" 
        };


        static string[] keystr2 = new string[] { 
            "A Flat", "E Flat", "B Flat", "F", "C", "G", "D", 
            "A", 
            "E", "B", "F Sharp", "C Sharp", "G Sharp", "D Sharp", "A Sharp" 
        };

        private string MakeKeySignature()
        {
            byte key = (byte)(Data[0] + 7);
            if (Data[1] == 0)
            return keystr[key] + " Major";
            return keystr2[key] + " minor"; 
        }

        private string MakeTimeSig()
        {
            return string.Format("{0}/{1} metronome:{2}ticks", Data[0], Math.Pow(2, Data[1]), Data[2]);
        }

        private string MakeTempo()
        {
            int tempo = Data[0] << 16 | Data[1] << 8 | Data[2];
            return tempo.ToString() + "microseconds/quarter note";
        }

        public static EventBase MakeEvent(int delta, byte[] buffer, ref int offset, int oldOffset)
        {
            byte msg = buffer[offset++];
            byte len = buffer[offset++];
            byte[] data = null;

            if(msg != 0x2F) //end track이 아닐 경우.
            {
                data = new byte[len];
                Array.Copy(buffer, offset, data, 0, len);
                offset += len;
            }

            byte[] buffer2 = new byte[offset - oldOffset];
            Array.Copy(buffer, oldOffset, buffer2, 0, buffer2.Length);

            return new MetaEvent(delta, msg, len, data, buffer2);
        }
    }
}