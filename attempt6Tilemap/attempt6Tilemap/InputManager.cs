using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace attempt6Tilemap
{
    internal class InputManager
    {
        MouseState mouseState;
        KeyboardState keyboardState;
        public InputManager()
        {
            
        }
        public void CheckKeys(Player player)
        {
            keyboardState = Keyboard.GetState();
            player.goingDown = false;
            player.goingLeft = false;
            player.goingRight = false;
            player.goingUp = false;
            
            if (keyboardState.IsKeyDown(Keys.A))
            {
                player.goingLeft = true;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                player.goingUp = true;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                player.goingDown = true;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                player.goingRight = true;
            }
        }
        public bool CheckButtonPressed(Vector2 buttoPos, Vector2 buttonSize, int pressCount, ButtonCount button)
        {
            mouseState = Mouse.GetState(); //get the state of the mouse
            if (mouseState.X >= buttoPos.X && mouseState.X <= buttoPos.X + buttonSize.X 
                && mouseState.Y >= buttoPos.Y && mouseState.Y <= buttoPos.Y + buttonSize.Y
                && pressCount == 0 && mouseState.LeftButton == ButtonState.Pressed)
                //checks if the mouse is pressed and the cursor in in the button rectangle
            {
                button.count = 100;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
