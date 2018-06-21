using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame
{
    public class CapeComponent : ReskinComponent
    {
        Tex2D CapeTexture;
        public Dictionary<Duck, Cape> Capes = new Dictionary<Duck, Cape>();

        public CapeComponent(Reskin reskin) : base(reskin)
        {

        }

        public override void OnLoad()
        {
            skin.Textures.TryGetValue("Cape Texture", out CapeTexture);
        }

        public override void OnSpawn(Duck duck)
        {
            AddCape(duck);
        }

        public override void OnApply(Duck duck)
        {
            AddCape(duck);
        }

        void AddCape(Duck duck)
        {
            if (CapeTexture == null || Capes.ContainsKey(duck)) return;

            Cape cape = new Cape(0,0,duck);
            cape.SetCapeTexture(CapeTexture);
            Level.Add(cape);

            Capes.Add(duck,cape);
        }

        public override void OnSkinReset(Duck duck)
        {
            Cape cape;
            if (!Capes.TryGetValue(duck, out cape)) return;

            Level.Remove(cape);
            Capes.Remove(duck);
        }
    }
}
