
using SFML.Graphics;
using SFML.System;

namespace GameEngine
{
    public class StatusBar: GameEntity
    {
        public Button butMenu;
        public Text s;
        public StatusBar()
        {
            isMoving = false;
            visible = true;
            shape = new RectangleShape(new Vector2f(800, 60));
            shape.OutlineColor = Color.Black;
            shape.OutlineThickness = 1;
            color = new Color(100,107,99);
            shape.FillColor = color;
            shape.Position = new Vector2f(0, 0);
            butMenu = new Button(600, 20, 70, 30, "Menu");
            butMenu.Click += (sender, e) => { Game.Pause(null,null); };
            s = new Text(20, 200, 20, 100, 40, Game.player.GetStat());
        }

        public override void draw(RenderWindow window)
        {
            window.Draw(shape);
            s.UpdateText(Game.player.GetStat());
            s.draw(window);
            butMenu.draw(window);
        }

        public override void Move()
        {
            
        }
    }
}