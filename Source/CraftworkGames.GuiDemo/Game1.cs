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
        private Texture2D _backgroundTexture;
        private Texture2D _blurredRectangle;

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
            _backgroundTexture = Content.Load<Texture2D>("Background");
            _blurredRectangle = Content.Load<Texture2D>("BlurredRectangle");

            Directory.SetCurrentDirectory(Content.RootDirectory);
            var fontFile = "ExampleFont.fnt";
            
            var drawManager = new GuiDrawManager(GraphicsDevice, Content, fontFile);
            var inputManager = new GuiInputManager();

            _gui = new GuiSystem(drawManager, inputManager);
            
            var textureAtlas = TextureAtlas.Load(Content, "FrameworkGUI.xml");
            var titleScreen = CreateTitleScreen(textureAtlas);

            _gui.RootLayout = titleScreen;
        }

        private ILayoutControl CreateTitleScreen(TextureAtlas textureAtlas)
        {
            var layout = new DockLayout();

            var stackLayout = new StackLayout()
            {
                HorizontalAlignment = HorizontalAlignment.Centre,
                VerticalAlignment = VerticalAlignment.Centre
            };

            var titleImage = new Image(new VisualStyle(textureAtlas.GetRegion("CraftworkGUI")))
            {
                Margin = new Margin(0, 0, 0, 40)
            };
            stackLayout.Controls.Add(titleImage);

            var playButton = CreateScalingButton(textureAtlas.GetRegion("PlayButton"));            
            stackLayout.Controls.Add(playButton);

            layout.Controls.Add(new DockItem(stackLayout, DockStyle.Fill));

            var optionsButton = CreateScalingButton(textureAtlas.GetRegion("CogButton"));
            optionsButton.VerticalAlignment = VerticalAlignment.Bottom;
            optionsButton.Clicked += (s, e) => _gui.RootLayout = CreateOptionsScreen(textureAtlas);
            layout.Controls.Add(new DockItem(optionsButton, DockStyle.Left));

            var socialStackLayout = new StackLayout()
            {
                VerticalAlignment = VerticalAlignment.Bottom
            };

            var facebookButton = CreateTiltingButton(textureAtlas.GetRegion("Facebook"), 0.1f);            
            facebookButton.Clicked += (s, e) => Process.Start("https://www.facebook.com/CraftworkGames");
            socialStackLayout.Controls.Add(facebookButton);

            var twitterButton = CreateTiltingButton(textureAtlas.GetRegion("Twitter"), -0.1f);            
            twitterButton.Clicked += (s, e) => Process.Start("https://twitter.com/craftworkgames");
            socialStackLayout.Controls.Add(twitterButton);

            layout.Controls.Add(new DockItem(socialStackLayout, DockStyle.Right));

            return layout;
        }

        private ILayoutControl CreateOptionsScreen(TextureAtlas textureAtlas)
        {
            var dockLayout = new DockLayout();

            var stackLayout = new StackLayout()
            {
                HorizontalAlignment = HorizontalAlignment.Centre,
                VerticalAlignment = VerticalAlignment.Centre
            };

            var toggleButton = new ToggleButton(new VisualStyle(textureAtlas.GetRegion("TickButton")), new VisualStyle(textureAtlas.GetRegion("CrossButton")))
            {
                Text = "Music",                
            };            
            stackLayout.Controls.Add(toggleButton);

            dockLayout.Controls.Add(new DockItem(stackLayout, DockStyle.Fill));

            var backButton = CreateScalingButton(textureAtlas.GetRegion("BackButton"));
            backButton.HorizontalAlignment = HorizontalAlignment.Left;
            backButton.Clicked += (s, e) => _gui.RootLayout = CreateTitleScreen(textureAtlas);
            dockLayout.Controls.Add(new DockItem(backButton, DockStyle.Bottom));

            return dockLayout;
        }

        private Button CreateScalingButton(ITextureRegion textureRegion)
        {            
            return new Button(new VisualStyle(textureRegion))
            {
                HoverStyle = new VisualStyle(textureRegion) { Scale = new Vector2(1.05f) },
                PressedStyle = new VisualStyle(textureRegion) { Scale = new Vector2(0.95f) },
            };
        }

        private Button CreateTiltingButton(ITextureRegion textureRegion, float rotation)
        {
            return new Button(new VisualStyle(textureRegion))
            {                
                HoverStyle = new VisualStyle(textureRegion)
                {
                    Rotation = rotation,
                    Origin = new Vector2(0.5f, 0.5f)
                },
            };
        }

        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _rotation += deltaTime * 0.1f;
            _gui.Update(deltaTime);

            base.Update(gameTime);
        }

        private float _rotation = 0;
        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            var position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            var rotation = _rotation;
            var origin = new Vector2(_blurredRectangle.Width / 2, _blurredRectangle.Height / 2);
            var scale = 1f;
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.Draw(_blurredRectangle, position, null, new Color(Color.White, 0.25f), rotation, origin, scale, SpriteEffects.None, 0);
            _spriteBatch.End();

            _gui.Draw();
            
            base.Draw(gameTime);
        }
    }
}

