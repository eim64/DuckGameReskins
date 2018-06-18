using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using ReskinMaker;
using System.Security.Cryptography;

// The title of your mod, as displayed in menus
[assembly: AssemblyTitle("reskins")]

// The author of the mod
[assembly: AssemblyCompany("Killer-Fackur || EIM64")]

// The description of the mod
[assembly: AssemblyDescription("Client mod that stuff")]

// The mod's version
[assembly: AssemblyVersion("69.420")]

namespace DuckGame
{
    public class reskinMod : DisabledMod,IUpdateable
    {
        public const string ReskinPath = @".\Reskins\";

        public static void UpdateNetmessageTypes()
        {
            var subclasses = Editor.GetSubclasses(typeof(NetMessage));
            Network.typeToMessageID.Clear();
            ushort key = 1;
            foreach (Type type in subclasses)
            {
                if (type.GetCustomAttributes(typeof(FixedNetworkID), false).Length != 0)
                {
                    FixedNetworkID customAttribute = (FixedNetworkID)type.GetCustomAttributes(typeof(FixedNetworkID), false)[0];
                    if (customAttribute != null)
                        Network.typeToMessageID.Add(type, customAttribute.FixedID);
                }
            }
            foreach (Type type in subclasses)
            {
                if (!Network.typeToMessageID.ContainsValue(type))
                {
                    while (Network.typeToMessageID.ContainsKey(key))
                        ++key;
                    Network.typeToMessageID.Add(type, key);
                    ++key;
                }
            }
        }

        // The mod's priority; this property controls the load order of the mod.
        public override Priority priority
		{
			get { return base.priority; }
		}
        
        public static ModConfiguration config;
        static PropertyInfo steamIdField = typeof(ModConfiguration).GetProperty("workshopID",BindingFlags.Instance|BindingFlags.NonPublic);
        static PropertyInfo disabledField = typeof(ModConfiguration).GetProperty("disabled", BindingFlags.Instance|BindingFlags.NonPublic);
        public static string replaceData
        {
            get
            {
                return config.isWorkshop ? steamIdField.GetValue(config, new object[0]).ToString() : "LOCAL";
            }
        }

        public static bool disabled
        {
            set { disabledField.SetValue(config,value,new object[0]); }
            get { return (bool)disabledField.GetValue(config, new object[0]); }
        }

        #region interface

        public bool Enabled
        {
            get
            {
                return true ;
            }
        }

        public int UpdateOrder
        {
            get
            {
                return 0;
            }
        }


        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        #endregion

        protected static MethodInfo _swapmethod;

        // This function is run before all mods are finished loading.
        protected override void OnPreInitialize()
		{
            DataTransferManager.onMessageCompleted += recieveData;



            config = configuration;
            Injections.InjectMethods();
            
            base.OnPreInitialize();
        }

        const BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance;


        // This function is run after all mods are loaded.
        protected override void OnPostInitialize()
		{
            var form = (Form)Control.FromHandle(MonoMain.instance.Window.Handle);
            form.FormClosing += FormClosed;
            (typeof(Game).GetField("updateableComponents", flags).GetValue(MonoMain.instance) as List<IUpdateable>).Add(this);

            UpdateNetmessageTypes();
            Reskin.InitializeReskins();
        }

        void FormClosed(object sender,EventArgs e)
        {
            if (!Program.commandLine.Contains("-download")) return;
                disabled = false;
            typeof(ModLoader).GetMethod("DisabledModsChanged",flags).Invoke(null,new object[0]);
        }

        bool updateLobby;
        static Level prevLevel;

        internal static Dictionary<Profile, Reskin> ActiveReskins = new Dictionary<Profile, Reskin>();


        void doLobbyStuff()
        {
            if (!ModLoader.modsEnabled || !(Level.current is TeamSelect2) || Steam.lobby == null || Steam.lobby.id == 0L)
            {
                updateLobby = true;
                return;
            }
            string str;
            if (Level.current is TeamSelect2 && updateLobby && !string.IsNullOrEmpty((str = Steam.lobby.GetLobbyData("mods"))))
            {
                int index = str.IndexOf(reskinMod.replaceData);
                if (index < 0)
                {
                    updateLobby = false;
                    return;
                };

                str = str.Remove(index, reskinMod.replaceData.Length)
                    .Trim('|').Replace("||", "|");
                Steam.lobby.SetLobbyData("mods", str);
                updateLobby = false;
            }
        }

