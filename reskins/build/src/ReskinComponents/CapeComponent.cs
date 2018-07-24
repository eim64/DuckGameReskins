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
            skin.Textures.TryGetValue("Cape Texture", out CapeTexture); //Try to load the Cape Texture
        }

        //Add the Cape on in-game spawn. 
        public override void OnSpawn(Duck duck)
        {
            AddCape(duck);
        }

        //Add the cape when the hat is selected. 
        public override void OnApply(Duck duck)
        {
            AddCape(duck);
        }

        void AddCape(Duck duck)
        {
            if (CapeTexture == null) return;    //The skin had no cape texture, return.

            Cape cape = new Cape(0,0,duck);
            cape.SetCapeTexture(CapeTexture);
            Level.Add(cape);                    //Add Cape to the level. 

            if (Capes.ContainsKey(duck))    //If theres already a cape in the level
            {
                Level.Remove(Capes[duck]);  //Remove the already existing cape from the level
                Capes[duck] = cape;         //Change the cape.

                return;
            }

            Capes.Add(duck, cape);          //If no existing cape was found, add it to the dictionary. 
        }

        //Remove cape when changing/removing a skin. 
        public override void OnSkinReset(Duck duck)
        {
            Cape cape;
            if (!Capes.TryGetValue(duck, out cape)) return;

            Level.Remove(cape);
            Capes.Remove(duck);
        }
    }
}
