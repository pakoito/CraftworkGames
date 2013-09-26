using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using CraftworkGames.Core;

namespace CraftworkGames.Gui
{
    public class GuiInputManager : IInputManager
    {
        public GuiInputManager()
        {
        }

        public event ItemEventHandler<Keys> KeyPressed;

        public void ReadInputState(float screenScaleX, float screenScaleY)
        {
            var previousKeys = _keyboardState.GetPressedKeys();

            _mouseState = Mouse.GetState();
            _keyboardState = Keyboard.GetState();
            _mousePosition = new Point((int)(_mouseState.X / screenScaleX), (int)(_mouseState.Y / screenScaleY));

            if (previousKeys != null)
            {
                foreach (var key in previousKeys)
                {
                    if (_keyboardState.IsKeyUp(key))
                    {
                        if (KeyPressed != null)
                            KeyPressed(this, new ItemEventArgs<Keys>(key));
                    }
                }
            }
        }

        private MouseState _mouseState;
        private KeyboardState _keyboardState;

        public bool IsInputPressed
        {
            get
            {
                return _mouseState.LeftButton == ButtonState.Pressed;
            }
        }

        public bool IsShiftDown
        {
            get
            {
                return _keyboardState.IsKeyDown(Keys.LeftShift) || _keyboardState.IsKeyDown(Keys.RightShift);
            }
        }

        private Point _mousePosition;
        public Point MousePosition
        {
            get
            {
                return _mousePosition;
            }
        }
    }
}