        static FieldInfo _fullscreen = typeof(MonoMain).GetField("_fullScreen", BindingFlags.Instance | BindingFlags.NonPublic);
        bool fullscreen
        {
            get { return (bool)_fullscreen.GetValue(MonoMain.instance); }
            set { _fullscreen.SetValue(MonoMain.instance, value); }
        }

        bool pFullscreen;

        

        public void Update(GameTime gt)
        {
            doLobbyStuff();

            if (Level.current?.initialized != true) return;

            if (Level.current != prevLevel)
                LevelChange();



            prevLevel = Level.current;

            if (profiles.Count == pHats.Count)
                for (int i = 0; i < profiles.Count; i++)
                    if (pHats[i] != profiles[i]?.team?.hat) HatChange(profiles[i]);

            pHats = profiles.Select(x => x?.team?.hat).ToList();


            DuckEvents.Update();
            foreach (var kvp in ActiveReskins)
            {
                var duck = kvp.Key.duck;
                if (duck == null) continue;

                var skin = kvp.Value;

                skin.Update(duck);

                var persona = kvp.Key.persona;

                var hat = duck.hat as TeamHat;
                if (hat != null && hat.team == duck.team) removeHat(duck);

                if (persona.sprite.texture.textureName != "RESKIN") skin.Apply(duck);
            }

            pFullscreen = fullscreen;
        }


        void resetAll()
        {
            foreach (var kvp in ActiveReskins)
                resetReskin(kvp.Key);
            ActiveReskins.Clear();
        }

        void resetReskin(Profile pro)
        {
            Reskin skin;
            if (!ActiveReskins.TryGetValue(pro, out skin)) return;

            foreach (var component in skin.Components)
                component.OnSkinReset(pro.duck);

            pro.persona = new DuckPersona(pro.persona.color);
            if (pro.duck != null)
                pro.duck.InitProfile();

            ActiveReskins.Remove(pro);
        }

        void requestReskin(Profile pro, string md5)
        {
            Send.Message(new NMRequestReskin(pro.networkIndex, md5), pro.connection);
        }

        void HatChange(Profile pro, Tex2D customHat = null)
        {
            Tex2D hat = customHat ?? pro?.team?.hat?.texture;
            resetReskin(pro);

            if (pro?.duck == null || hat == null || hat.height <= ReskinFile.HatHeight || hat.width != 64) return;


            ReskinFile file = Reskin.tryLoadReskin(hat);


            if (file == null)
            {
                DevConsole.Log("Error loading reskin", Color.Red);
                return;
            }
            file.Hat.Dispose();


            string md5 = (file.getChunk("MD5") as TextChunk)?.Text;
            string dir;

            if (Network.isActive && !pro.localPlayer && md5 != null)
            {
                if (Reskin.Exists(md5)) dir = reskinMod.ReskinPath + md5;
                else {
                    requestReskin(pro, md5);
                    return;
                }
            }
            else dir = Reskin.CreateReskinFiles(file);

            Reskin r = Reskin.GetReskin(dir);
            if (!Reskin.IsValid(r)) { DevConsole.Log("something is wrong with that reskin!", Color.Red); return; }



            ActiveReskins.Add(pro, r);
            r.Apply(pro.duck);
        }

        void recieveData(object sender, DataTransferArgs args)
        {
            if (args.dataType != "reskin") return;
            DevConsole.Log("swapping hat texture!", Color.Green);

            args.profile.team.hat.texture = Team.Deserialize(args.data).hat.texture;
            HatChange(args.profile);
        }

        void removeHat(Duck duck)
        {
            var hat = duck.GetEquipment(typeof(TeamHat)) as TeamHat;
            if (hat == null) return;


            duck.Unequip(hat);
            Level.Remove(hat);
        }

        List<Profile> profiles
        {
            get { return Profiles.active; }
        }

        List<SpriteMap> pHats = new List<SpriteMap>();

        public void LevelChange()
        {
            foreach (var reskin in ActiveReskins.Values)
                foreach (var component in reskin.Components)
                    component.OnLevelChange();

            if (Level.current is TitleScreen || Level.current is ArcadeLevel)
                resetAll();
        }
    }
}
