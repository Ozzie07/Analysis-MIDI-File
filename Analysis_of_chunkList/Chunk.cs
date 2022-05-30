using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analysis_of_chunkList
{
    public class Chunk
    {
        public int ChunkType { get; }

        public int Length { get; }

        public byte[] Data { get; }

        public string CTString
        {
            get { return StaticFunctions.GetString(ChunkType); }
        }

        public Chunk(int chunkType, int length, byte[] buffer)
        {
            ChunkType = chunkType;
            Length = length;
            Data = buffer;
        }

        public static Chunk Parse(Stream stream)
        {
            try
            {
                BinaryReader br = new BinaryReader(stream);
                int chunkType = br.ReadInt32();
                int length = br.ReadInt32();
                length = StaticFunctions.ConvertHostOrder(length);
                byte[] buffer = br.ReadBytes(length);
                return new Chunk(chunkType, length, buffer);
            }
            catch
            {
                return null;
            }
        }
    }
}
