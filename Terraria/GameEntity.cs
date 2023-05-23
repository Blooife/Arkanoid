using System;
using System.Data;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Arkanoid
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class GameEntity : SFML.Graphics.Drawable
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

        public virtual void SerializeToText(string filename) {}
        
        public virtual void DeserializeFromText(string[] prop){}

        public virtual void ChangeDirection(GameEntity obj)
        {
        }
        public virtual void ChangeDirectionX(GameEntity obj)
        {
        }
        public virtual void ChangeDirectionY(GameEntity obj)
        {
        }


    }
}