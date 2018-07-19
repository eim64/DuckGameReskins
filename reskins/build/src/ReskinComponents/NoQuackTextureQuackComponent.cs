using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame
{
    class NoQuackTextureQuackComponent : ReskinComponent
    {

        public Dictionary<Duck, float> ApplicableDucks = new Dictionary<Duck, float>();
        bool hasQuackTexture;

        public NoQuackTextureQuackComponent(Reskin skin) : base(skin) { }

        public override void OnLoad()
        {
            hasQuackTexture = skin.Textures.ContainsKey("Quack Texture");
        }

        public override void Update(Duck duck)
        {
            if (hasQuackTexture) return;

            float scale;
            if (!ApplicableDucks.TryGetValue(duck, out scale)) {
                ApplicableDucks.Add(duck,1);
                return;
            }

            duck.scale = new Vec2(scale);
            if (duck.ragdoll != null)
                duck.ragdoll.part1.scale = duck.scale;

            scale/=1.1f;
            if (scale < 1) scale = 1;
            ApplicableDucks[duck] = scale;
        }

        public override void OnQuack(Duck duck)
        {
            if (hasQuackTexture) return;

            ApplicableDucks[duck] = 1.2f;
        }
    }
}
