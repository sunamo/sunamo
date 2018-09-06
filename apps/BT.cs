using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace apps
{
    public class BT //: sunamo.BT
    {
        public static IBuffer ConvertFromStringToBuffer(String str)
        {
            using (InMemoryRandomAccessStream memoryStream = new InMemoryRandomAccessStream())
            {
                using (DataWriter dataWriter = new DataWriter(memoryStream))
                {
                    dataWriter.WriteString(str);
                    return dataWriter.DetachBuffer();
                }
            }
        }

        public static IBuffer ConvertFromBytesToBuffer(byte[] p)
        {
            using (InMemoryRandomAccessStream memoryStream = new InMemoryRandomAccessStream())
            {
                using (DataWriter dataWriter = new DataWriter(memoryStream))
                {
                    dataWriter.WriteBytes(p);
                    return dataWriter.DetachBuffer();
                }
            }
        }

        public static string ConvertFromBufferToString(IBuffer ib)
        {
            DataReader reader = DataReader.FromBuffer(ib);
            byte[] fileContent = new byte[reader.UnconsumedBufferLength];
            reader.ReadBytes(fileContent);
            string text = Encoding.UTF8.GetString(fileContent, 0, fileContent.Length);
            return text;
        }

        public static byte[] ConvertFromBufferToByteArray(IBuffer ib)
        {
            DataReader reader = DataReader.FromBuffer(ib);
            byte[] fileContent = new byte[reader.UnconsumedBufferLength];
            reader.ReadBytes(fileContent);
            return fileContent;
        }

        

        public async static Task< InMemoryRandomAccessStream> ConvertFromBufferToInMemoryRandomAccessStream(IBuffer ib)
        {
            
            var vr = new InMemoryRandomAccessStream();
            await vr.WriteAsync(ib);
            return vr;

        }

        public static IBuffer ConvertFromByteArrayToBuffer(byte[] v)
        {
            return v.AsBuffer();
        }
    }
}
