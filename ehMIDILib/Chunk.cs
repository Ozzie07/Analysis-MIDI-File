using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ehMIDILib
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

        public byte[] Buffer //원본 데이터가 있는 부분.
        {
            get
            {
                byte[] ct_buf = BitConverter.GetBytes(ChunkType);
                int belen = StaticFunctions.ConvertHostOrder(Length);
                byte[] len_buf = BitConverter.GetBytes(belen);
                byte[] buffer = new byte[ct_buf.Length + len_buf.Length + Data.Length];
                Array.Copy(ct_buf, buffer, ct_buf.Length);
                Array.Copy(len_buf, 0, buffer, ct_buf.Length, len_buf.Length);
                Array.Copy(Data, 0, buffer, ct_buf.Length + len_buf.Length, Data.Length);

                return buffer;
            }
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

                switch(StaticFunctions.ConvertHostOrder(chunkType))
                {
                    case 0x4d546864: return new Header(chunkType, length, buffer);
                    case 0x4d54726b: return new Track(chunkType, length, buffer);
                }

                return new Chunk(chunkType, length, buffer);
            }
            catch
            {
                return null;
            }
        }
    }
}
