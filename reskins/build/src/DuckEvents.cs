using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame
{

    class DuckEvents
    {
        public static event EventHandler<EventArgs> OnDuckSpawn;

        public static event EventHandler<EventArgs> OnDuckQuack;

        static Dictionary<Profile, DuckInfo> Infos = new Dictionary<Profile, DuckInfo>();

        public static void Update()
        {
            foreach (var profile in reskinMod.ActiveReskins.Keys)
            {
                if (!Infos.ContainsKey(profile))
                {
                    Infos.Add(profile, new DuckInfo(profile.duck));
                    DevConsole.Log("added profile!", Color.Green);
                    return;
                }

                var info = Infos[profile];
                info.UpdateInfo(profile.duck);

                if (info.exists && !info.pexists) OnDuckSpawn(profile, new EventArgs());
                if (info.quack && !info.pquack) OnDuckQuack(profile, new EventArgs());
            }
        }

        protected class DuckInfo
        {
            public bool exists;
            public bool quack;

            public bool pexists;
            public bool pquack;

            public DuckInfo(Duck duck)
            {
                UpdateInfo(duck);
            }

            public void UpdateInfo(Duck duck)
            {
                if (duck == null) return;

                pexists = exists;
                pquack = quack;

                exists = duck.localSpawnVisible;
                quack = duck.IsQuacking();
            }
        }
    }
}
