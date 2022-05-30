namespace ehMIDILib
{
    public class Header : Chunk
    {
        public int Format
        {
            get
            {
                return StaticFunctions.ConvertHostOrderS(Data, 0);
            }
        }

        public int TrackCount
        {
            get
            {
                return StaticFunctions.ConvertHostOrderS(Data, 2);
            }
        }

        public int Division
        {
            get
            {
                return StaticFunctions.ConvertHostOrderS(Data, 4);
            }
        }

        public Header(int chunkType, int length, byte[] buffer) : base(chunkType, length, buffer)
        {

        }
    }
}