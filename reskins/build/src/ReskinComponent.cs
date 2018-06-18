using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame
{
    class ReskinComponent
    {
        protected Reskin skin;

        public ReskinComponent(Reskin reskin)
        {
            skin = reskin;
        }

        public virtual void OnSpawn(Duck duck) { }

        public virtual void Update(Duck duck) { }

        public virtual void OnApply(Duck duck) { }

        public virtual void OnQuack(Duck duck) { }

        public virtual void OnSkinReset(Duck duck) { }

        public virtual void OnLoad() { }

        public virtual void OnLevelChange() { }
    }
}
