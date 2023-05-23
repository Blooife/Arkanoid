using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Text = GameEngine.Text;

namespace Arkanoid
{
    public class GameMessage: GameEntity
    {
        public Text message;
        public Button butOK;

        public GameMessage(string str)
        {
            shape = new RectangleShape(new Vector2f(300, 200));
            shape.FillColor = new Color(100,107,99);
            shape.Position = new Vector2f(250, 200);
            shape.OutlineColor = Color.Black;
            shape.OutlineThickness = 1;
            butOK = new Button(300, 340, 200, 50, "ok");
            butOK.Click += (sender, e) => HideMessage();
            message = new Text(30,  250,200,300,150, str);
            visible = false;
            isMoving = false;
        }

        public void ShowMessage()
        {
            visible = true;
            Game.pause = true;
        }

        public override void draw(RenderWindow window)
        {
            butOK.CheckClick(window,Mouse.GetPosition(window));
            window.Draw(shape);
            butOK.draw(window);
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
        public GameMessage mLostLives;
        public GameMessage mLostGame;
        public GameMessage mWinGame;

        public Messages()
        {
            mLostLives = new GameMessage("You lost a live. Enter ok to continue");
            mLostGame = new GameMessage("You lost");
            mWinGame = new GameMessage("You won");
            /*mLostGame.butOK.Click -= (sender, e) =>  mLostGame.HideMessage(); 
            mLostGame.butOK.Click += (sender, e) =>
            {
                mLostGame.HideMessage();
                Game.Pause();
            };*/
        }
    }
}