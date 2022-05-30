using ehMIDILib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisOfHeaderChunk
{
    class Program
    {
        static string fname = "example.mid";

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

                if(chunk is Header)
                {
                    ViewHeader(chunk as Header);
                }
            }
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
