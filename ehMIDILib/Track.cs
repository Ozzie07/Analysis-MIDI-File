using System.Collections;
using System.Collections.Generic;

namespace ehMIDILib
{
    public class Track : Chunk, IEnumerable
    {
        List<EventBase> events = new List<EventBase>();

        public Track(int chunkType, int length, byte[] buffer) : base(chunkType, length, buffer)
        {
            Parsing(buffer);
        }

        
        private void Parsing(byte[] buffer)
        {
            int offset = 0;
            EventBase eventBase = null;

            while(offset < buffer.Length)
            {
                eventBase = EventBase.Parsing(buffer, ref offset, eventBase);

                if(eventBase == null)
                {
                    break;
                }
                events.Add(eventBase);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return events.GetEnumerator();
        }
    }
}