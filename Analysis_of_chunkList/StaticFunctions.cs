using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Analysis_of_chunkList
{
    public static class StaticFunctions
    {
        public static string GetString(int magic)
        {
            //어떤 파일이 무슨 파일 형식임을 알려주기 위한 4 byte 크기의 부분.
            byte[] data= BitConverter.GetBytes(magic);
            ASCIIEncoding en = new ASCIIEncoding();
            return en.GetString(data);
        }

        public static int ConvertHostOrder(int data)
        {
            return IPAddress.NetworkToHostOrder(data);
        }
    }
}
