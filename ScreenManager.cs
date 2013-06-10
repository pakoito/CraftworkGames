using System;
using System.Collections.Generic;
using CraftworkGames.Core;

namespace CraftworkGames.Gui
{
    public class ScreenManager
    {
        public ScreenManager(GuiSystem guiManager, TextureAtlas textureAtlas)
        {
            _guiManager = guiManager;
            TextureAtlas = textureAtlas;
        }

        public TextureAtlas TextureAtlas { get; private set; }

        private GuiSystem _guiManager;
        private Dictionary<int, Screen> _screenMap = new Dictionary<int, Screen>();

        public void Register(int id, Screen screen)
        {
            if(!_screenMap.ContainsKey(id))
                _screenMap.Add(id, screen);
        }

        public void Activate(int id)
        {
            var screen = _screenMap[id];

            if(!screen.IsInitialised)
                screen.Initialise(this);

            if (_guiManager.Screen != null)
                _guiManager.Screen.Deactivate();

            screen.Activate();
            _guiManager.Screen = screen;

        }

        public void Activate(int id, float delay)
        {
            var delayedAction = new DelayedAction(() => Activate(id), delay);
            //_engine.AddEntity(delayedAction);
        }

        public ITextureRegion GetTexture(string name)
        {
            return _guiManager.LoadTexture(name);
        }
    }
}

