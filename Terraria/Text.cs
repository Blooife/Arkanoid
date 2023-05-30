using SFML.Graphics;
using SFML.System;

namespace GameEngine
{
    public class Text: GameEntity
    {
        public SFML.Graphics.Text text;

        public Text(uint size, int x, int y, int width, int height, string str)
        {
            visible = true;
            isMoving = false;
            text = new SFML.Graphics.Text();
            text.CharacterSize = size;
            color = Color.Black;
            text.FillColor = color;
            text.DisplayedString = str;
            x1 = x;
            y1 = y;
            text.Font = new Font("C:/Users/Asus/Desktop/TerraEngine-master/TerraEngine-master/Terraria/Akshar-Bold.ttf");
            text.Position = new Vector2f(
                x + width / 2 - text.GetGlobalBounds().Width / 2,
                y + height / 2 - text.GetGlobalBounds().Height / 2
            );
            
        }

        public Text(string str)
        {
            visible = true;
            isMoving = false;
            text = new SFML.Graphics.Text();
            text.FillColor = Color.Black;
            text.DisplayedString = str;
            text.Font = new Font("C:/Users/Asus/Desktop/TerraEngine-master/TerraEngine-master/Terraria/Akshar-Bold.ttf");
        }

        public void UpdateText(string newstr)
        {
            text.DisplayedString = newstr;
        }

        public override void draw(RenderWindow window)
        {
           // text.Position = new Vector2f(x1, y1);
            window.Draw(text);
        }

        public override void Move()
        {
            
        }
    }
}