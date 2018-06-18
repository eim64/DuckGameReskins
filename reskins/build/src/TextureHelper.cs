using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using ReskinMaker;
using System.Security.Cryptography;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;

namespace DuckGame
{
    class TextureHelper
    {

        public static Bitmap getBitmap(Tex2D texture)
        {
            Bitmap ret;

            using (MemoryStream stream = new MemoryStream())
            {
                (texture.nativeObject as Texture2D).SaveAsPng(stream,texture.width,texture.height);
                ret = (Bitmap)Image.FromStream(stream);
            }

            return ret;
        }

        private static ConstructorInfo _Tex2D = typeof(Tex2D).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(Texture2D), typeof(string), typeof(short) }, null);
        public static Tex2D getTex2D(Texture2D tex)
        {
            var tex2d = (Tex2D)_Tex2D.Invoke(new object[] { tex, tex.Name, (short)0 });
            Content.AssignTextureIndex(tex2d);

            return tex2d;
        }


        public static bool SameImage(Tex2D A,Tex2D B)
        {
            if (A.width != B.width || A.height != B.height) return false;

            byte[] dA = new byte[A.width * A.height * 4];
            byte[] dB = new byte[B.width * B.height * 4];
            A.GetData(dA);
            B.GetData(dB);


            for (int i = 0; i < dB.Length; i++)
                if (dB[i] != dA[i]) return false;

            return true;
        }

        
    }
}
