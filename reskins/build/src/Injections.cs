using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace DuckGame
{
    class Injections
    {
        public static void InjectMethods()
        {
            SwapMethods(
                 typeof(Graphics).GetMethod("DrawWithoutUpdate", new Type[] { typeof(SpriteMap), typeof(float), typeof(float), typeof(float), typeof(float), typeof(bool) }),
                 typeof(Injections).GetMethod("DrawWithoutUpdate", new Type[] { typeof(SpriteMap), typeof(float), typeof(float), typeof(float), typeof(float), typeof(bool) }));
        }

        public static void DrawWithoutUpdate(SpriteMap g, float x, float y, float scaleX = 1f, float scaleY = 1f, bool maintainFrame = false)
        {
            g.x = x;
            g.y = y;
            g.xscale = scaleX;
            g.yscale = scaleY;

            g.Draw();
        }

        static unsafe void SwapMethods(MethodInfo methodToReplace,MethodInfo methodToInject)
        {
            RuntimeHelpers.PrepareMethod(methodToReplace.MethodHandle);
            RuntimeHelpers.PrepareMethod(methodToInject.MethodHandle);
            unsafe
            {
                if (IntPtr.Size == 4)
                {
                    int* inj = (int*)methodToInject.MethodHandle.Value.ToPointer() + 2;
                    int* tar = (int*)methodToReplace.MethodHandle.Value.ToPointer() + 2;

                    *tar = *inj;
                }
            }
        }

    }
}
