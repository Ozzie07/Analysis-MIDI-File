using System;

namespace ehMIDILib
{
    public class MIDIEvent : EventBase
    {
        public byte FirstData
        {
            get;
        }
        public byte SecondData
        {
            get;
        }
        public string Status
        {
            get
            {
                if(EventType < 0x80)
                {
                    return "Running Status";
                }
                switch(EventType >> 4)
                {
                    case 0x8: return "Note Off";
                    case 0x9: return "Note On";
                    case 0xA: return "Note after touch";
                    case 0xB: return "Controller";
                    case 0xC: return "Change Instrument";
                    case 0xD: return "Channel after touch";
                    case 0xE: return "Pitch Bend";
                }

                return string.Empty;
            }
        }

        public string Note
        {
            get
            {
                return StaticFunctions.GetNoteName(FirstData);
            }
        }

        public string ControlData
        {
            get
            {
                return StaticFunctions.GetControlStr(SecondData);
            }
        }

        public string InstrumentName
        {
            get
            {
                return StaticFunctions.GetInstrumentName(FirstData);
            }
        }

        public int Channel
        {
            get
            {
                return EventType & 0x0F; //하위 4 비트 추출.
            }
        }

        public string Description
        {
            get
            {
                if (EventType < 0x80)
                {
                    return "Running Status";
                }
                switch (EventType >> 4)
                {
                    case 0x8: 
                    case 0x9: 
                    case 0xA: return MakeNoteVelocity();
                    case 0xB: return MakeControlChange();
                    case 0xC: return MakeInstrument();
                    case 0xD: return MakeChannel();
                    case 0xE: return MakePitchBend();
                }

                return string.Empty;
            }

        }

        private string MakePitchBend()
        {
            return string.Format("{0}:{1}:{2}:{3}", Status, Delta, FirstData & 0x7F, SecondData >> 1);
        }

        private string MakeChannel()
        {
            return string.Format("{0}:{1}:{2}", Status, Delta, FirstData);
        }

        private string MakeInstrument()
        {
            return string.Format("{0}:{1}:{2}", Status, Delta, InstrumentName);
        }

        private string MakeControlChange()
        {
            return string.Format("{0}:{1}:{2}:{3}", Status, Delta, FirstData, ControlData);
        }

        private string MakeNoteVelocity()
        {
            return string.Format("{0}:{1}:Pitch:{2}:Velocity:{3}", Status, Delta, Note, SecondData);
        }

        public MIDIEvent(byte eventType, int delta, byte firstData, byte secondData, byte[] buffer) : base(eventType, delta, buffer)
        {
            FirstData = firstData;
            SecondData = secondData;
        }

        public static EventBase MakeEvent(byte eventType, int delta, byte[] buffer, ref int offset, int oldOffset, byte prev_eventType)
        {
            byte firstData;
            byte secondData = 0;

            //running state
            if(eventType<0x80)
            {
                firstData = eventType;
                eventType = prev_eventType;
            }
            else
            {
                firstData = buffer[offset++];
            }

            switch (eventType >> 4)
            {

                //Using Second Data.
                case 0x8: //NoteOff
                case 0x9: //NoteOn
                case 0xA: //Note after touch
                case 0xB: //Controller
                case 0xE: //Pitch Bend
                    secondData = buffer[offset++];
                    break;

                case 0xC: //Change Instrument
                case 0xD: //Channel after touch1
                    break;
                default: return null;
            }
            byte[] buffer2 = new byte[offset - oldOffset];

            Array.Copy(buffer, oldOffset, buffer2, 0, buffer2.Length);

            return new MIDIEvent(eventType, delta, firstData, secondData, buffer2);
        }
    }
}