using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using CraftworkGames.Core;
using Microsoft.Xna.Framework.Content;

namespace CraftworkGames.Gui
{
    public class GuiDrawManager : IDrawManager
    {   
        public GuiDrawManager(GraphicsDevice graphicsDevice, ContentManager contentManager, string fontFile)
        {
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            
            // TODO: Allow XNA fonts as well
            LoadFont(fontFile); 
        }

        private GraphicsDevice _graphicsDevice;
        private ContentManager _contentManager;
        private SpriteBatch _spriteBatch;
        private FontRenderer _fontRenderer;

        //public ITextureRegion LoadTexture(string name)
        //{
        //    var texture = _contentManager.Load<Texture2D>(name);
        //    var textureAtlas = new TextureAtlas(name, texture);
        //    return new TextureRegion(texture, name, 0, 0, texture.Width, texture.Height);
        //}

        private void LoadFont(string fontFile)
        {
            string path = fontFile;

            using (var stream = TitleContainer.OpenStream(path))
            {
                var fontData = FontLoader.Load(stream);
                var texture = _contentManager.Load<Texture2D>(fontData.Pages[0].File);
                _fontRenderer = new FontRenderer(fontData, texture);
            }
        }

        public void Draw(int screenWidth, int screenHeight, IEnumerable<Control> controls)
        {
            var scaleX = (float)_graphicsDevice.Viewport.Width / screenWidth;
            var scaleY = (float)_graphicsDevice.Viewport.Height / screenHeight;
            var screenScale = new Vector3(scaleX, scaleY, 1.0f);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
                               SamplerState.AnisotropicClamp, DepthStencilState.Default,
                               RasterizerState.CullNone, null, Matrix.CreateScale(screenScale));

            foreach (var control in controls)
                control.Draw(this);

            _spriteBatch.End();
        }

        public void Draw(IGuiSprite sprite, Rectangle destinationRectangle)
        {
            var textureRegion = sprite.TextureRegion as TextureRegion;
            var texture = textureRegion.Texture;

            if (texture != null)
            {
                var sourceRectangle = textureRegion.Rectangle;
                var destRectangle = destinationRectangle;

                if (sprite.Scale != Vector2.One)
                {
                    var scaledWidth = sprite.Scale.X * destRectangle.Width;
                    var scaledHeight = sprite.Scale.Y * destRectangle.Height;
                    var offsetX = (int)(destRectangle.Width - scaledWidth) / 2;
                    var offsetY = (int)(destRectangle.Height - scaledHeight) / 2;
                    destRectangle = new Rectangle(offsetX + destRectangle.X, offsetY + destRectangle.Y, (int)scaledWidth, (int)scaledHeight);
                }

                _spriteBatch.Draw(texture, destRectangle, sourceRectangle, sprite.Colour, sprite.Rotation, sprite.Origin, sprite.Effect, sprite.Depth);
            }
        }

        private void Draw(TextureRegion textureRegion, Rectangle destinationRectangle)
        {
            var texture = textureRegion.Texture;

            if (texture != null)
            {
                var sourceRectangle = new Rectangle(textureRegion.X, textureRegion.Y, textureRegion.Width, textureRegion.Height);

                _spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        public void DrawText(string text, Rectangle destinationRectangle, TextStyle style)
        {
            if (string.IsNullOrEmpty(text))
                return;

            var textPosition = AlignText(text, destinationRectangle, style);

            _fontRenderer.DrawText(_spriteBatch, textPosition.X, textPosition.Y, text, style.Colour);
        }

        private Point AlignText(string text, Rectangle destinationRectangle, TextStyle style)
        {
            int x = 0, y = 0;
            var size = _fontRenderer.MeasureText(text);
            var centre = destinationRectangle.Center;
            var lineHeight = _fontRenderer.FontFile.Common.LineHeight;

            switch (style.HorizontalAlignment)
            {
                case HorizontalAlignment.Centre:
                    x = centre.X - size.Width / 2;
                    break;
                case HorizontalAlignment.Stretch:
                case HorizontalAlignment.Left:
                    x = destinationRectangle.X;
                    break;
                case HorizontalAlignment.Right:
                    x = destinationRectangle.Right - size.Width;
                    break;
            }

            switch (style.VerticalAlignment)
            {
                case VerticalAlignment.Centre:
                    y = centre.Y - lineHeight / 2;
                    break;
                case VerticalAlignment.Stretch:
                case VerticalAlignment.Top:
                    y = destinationRectangle.Y;
                    break;
                case VerticalAlignment.Bottom:
                    y = destinationRectangle.Bottom - size.Height;
                    break;
            }

            return new Point(x, y);
        }
    }
}
