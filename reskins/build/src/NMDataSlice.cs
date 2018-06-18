using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame
{
    [FixedNetworkID(0xff70)]
    class NMDataSlice : NMDuckNetwork
    {
        public bool last;
        public BitBuffer data;
        public short transferSession;
        public byte index;

        public NMDataSlice() { }

        public NMDataSlice(BitBuffer _data,short _session,bool _last,byte _index)
        {
            data = _data;
            transferSession = _session;
            last = _last;
            index = _index;
        }

        protected override void OnSerialize()
        {
            base.OnSerialize();
            serializedData.Write(data);
        }

        public override void OnDeserialize(BitBuffer msg)
        {
            base.OnDeserialize(msg);
            DataTransferManager.OnDataRecieved(connection,index, msg.ReadBitBuffer(),transferSession,last);
        }

    }
}
