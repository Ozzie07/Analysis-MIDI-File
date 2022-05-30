using System;
namespace ehMIDILib
{
    public class EventBase
    {
        /// <summary>
        /// 해당 이벤트가 언제 발생해야하는지를 알려주는 값.
        /// </summary>
        public int Delta
        {
            get;
            private set;
        }

        public byte EventType
        { 
            get;
        }


        public byte[] Buffer
        {
            get;
        }

        public EventBase(byte eventType, int delta, byte[] buffer)
        {
            EventType = eventType;
            Delta = delta;
            Buffer = buffer;
        }

        public static EventBase Parsing(byte[] buffer, ref int offset, EventBase eventBase)
        {
            int oldOffset = offset;
            int delta = StaticFunctions.ReadDeltaTime(buffer, ref offset);
            if(buffer[offset] == 0xFF)
            {
                offset++;
                return MetaEvent.MakeEvent(delta, buffer, ref offset, oldOffset);
            }

            if(buffer[offset]<0xF0)
            {
                return MIDIEvent.MakeEvent(buffer[offset++], delta, buffer, ref offset, oldOffset, eventBase.EventType);
            }

            return SysEvent.MakeEvent(buffer[offset++], delta, buffer, ref offset, oldOffset);
            return null;
        }
    }
}