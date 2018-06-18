using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame
{
    class ScaledSpriteMap : SpriteMap
    {
        public Vec2 ActualScale;

        public ScaledSpriteMap(Tex2D texture, SpriteMap original,Vec2 OldSize,int newWidth,int newHeight) : base(original.texture, newWidth, newHeight)
        {
            ActualScale = new Vec2(OldSize.x/(float)newWidth,(float)OldSize.y/(float)newHeight);
            
            _texture = texture;
            scale = original.scale;
            center = original.center;
            depth = original.depth;
            alpha = original.alpha;
            angle = original.angle;
            flipV = original.flipV;
            flipH = original.flipH;
            original.CloneAnimations(this);
        }

        protected ScaledSpriteMap(ScaledSpriteMap map) : base(map.texture,map.width,map.height)
        {
            ActualScale = map.ActualScale;

            scale = map.scale;
            position = map.position;
            center = map.center;
            depth = map.depth;
            alpha = map.alpha;
            angle = map.angle;
            flipH = map.flipH;
            flipV = map.flipV;
            map.CloneAnimations(this);
        }

        public override void Draw()
        {
            Vec2 ogScale = scale;
            scale = scale * ActualScale;

            base.Draw();

            scale = ogScale;
        }

        public override void Draw(Rectangle r)
        {
            Vec2 ogScale = scale;
            scale = scale * ActualScale;

            base.Draw(r);
            scale = ogScale;
        }

        public override Sprite Clone()
        {
            return new ScaledSpriteMap(this);
        }
    }
}
