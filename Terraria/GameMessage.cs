
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameEngine
{
    public class GameMessage: GameEntity
    {
        public Text message;
        public Button butOk;

        public GameMessage(string str)
        {
            shape = new RectangleShape(new Vector2f(200, 120));
            shape.FillColor = new Color(100,107,99);
            shape.Position = new Vector2f(350, 200);
            shape.OutlineColor = Color.Black;
            shape.OutlineThickness = 1;
            butOk = new Button(400, 270, 100, 30, "OK");
            butOk.Click += OnBOKClick;
            message = new Text(20,  350,200,200,100, str);
            visible = false;
            isMoving = false;
        }

        public void OnBOKClick(object sender, EventArgs e)
        {
            HideMessage();
            Game.Pause(null,null);
        }
        public void ShowMessage()
        {
            visible = true;
            Game.pause = true;
        }

        public override void draw(RenderWindow window)
        {
            butOk.CheckClick(window,Mouse.GetPosition(window));
            window.Draw(shape);
            butOk.draw(window);
            message.draw(window);
        }

        public void HideMessage()
        {
            visible = false;
            Game.pause = false;
        }
        
        public override void Move()
        {
            
        }
        
    }

    public class Messages
    {
        //public GameMessage mLostLives;
        public GameMessage mLostGame;
        public GameMessage mWinGame;

        public Messages()
        {
            mLostGame = new GameMessage("You lost");
            mWinGame = new GameMessage("You won");
        }
    }
}