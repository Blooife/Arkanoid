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
    public abstract class GameEntity : Drawable
    {
        public Shape shape;
        [JsonProperty] public float x1, y1, x2, y2;
        public float width, height;
        public int x, y;
        [JsonProperty]public Color color;
        public int backgroundColor;
        [JsonProperty] public bool visible;
        [JsonProperty] public bool isMoving;
        [JsonProperty] public string type;

        public GameEntity()
        {
            visible = true;
            isMoving = false;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            throw new System.NotImplementedException();
        }

        public abstract void Move();


        public virtual void draw(RenderWindow window)
        {
            float scaleX =(float) window.Size.X / 800;
            float scaleY = (float)window.Size.Y / 600;
            float scale = Math.Min(scaleX, scaleY);
                
            Console.WriteLine($"{scale}  {scaleX}   {scaleY}");
            Console.WriteLine(scale.ToString());
            shape.Position = new Vector2f(x1, y1);
            window.Draw(shape);
        }
        
        public void SerializeObject(object obj, string filePath)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            List<string> lines = new List<string>();
            int i = 0;
            foreach (FieldInfo field in fields)
            {
                    string fieldName = field.Name;
                    object fieldValue = field.GetValue(obj);
                    lines.Add(fieldName + ":" + fieldValue);
                    i++;
            }
            File.AppendAllLines(filePath, lines);
        }

        public T DeserializeObject<T>(string filePath) where T : new()
        {
            T obj = new T();

            using (StreamReader reader = new StreamReader(filePath))
            {
                Type type = typeof(T);
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('=');

                    if (parts.Length == 2)
                    {
                        string fieldName = parts[0].Trim();
                        string fieldValue = parts[1].Trim();

                        FieldInfo field = Array.Find(fields, f => f.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase));

                        if (field != null)
                        {
                            object value = Convert.ChangeType(fieldValue, field.FieldType);
                            field.SetValue(obj, value);
                        }
                    }
                }
            }

            return obj;
        }

        public virtual void UpdateSize()
        {
            
        }

        public bool CheckCollisions(GameEntity obj)
        {
            /*bool res = false;
            int distX = Math.Abs(x - obj.x) - obj.width/2;
            int distY = Math.Abs(y - obj.y) - obj.height/2;

            if (distX + 1 < this.width/2 && distY + 1 < this.width/2) {
                res = true;
                if (distX < distY)
                    ChangeDirectionY(obj);
                else
                    ChangeDirectionX(obj);
            }
            return res;*/
            if(y2 >= obj.y1 && y1 <= obj.y2)
                if (x2 >= obj.x1 && x1 <= obj.x2)
                {
                    return true;
                }
            return false;
        }
        
        public virtual void ChangeWidth(int w){}

        public virtual void SerializeToText(string filename) {}
        
        public virtual void DeserializeFromText(string[] prop){}

        public virtual void OnCollision(GameEntity obj)
        {
        }


    }
}