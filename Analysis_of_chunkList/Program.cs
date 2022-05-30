using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analysis_of_chunkList
{
    internal class Program
    {
        static string fname = "example.mid";

        static void Main(string[] args)
        {
            FileStream fs = new FileStream(fname, FileMode.Open);

            while(fs.Position < fs.Length)
            {
                Chunk chunk = Chunk.Parse(fs);
                if(chunk != null)
                {
                    if(chunk != null)
                    {
                        Console.WriteLine("{0}:{1}bytes", chunk.CTString, chunk.Length);
                    }
                }
            }
        }
    }
}
