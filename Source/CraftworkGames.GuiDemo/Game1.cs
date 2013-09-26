using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using CraftworkGames.Gui;
using CraftworkGames.Core;
using System.IO;
using System.Diagnostics;

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
            var fontFile = "ExampleFont.fnt";
            
            var drawManager = new GuiDrawManager(GraphicsDevice, Content, fontFile);
            var inputManager = new GuiInputManager();

            _gui = new GuiSystem(drawManager, inputManager);

            var layout = new DockLayout();
            
            var buttonOnTexture = Content.Load<Texture2D>("ButtonSwitchOn.png");
            var buttonOffTexture = Content.Load<Texture2D>("ButtonSwitchOff.png");
            var textureRegionOn = new TextureRegion(buttonOnTexture);
            var textureRegionOff = new TextureRegion(buttonOffTexture);

            var button = new Button(new VisualStyle(textureRegionOff))
            {
                PressedStyle = new VisualStyle(textureRegionOn),
                VerticalAlignment = VerticalAlignment.Top,                
            };
            layout.Controls.Add(new DockItem(button, DockStyle.Left));

            var textureAtlas = TextureAtlas.Load(Content, "FrameworkGUI.xml");
            var textureRegion = textureAtlas.GetRegion("PlayButton");
            var playButton = new Button(new VisualStyle(textureRegion))
            {
                HoverStyle = new VisualStyle(textureRegion) { Colour = new Color(Color.White, 0.8f) },
                PressedStyle = new VisualStyle(textureRegion) { Scale = new Vector2(0.95f) },
            };
            layout.Controls.Add(new DockItem(playButton, DockStyle.Fill));

            var facebookRegion = textureAtlas.GetRegion("Facebook");
            var facebookButton = new Button(new VisualStyle(facebookRegion))
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                HoverStyle = new VisualStyle(facebookRegion) 
                { 
                    Rotation = 0.1f, 
                    Origin = new Vector2(0.5f, 0.5f) 
                },
            };
            facebookButton.Clicked += (s, e) => Process.Start("https://www.facebook.com/CraftworkGames");

            var twitterRegion = textureAtlas.GetRegion("Twitter");
            var twitterButton = new Button(new VisualStyle(twitterRegion)) 
            { 
                VerticalAlignment = VerticalAlignment.Bottom,
                HoverStyle = new VisualStyle(twitterRegion) 
                { 
                    Rotation = -0.1f, 
                    Origin = new Vector2(0.5f, 0.5f) 
                },
            };
            twitterButton.Clicked += (s, e) => Process.Start("https://twitter.com/craftworkgames");
            layout.Controls.Add(new DockItem(facebookButton, DockStyle.Right));
            layout.Controls.Add(new DockItem(twitterButton, DockStyle.Right));

            _gui.RootLayout = layout;
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

