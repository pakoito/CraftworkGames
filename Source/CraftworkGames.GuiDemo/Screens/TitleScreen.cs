using System;
using CraftworkGames.Gui;
using Microsoft.Xna.Framework;

namespace CraftworkGames.GuiDemo
{
    public class TitleScreen : Screen
    {
        public TitleScreen()
        {
        }

        private Label _label;
        private Button _playButton;

        public override void OnInitialised(ScreenManager screenManager)
        {
            var textureAtlas = screenManager.TextureAtlas;

            // backgrounds need some work to support a different texture
            // but the following line will show the idea
            //Background = new VisualStyle(textureAtlas.GetRegion("Facebook"));
            
            var textureRegion = textureAtlas.GetRegion("PlayButton");
            var normalStyle = new VisualStyle(textureRegion);
            var hoverStyle = new VisualStyle(textureRegion) { Scale = new Vector2(1.05f) };
            var pressedStyle = new VisualStyle(textureRegion) { Scale = new Vector2(0.95f) };

            _playButton = new Button(normalStyle)
            {
                HoverStyle = hoverStyle,
                PressedStyle = pressedStyle
            };
            _playButton.Clicked += PlayButton_Clicked;
            _playButton.Pressed += PlayButton_Pressed;
            Items.Add(_playButton);

            _label = new Label()
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                Height = 50,
                Width = 150
            };
            Items.Add(_label);
        }

        private int _pressedCount = 0;
        private void PlayButton_Pressed(object sender, EventArgs e)
        {
            _pressedCount++;
            _label.Text = string.Format("Pressed {0} time(s)", _pressedCount);
        }

        private void PlayButton_Clicked (object sender, EventArgs e)
        {
            _label.Text = "Play Button Clicked";
        }
    }
}

