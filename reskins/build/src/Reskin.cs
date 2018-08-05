using Microsoft.Xna.Framework.Graphics;
using ReskinMaker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace DuckGame
{
    public class Reskin
    {
        static FieldInfo _textureName = typeof(Tex2DBase).GetField("_textureName",BindingFlags.NonPublic|BindingFlags.Instance);
        public static Tex2D CorrectTexture(Tex2D tex, bool recolor = false, Vec3 color = default(Vec3))
        {
            if (recolor)
                return Graphics.Recolor(tex, color);
            RenderTarget2D t = new RenderTarget2D(tex.width, tex.height);
            Graphics.SetRenderTarget(t);
            Graphics.Clear(new Color(0, 0, 0, 0));
            Graphics.screen.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            Graphics.Draw(tex, new Vec2(), new Rectangle?(), Color.White, 0.0f, new Vec2(), new Vec2(1f), SpriteEffects.None, (Depth)0.5f);
            Graphics.screen.End();
            Graphics.device.SetRenderTarget(null);
            _textureName.SetValue(t,"RESKIN");
            
            return t;
        }

        internal static ScaledSpriteMap GetFixedSpriteMap(Tex2D texture,SpriteMap replace,Vec2 oldSize,Vec2 newSize, bool recolor = false, Vec3 color = default(Vec3))
        {
            ScaledSpriteMap sprite = new ScaledSpriteMap(CorrectTexture(texture, recolor, color), replace,oldSize, (int)newSize.x, (int)newSize.y);
            sprite.CenterOrigin();
            
            return sprite;
        }

        public static bool IsValid(Reskin skin)
        {
            return skin.Textures.ContainsKey("Duck Texture");
        }

        public static Dictionary<string, Reskin> allReskins = new Dictionary<string, Reskin>();
        public static Dictionary<string, byte[]> hatData = new Dictionary<string, byte[]>();
        const string SettingsFileName = "Settings";

        public static List<Type> ComponentTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(ReskinComponent))).ToList();
        protected List<ReskinComponent> CreateComponents()
        {
            return ComponentTypes.Select(type=>Activator.CreateInstance(type,new object[] { this }) as ReskinComponent).ToList();
        }

        public List<ReskinComponent> Components;

        public Dictionary<string, Tex2D> Textures = new Dictionary<string, Tex2D>();

        public Dictionary<string, string> Settings = new Dictionary<string, string>();
        public Vec2 duckSpriteSize;
        public Vec2 ArmSpriteSize;
        public Vec2 FeatherSpriteSize;

        public Vec2 defDuckSize = new Vec2(32);
        public Vec2 defArmSize = new Vec2(16);
        public Vec2 defFeatherSize = new Vec2(12,4);


        public string directory;

        public static Reskin GetReskin(string dir)
        {
            if (allReskins.ContainsKey(dir)) return allReskins[dir];
            
            return new Reskin(dir);
        }

        public Reskin(string path)
        {
            directory = path;

            if (allReskins.ContainsKey(directory)) return;

            Components = CreateComponents();

            var settingsPath = Path.Combine(directory, "Settings");
            if (File.Exists(settingsPath))
                LoadSettings(File.ReadAllText(settingsPath));

            LoadTextures(path,path);

            duckSpriteSize = GetSettingVec2("DuckSpriteSize", new Vec2(32));
            ArmSpriteSize = GetSettingVec2("ArmSpriteSize", new Vec2(16));
            FeatherSpriteSize = GetSettingVec2("FeatherSpriteSize", new Vec2(12, 4));


            allReskins.Add(directory, this);

            foreach (var component in Components)
                component.OnLoad();
        }


        void LoadTextures(string folder,string prefix)
        {
            var rootFolder = folder.Remove(0,prefix.Length);

            DirectoryInfo dir = new DirectoryInfo(folder);
            foreach (FileInfo file in dir.GetFiles("*.png"))
                Textures.Add(rootFolder.TrimStart('\\') + file.Name.Substring(0, file.Name.Length - 4),TextureHelper.getTex2D(ContentPack.LoadTexture2D(file.FullName)));

            foreach (var d in dir.GetDirectories())
                LoadTextures(folder+'\\'+d.Name+'\\',prefix);
        }

        #region settings stuff

        const char KVseparator = ';';
        const char ItemSeparator = '\n';

        public void LoadSettings(string settings)
        {
            Settings = SettingsChunk.GetDict(settings);
        }

        public string GetSetting(string key)
        {
            string output;
            if (Settings.TryGetValue(key, out output)) return output;

            return null;
        }

        public Vec2 GetSettingVec2(string key,Vec2 defaultValue,char separator = 'x')
        {
            var split = GetSetting(key)?.Split(separator);
            if (split == null || split.Length < 2) return defaultValue;

            float a, b;
            if (float.TryParse(split[0], out a) && float.TryParse(split[1], out b)) return new Vec2(a,b);

            return defaultValue;
        }

        public int GetSettingInt(string key,int defaultValue)
        {
            int output;
            if (Int32.TryParse(GetSetting(key), out output)) return output;

            return defaultValue;
        }

        public bool GetSettingBool(string key, bool defaultValue)
        {
            bool output;
            if (bool.TryParse(GetSetting(key),out output)) return output;

            return defaultValue;
        }
        #endregion

        public void Update(Duck duck)
        {
            LandonExceptions.FixRagdoll(duck,duckSpriteSize);

            foreach (var component in Components)
                component.Update(duck);
        }

        /// <summary>
        /// Gets all textures in folder.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public Dictionary<string,Tex2D> GetTextures(string folder)
        {
            if (folder == null) throw new ArgumentNullException();

            folder = folder.TrimEnd('\\')+'\\';

            return Textures.Where(kvp=>kvp.Key.StartsWith(folder)).ToDictionary(kvp=>kvp.Key.Remove(0,folder.Length),kvp=>kvp.Value);
        }


        public void Apply(Duck duck)
        {
            var persona = duck.persona;

            Vec3 color = persona.color;
            bool recolor = GetSettingBool("RecolorDucks",false);

            Tex2D tex;

            if (Textures.TryGetValue("Duck Texture",out tex))
                persona.sprite = GetFixedSpriteMap(tex, persona.sprite,defDuckSize, duckSpriteSize, recolor, color);

            if (Textures.TryGetValue("Quack Texture", out tex))
                persona.quackSprite = GetFixedSpriteMap(tex, persona.quackSprite, defDuckSize, duckSpriteSize, recolor, color);
            else persona.quackSprite = persona.sprite;

            if (Textures.TryGetValue("Controlled Texture", out tex))
                persona.controlledSprite = GetFixedSpriteMap(tex, persona.controlledSprite, defDuckSize, duckSpriteSize, recolor, color);
            else persona.controlledSprite = persona.quackSprite;

            if (Textures.TryGetValue("Feather Texture", out tex))
                persona.featherSprite = GetFixedSpriteMap(tex,persona.controlledSprite,defFeatherSize,FeatherSpriteSize,recolor,color);

            if (Textures.TryGetValue("Arm Texture",out tex))
                persona.armSprite = GetFixedSpriteMap(tex, persona.armSprite, defArmSize, ArmSpriteSize, recolor, color);

            foreach (var component in Components)
                component.OnApply(duck);

            duck.InitProfile();
        }

        public static bool Exists(string UID)
        {
            return String.IsNullOrEmpty(UID) ? false : Directory.Exists(reskinMod.ReskinPath + UID);
        }

        public string getTexturePath(string texture)
        {
            return Textures.ContainsKey(texture) ? Path.Combine(directory,texture+".png") : null;
        }

        public string GetPath(string md5orDir)
        {
            return Directory.Exists(md5orDir) ? md5orDir : reskinMod.ReskinPath + md5orDir;
        }



        public static string CreateReskinFiles(ReskinFile file)
        {
            string folder = reskinMod.ReskinPath + file.UID + '\\';

            if (!Directory.Exists(reskinMod.ReskinPath)) Directory.CreateDirectory(reskinMod.ReskinPath);
            if (Directory.Exists(folder)) return folder; else Directory.CreateDirectory(folder);

            foreach (var chunk in file.OtherData)
                CreateFile(chunk,folder);

            return folder;
        }

        static void CreateFile(DataChunk chunk,string folder)
        {
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string baseName = folder + chunk.Key;
            if (chunk is ImageChunk)
                File.WriteAllBytes(baseName + ".png", chunk.GetCustomData());
            else if (chunk is SettingsChunk)
                File.AppendAllText(folder+SettingsFileName, '\n'+(chunk as SettingsChunk).Text);
            else if (chunk is ChunkGroup)
                foreach (var c in (chunk as ChunkGroup).chunks)
                    CreateFile(c, baseName + '\\');
        }


        public static ReskinFile tryLoadReskin(Tex2D texture,bool loadHat = true)
        {
            try{ return ReskinFile.ParseFile(TextureHelper.getBitmap(texture),loadHat); } catch { }

            return null;
        }

        static string[] GetFiles(string path, string filter, bool topOnly = true)
        {
            return Directory.GetFiles(path, filter, topOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
        }


        public static void InitializeReskins()
        {
            MonoMain.loadMessage = "loading new reskins";


            List<string> files = new List<string>(GetFiles(Directory.GetCurrentDirectory(), "*.rsk"));
            foreach (string dir in ModLoader.accessibleMods.Select(x=>x?.configuration?.contentDirectory?.Replace('/','\\')).Where(x=>x != null))
                files.AddRange(GetFiles(dir,"*.rsk",false));

            foreach (Team team in files.Select(x => Team.Deserialize(x)))
            {
                if (team == null || !team.hasHat || team.hat.texture.height <= ReskinFile.HatHeight || team.hat.texture.width != 64) continue;

                DevConsole.Log("trying to load skin: " + team.name, Color.Green);
                ReskinFile reskin = tryLoadReskin(team.hat.texture);
                if (reskin == null) continue;

                string dir = CreateReskinFiles(reskin);

                ReskinFile file = new ReskinFile(reskin.Hat);
                file.OtherData.Add(new TextChunk("MD5", reskin.UID));

                if (!IsValid(GetReskin(reskinMod.ReskinPath+reskin.UID))) continue;

                if (!hatData.ContainsKey(reskin.UID))
                    hatData.Add(reskin.UID, team.customData);

                team.customData = file.getHat(team.name + "md5"); ;

                file.Hat.Dispose();
                reskin.Hat.Dispose();

                DevConsole.Log("successfully loaded: " + reskin.UID+"/"+team.name+", with customdatasize: "+team.customData.Length, Color.Green);

                Teams.AddExtraTeam(team);
            }

            DuckEvents.OnDuckQuack += OnDuckQuack;
            DuckEvents.OnDuckSpawn += OnDuckSpawned;

            MonoMain.loadMessage = "loading mods";
        }

        static void OnDuckQuack(object sender, EventArgs args)
        {
            Profile profile = sender as Profile;
            if (profile.duck == null) return;

            Reskin skin;
            if (reskinMod.ActiveReskins.TryGetValue(profile, out skin))
                foreach (var component in skin.Components)
                    component.OnQuack(profile.duck);
        }

        static void OnDuckSpawned(object sender, EventArgs args)
        {
            Profile profile = sender as Profile;
            if (profile.duck == null) return;

            Reskin skin;
            if (reskinMod.ActiveReskins.TryGetValue(profile, out skin))
                foreach (var component in skin.Components)
                    component.OnSpawn(profile.duck);
        }


    }
}
