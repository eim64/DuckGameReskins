using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DuckGame
{
    [FixedNetworkID(0xff69)]
    class NMRequestReskin : NMDuckNetworkEvent
    {
        public byte profile;
        public string md5;

        public NMRequestReskin()
        {

        }

        public NMRequestReskin(byte Profile,string uid)
        {
            profile = Profile;
            md5 = uid;
            DevConsole.Log("sent a request for skin: "+md5,Color.Green);
        }

        protected override void OnSerialize()
        {
            base.OnSerialize();
        }

        public override void Activate()
        {
            Profile pro = DuckNetwork.profiles[profile];

            if (!Reskin.Exists(md5))
            {
                DevConsole.Log("recieved request for non existing md5",Color.Red);
                return;
            }

            DataTransferManager.SendLotsOfData(Reskin.hatData[md5],"reskin",connection);
        }
    }
}
