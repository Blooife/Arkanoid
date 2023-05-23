using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Arkanoid;
using Newtonsoft.Json;
using SFML.System;
using SFML.Window;

namespace GameEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Settings
    { 
        [JsonProperty]public int level; 
        [JsonProperty]public int volume;
        [JsonProperty]public int difficulty;
        [JsonProperty]public string type;
        [JsonProperty] public Vector2i size = new Vector2i();
        public List<Button> listBtn = new List<Button>();
        
        public Settings(int l, int v, int d, int x, int y)
        {
            size = new Vector2i(x, y);
            //Game.Window.Size = new Vector2u((uint)size.X, (uint)size.Y);
            level = l;
            volume = v;
            type = "Settings";
            difficulty = d;
            listBtn.Add(new Button(260, 230, 280, 30, "Easy"));
            listBtn.Add(new Button(260, 290, 280, 30, "Normal"));
            listBtn.Add(new Button(260, 350, 280, 30, "Hard"));
            listBtn.Add(new Button(110, 260, 280, 30, "Difficulty"));
            listBtn.Add(new Button(410, 260, 280, 30, "Screen size"));
            listBtn.Add(new Button(260, 230, 280, 30, "Size 700*550"));
            listBtn.Add(new Button(260, 290, 280, 30, "Size 800*600"));
            listBtn.Add(new Button(260, 410, 280, 30, "Size 1024*600"));
            listBtn.Add(new Button(260, 350, 280, 30, "Size 1280*760"));
            listBtn.Add(new Button(260, 470, 280, 30, "Size full"));
            listBtn[3].Click += (sender, args) => DifficultyOnClick();
            listBtn[4].Click += ScreenSizeOnClick;
            listBtn[0].Click += EasyOnClick;
            listBtn[1].Click += NormalOnClick;
            listBtn[2].Click += HardOnClick;
            listBtn[5].Click += Size1OnClick;
            listBtn[6].Click += Size2OnClick;
            listBtn[7].Click += Size3OnClick;
            listBtn[8].Click += Size5OnClick;
            listBtn[9].Click += Size4OnClick;
            DifficultyVisible(false);
            ScreenSizeVisible(false);
            SettingsVisible(false);
        }

        public void SettingsVisible(bool v)
        {
            for (int i = 3; i <5; i++)
            {
                listBtn[i].visible = v;
            }
        }
        public void EasyOnClick(object sender, EventArgs args)
        {
            DifficultyVisible(false);
            difficulty = 4;
            Game.UpdateDifficulty();
        }
        public void NormalOnClick(object sender, EventArgs args)
        {
            DifficultyVisible(false);
            difficulty = 5;
            Game.UpdateDifficulty();
        }
        public void HardOnClick(object sender, EventArgs args)
        {
            DifficultyVisible(false);
            difficulty = 6;
            Game.UpdateDifficulty();
        }

        public void DifficultyVisible(bool v)
        {
            for (int i = 0; i < 3; i++)
            {
                listBtn[i].visible = v;
            }  
        }

        public void ScreenSizeVisible(bool v)
        {
            for (int i = 5; i < 10; i++)
            {
                listBtn[i].visible = v;
            }  
        }
        
        
        public void DifficultyOnClick()
        {
            SettingsVisible(false);
            DifficultyVisible(true);
        }

        public void ScreenSizeOnClick(object sender, EventArgs ev)
        {
            ScreenSizeVisible(true);
            SettingsVisible(false);
        }
        
        public void Size1OnClick(object sender, EventArgs ev)
        {
            size = new Vector2i(700, 550);
            Game.ChangeWindowSize(700, 550, 0,0);
            ScreenSizeVisible(false);
        }
        public void Size5OnClick(object sender, EventArgs ev)
        {
            size = new Vector2i(1280, 760);
            Game.ChangeWindowSize(1280, 760, 0,0);
            ScreenSizeVisible(false);
        }
        
        public void Size2OnClick(object sender, EventArgs ev)
        {
            size = new Vector2i(800, 600);
            Game.ChangeWindowSize(800, 600, 0,0);
            ScreenSizeVisible(false);
        }
        
        public void Size3OnClick(object sender, EventArgs ev)
        {
            size = new Vector2i(1024, 600);
            Game.ChangeWindowSize(1024, 600,0,0);
            ScreenSizeVisible(false);
        }
        
        public void Size4OnClick(object sender, EventArgs ev)
        {
            size = new Vector2i(1366, 728);
            Game.ChangeWindowSize(1366, 728, 0,0);
            ScreenSizeVisible(false);
        }
        
        public void UpdateSettings(int l, int v, int d, int x, int y)
        {
            level = l;
            volume = v;
            difficulty = d;
            size = new Vector2i(x, y);
            Game.Window.Size = new Vector2u((uint)size.X, (uint)size.Y);
        }
        
        public void SerializeToText(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename,true))
            {
                writer.WriteLine("Settings");
                writer.WriteLine($"{level} {volume} {difficulty} {size.X} {size.Y}");
                writer.Flush();
            }
        }
    }
}