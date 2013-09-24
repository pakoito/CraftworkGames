using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using CraftworkGames.Gui;
using CraftworkGames.Core;
using System.IO;

namespace CraftworkGames.GuiDemo
{
	/// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GuiSystem _gui;

        public Game1(bool isFullScreen = false)
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = isFullScreen;     

            Content.RootDirectory = "Content";	
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Directory.SetCurrentDirectory(Content.RootDirectory);
            var textureAtlas = TextureAtlas.Load(Content, "FrameworkGUI.xml");
            var fontFile = "ExampleFont.fnt";

            _gui = new GuiSystem(_graphics.GraphicsDevice, Content);
            _gui.LoadContent(new GuiContent(textureAtlas, fontFile));

            var screen = new TitleScreen();

            _gui.AddComponent(screen);
        }

        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            var deltaTime = (float)gameTime.ElapsedGameTime.Seconds;

            _gui.Update(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
		
            _gui.Draw();
            
            base.Draw(gameTime);
        }
    }
}

