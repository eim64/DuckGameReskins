using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DuckGame
{
    public class ItemRetextureComponent : ReskinComponent
    {
        Dictionary<Duck, List<Holdable>> Holdables = new Dictionary<Duck, List<Holdable>>();
        Dictionary<string, Tex2D> Equipment = new Dictionary<string, Tex2D>();

        public ItemRetextureComponent(Reskin reskin) : base(reskin)
        {

        }

        public override void OnLoad()
        {
            const string prefix = @"Equipment Retextures\";
            Equipment = skin.Textures.Where(x=>x.Key.StartsWith(prefix)).ToDictionary(x => x.Key.Remove(0,prefix.Length),x=>x.Value);
        }

        public override void Update(Duck duck)
        {
            if (Equipment.Count == 0) return;
            if (!Holdables.ContainsKey(duck)) Holdables.Add(duck, new List<Holdable>());

            var pEquips = Holdables[duck];
            var equipments = duck._equipment.Cast<Holdable>().ToList();
            if (duck.holdObject != null && !(duck.holdObject is Equipment)) equipments.Add(duck.holdObject);

            foreach (var equipment in equipments.Where(x => !pEquips.Contains(x)))
            {
                Tex2D tex2d;

                foreach (var sprite in getSprites(equipment))
                    if (sprite?.texture?.textureName != null && Equipment.TryGetValue(sprite.texture.textureName, out tex2d) && sprite.texture.width <= tex2d.width && sprite.texture.height <= tex2d.height) sprite.texture = tex2d;
                 pEquips.Add(equipment);
            }
        }

        public override void OnLevelChange()
        {
            Holdables.Clear();
        }


        static IEnumerable<Sprite> getSprites(Holdable e)
        {
            return e?.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(FI => FI.FieldType.IsAssignableFrom(typeof(Sprite))).Select(FI => (Sprite)FI.GetValue(e)); 
        }
    }
}
