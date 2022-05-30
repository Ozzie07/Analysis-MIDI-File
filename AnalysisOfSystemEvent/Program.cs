using ehMIDILib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisOfSystemEvent
{
    class Program
    {
        static string fname = "Beethoven Symphony No.7_StudioOne.mid";

        static void Main(string[] args)
        {
            FileStream fs = new FileStream(fname, FileMode.Open);

            while (fs.Position < fs.Length)
            {
                Chunk chunk = Chunk.Parse(fs);
                if (chunk != null)
                {
                    if (chunk != null)
                    {
                        Console.WriteLine("{0}:{1}bytes", chunk.CTString, chunk.Length);
                    }
                }

                if (chunk is Header)
                {
                    ViewHeader(chunk as Header);
                }

                if (chunk is Track)
                {
                    ViewTrack(chunk as Track);
                }

            }
        }

        private static void ViewTrack(Track track)
        {
            Console.WriteLine("---- Track Chunk ----");
            int ecnt = 0;
            foreach (EventBase eventItem in track)
            {
                ecnt++;
                Console.WriteLine(StaticFunctions.HexaString(eventItem.Buffer));
                Console.WriteLine("{0}th delta : {1}", ecnt, eventItem.Delta);

                if (eventItem is MetaEvent)
                {
                    Console.Write("<Meta> ");
                    ViewMeta(eventItem as MetaEvent);
                }

                if (eventItem is MIDIEvent)
                {
                    Console.WriteLine("<MIDI> ");
                    ViewMIDI(eventItem as MIDIEvent);
                }

                if (eventItem is SysEvent)
                {
                    Console.WriteLine("<System> ");
                    ViewSys(eventItem as SysEvent);
                }
            }
        }

        private static void ViewSys(SysEvent sysEvent)
        {
            Console.WriteLine("Normally it does not exist in MIDI file");
        }

        private static void ViewMIDI(MIDIEvent mIDIEvent)
        {
            Console.WriteLine(mIDIEvent.Status);
            Console.WriteLine(mIDIEvent.Description);
        }

        private static void ViewMeta(MetaEvent metaEvent)
        {
            Console.WriteLine("msg : {0} length : {1}", metaEvent.Msg, metaEvent.Length);
            Console.WriteLine(metaEvent.MetaDescription);
        }

        private static void ViewHeader(Header header)
        {
            Console.WriteLine("--- Header Chunk ----");
            Console.WriteLine(StaticFunctions.HexaString(header.Buffer));
            Console.WriteLine("Format : {0}", header.Format);
            Console.WriteLine("Tracks : {0}", header.TrackCount);
            Console.WriteLine("Division : {0}", header.Division);
            Console.WriteLine();
        }
    }
}
