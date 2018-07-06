using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame
{
    class DataTransferSession
    {
        public short id;
        public byte max = byte.MaxValue;
        public Dictionary<byte, byte[]> recievingData = new Dictionary<byte, byte[]>();

        public DataTransferSession(short session,byte index,BitBuffer data,bool last)
        {
            id = session;
            RecieveData(index,data,last);
        }

        public void Reset(short session)
        {
            id = session;
            max = byte.MaxValue;
            recievingData.Clear();
        }

        public bool finished
        {
            get { return recievingData.Count == max+1; }
        }


        public void RecieveData(byte index,BitBuffer data,bool last)
        {
            if (last) max = index;
            recievingData.Add(index,data.GetBytes());
        }

        public byte[] GetData()
        {
            return ReskinMaker.byteStuff.combine((from pair in recievingData orderby pair.Key ascending select pair.Value).ToArray());
        }
    }
}
