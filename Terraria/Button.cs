using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameEngine
{
    public class Button: Text
    {
        public Button(int x, int y, int width, int height, string str):base(str)
        {
            x1 = x;
            y1 = y;
            x2 = x1 + width;
            y2 = y1 + height;
            shape = new RectangleShape(new Vector2f(width,height));
            color = new Color(219, 215, 210);
            shape.FillColor = color;
            shape.OutlineColor = Color.Black;
            shape.OutlineThickness = 1;
            shape.Position = new Vector2f(x, y);
            text.CharacterSize = (uint)(0.5 * height);
            text.Position = new Vector2f(
                x + width / 2 - text.GetGlobalBounds().Width / 2,
                y + height / 2 - text.GetGlobalBounds().Height / 2
            );
        }
        
        public bool IsClicked(RenderWindow window, Vector2i mousePos)
        {
            var position = Mouse.GetPosition(window);
            Vector2f scalingFactors = new Vector2f(
                (float)800/window.Size.X,
                (float)600/window.Size.Y
            );
            Vector2f newMousePos = new Vector2f(
                position.X * scalingFactors.X,
                position.Y * scalingFactors.Y
            );
            if (shape.GetGlobalBounds().Contains(newMousePos.X, newMousePos.Y) && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                //Mouse.SetPosition(new Vector2i(mousePos.X, mousePos.Y+30), window);
                return true;
            }
            return false;
        }

        public event EventHandler Click;

        protected virtual void OnClick()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }

        public void SetVisible(bool vis)
        {
            this.visible = vis;
        }

        public void CheckClick(RenderWindow window, Vector2i mousePos)
        {
            if (IsClicked(window, mousePos)) 
            {
                OnClick();
            }
        }

        public void ChangeColor(Color c)
        {
            color = c;
            shape.FillColor = color;
        }
        
        public override void draw(RenderWindow window)
        {
            this.CheckClick(window, Mouse.GetPosition(window));
            window.Draw(shape);
            window.Draw(text);
        }
    }
}