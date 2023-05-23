using System.Collections.Generic;
using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Arkanoid
{
    public class MenuItem: GameEntity
    {
        private string info;

        public MenuItem()
        {
            
        }
        
        public override void Move()
        {
            
        }
        
    }

    public class Menu: GameEntity
    {
        public Button[] items = new Button[]
        {
            new Button(210,100,380,50,"Resume Game"),
            new Button(210,160,380,50,"New Game"),
            new Button(210,220,380,50,"Save Game"),
            new Button(210,280,380,50,"Load Txt"),
            new Button(210,340,380,50,"Load Json"),
            new Button(210,400,380,50,"Settings"),
            new Button(210,460,380,50,"Exit Game"),
        };

        public Menu()
        {
            items[0].Click += (sender, e) => { Game.Continue(); };
            items[1].Click += (sender, e) => { Game.NewGame(); };
            items[2].Click += (sender, e) => { Game.Save(); };
            items[3].Click += (sender, e) => { Game.Load(); };
            items[4].Click += (sender, e) => { Game.LoadJson(); };
            items[5].Click += (sender, e) => { Game.ShowSettings(); };
            items[6].Click += (sender, e) => { Game.Exit(); };
            visible = false;
            isMoving = false;
            shape = new RectangleShape(new Vector2f(400, 400));
            shape.OutlineColor = Color.Transparent;
            shape.OutlineThickness = 1;
           // color = new Color(100,107,99);
            color = Color.Transparent;
            shape.FillColor = color;
            shape.Position = new Vector2f(200, 50);
        }

        public void ShowMenu()
        {
            visible = true;
        }

        public void HideMenu()
        {
            visible = false;
        }

        public override void draw(RenderWindow window)
        {
            window.Draw(shape);
            foreach (var el in items)
            {
                el.CheckClick(window,Mouse.GetPosition(window));
                el.draw(window);
            }
        }

        public override void Move()
        {
            
        }
    }
}