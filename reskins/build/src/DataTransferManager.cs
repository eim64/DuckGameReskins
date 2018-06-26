using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace DuckGame
{
    class DataTransferArgs : EventArgs{
        public byte[] data;
        public string dataType;
        public Profile profile;
    }

    class DataTransferManager
    {
        public static event EventHandler<DataTransferArgs> onMessageCompleted;

        public static Dictionary<byte, string> dataTypes = new Dictionary<byte, string>()
        {
            { 0x01,"undefined" },
            { 0xF1 ,"reskin" }
        };

        const int SliceSize = 1024;
        static short currentSession = 0;

        static Dictionary<NetworkConnection, DataTransferSession> ActiveSessions = new Dictionary<NetworkConnection, DataTransferSession>();

        public static void SendLotsOfData(byte[] bytes,string type,NetworkConnection connection)
        {
            currentSession++;
            byte[] data = new byte[bytes.Length+1];
            if (!dataTypes.ContainsValue(type))
                type = "undefined";

            data[0] = dataTypes.FirstOrDefault(x=>x.Value==type).Key;
            bytes.CopyTo(data,1);

            var compressedData = compress(data);

            byte index = 0;
            int position = 0;
            BitBuffer bit = new BitBuffer();
            for(;;)
            {
                bit.Write(compressedData[position]);
                position++;

                bool flag = position == compressedData.Length;
                if (position % SliceSize == 0 || flag)
                {
                    Send.Message(new NMDataSlice(bit, currentSession, flag,index++),connection);
                    if (flag) break;

                    bit = new BitBuffer();
                }
            }
        }

        public static void OnDataRecieved(NetworkConnection connection,byte index,BitBuffer data,short session,bool last)
        {
            if (!ActiveSessions.ContainsKey(connection))
            {
                ActiveSessions.Add(connection, new DataTransferSession(session,index,data,last));

                DevConsole.Log("Starting new TransferSession"+session,Color.Green);

                return;
            }

            var TransferSession = ActiveSessions[connection];

            if (TransferSession.id != session)
                TransferSession.Reset(session);

            TransferSession.RecieveData(index,data,last);

            if (!TransferSession.finished) return;


            DevConsole.Log("recieved last dataslice",Color.Green);

            ActiveSessions.Remove(connection);

            byte[] bytes = decompress(TransferSession.GetData());
            onMessageCompleted(connection, new DataTransferArgs() { data = bytes.Skip(1).ToArray(), dataType = dataTypes[bytes[0]], profile = connection.profile });
        }

        public static byte[] compress(byte[] bytes)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionMode.Compress))
                dstream.Write(bytes, 0, bytes.Length);
            return output.ToArray();
        }

        public static byte[] decompress(byte[] bytes)
        {
            MemoryStream input = new MemoryStream(bytes);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
                dstream.CopyTo(output);
            return output.ToArray();
        }
         
    }
}
