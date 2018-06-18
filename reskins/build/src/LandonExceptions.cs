using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DuckGame
{
    class LandonExceptions
    {
        public static void FixRagdoll(Duck duck,Vec2 duckSpriteSize)
        {
            if (duck?.ragdoll == null) return;

            Vec2 multiplier = duckSpriteSize / new Vec2(32);
            FixRagdollPartCenter(duck.ragdoll.part1, multiplier);
            FixRagdollPartCenter(duck.ragdoll.part2, multiplier);
            FixRagdollPartCenter(duck.ragdoll.part3, multiplier);
        }

        static void FixRagdollPartCenter(RagdollPart part, Vec2 multiplier)
        {
            Vec2 val;
            switch (part.part)
            {
                case 0:
                case 1:
                    val = new Vec2(16, 13);
                    break;

                case 3: val = new Vec2(6, 8); break;
                default: val = new Vec2(8); break;
            }

            part.center = val * multiplier;
        }
    }
}
