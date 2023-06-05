using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;

namespace GameEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Settings
    { 
        [JsonProperty]public int level; 
        [JsonProperty]public int volume;
        [JsonProperty]public int difficulty;
        [JsonProperty]public string type;
        [JsonProperty] public Vector2i size;
        
        public List<Button> listDiff = new List<Button>();
        public List<Button> listSize = new List<Button>();
        private Button btnClose;
        
        public Settings(int l, int v, int d, int x, int y)
        {
            size = new Vector2i(x, y);
            //Game.Window.Size = new Vector2u((uint)size.X, (uint)size.Y);
            level = l;
            volume = v;
            type = "Settings";
            difficulty = d;
            btnClose = new Button(610, 480, 30, 30, "OK");
            btnClose.Click += CloseSettings;
            listDiff.Add(new Button(200, 230, 140, 40, "Super easy"));
            listDiff.Add(new Button(200, 280, 140, 40, "Easy"));
            listDiff.Add(new Button(200, 330, 140, 40, "Normal"));
            listDiff.Add(new Button(200, 380, 140, 40, "Hard"));
            listDiff.Add(new Button(200, 430, 140, 40, "Super hard"));
            foreach (var b in listDiff)
            {
                b.Click += OnClickDiff;
            }
            listSize.Add(new Button(500, 230, 140, 40, "Size 700*550"));
            listSize.Add(new Button(500, 280, 140, 40, "Size 800*600"));
            listSize.Add(new Button(500, 330, 140, 40, "Size 1024*600"));
            listSize.Add(new Button(500, 380, 140, 40, "Size 1280*760"));
            listSize.Add(new Button(500, 430, 140, 40, "Size full"));
            foreach (var b in listSize)
            {
                b.Click += OnClickSize;
            }
            SettingsVisible(false);
        }

        public void CloseSettings(object sender,EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                if (listDiff[i].color == Color.Red)
                {
                    difficulty = 4 + i;
                    Game.UpdateDifficulty();
                    break;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (listSize[i].color == Color.Red)
                {
                    switch (i)
                    {
                        case 0:
                        {
                            size = new Vector2i(700, 550);
                            Game.ev.OnChangeSize(700,550,300,50);
                            //Game.ChangeWindowSize(700, 550, 300,50);
                            break;
                        }
                        case 1:
                        {
                            size = new Vector2i(800, 600);
                            Game.ChangeWindowSize(800, 600, 250,50);
                            break;
                        }
                        case 2:
                        {
                            size = new Vector2i(1024, 600);
                            Game.ChangeWindowSize(1024, 600,0,0);
                            break;
                        }
                        case 3:
                        {
                            size = new Vector2i(1280, 760);
                            Game.ChangeWindowSize(1280, 760, 0,0);
                            break;
                        }
                        case 4:
                        {
                            size = new Vector2i(1366, 728);
                            Game.ChangeWindowSize(1366, 728, 0,0);
                            break;
                        }
                    }
                    break;
                }
            }
            SettingsVisible(false);
        }
        public void OnClickDiff(object sender, EventArgs e)
        {
            foreach (var b in listDiff)
            {
                b.ChangeColor(new Color(219, 215, 210));
            }
            Button s = (Button)sender;
            s.ChangeColor(Color.Red);
        }
        public void OnClickSize(object sender, EventArgs e)
        {
            foreach (var b in listSize)
            {
                b.ChangeColor(new Color(219, 215, 210));
            }
            Button s = (Button)sender;
            s.ChangeColor(Color.Red);
        }
        public void SettingsVisible(bool v)
        {
            foreach (var b in listDiff)
            {
                b.visible = v;
            }
            foreach (var b in listSize)
            {
                b.visible = v;
            }

            btnClose.visible = v;
        }
        public void UpdateSettings(int l, int v, int d, int x, int y)
        {
            level = l;
            volume = v;
            difficulty = d;
            size = new Vector2i(x, y);
            Game.window.Size = new Vector2u((uint)size.X, (uint)size.Y);
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

        public void AddToList(List<GameEntity> dobj)
        {
            foreach (var b in listDiff)
            {
                dobj.Add(b);
            }
            foreach (var b in listSize)
            {
                dobj.Add(b);
            }
            dobj.Add(btnClose);
        }
        public void SerializeObject(object obj, string filePath)
        {
            Type typ = obj.GetType();
            FieldInfo[] fields = typ.GetFields(BindingFlags.Public | BindingFlags.Instance);
            List<string> lines = new List<string>();
            foreach (FieldInfo field in fields)
            {
                string fieldName = field.Name;
                object fieldValue = field.GetValue(obj);
                lines.Add(fieldName + ":" + fieldValue);
            }
            File.AppendAllLines(filePath, lines);
            
        }
    }
}