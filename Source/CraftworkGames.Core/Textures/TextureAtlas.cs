/*
MIT License
Copyright © 2013 Craftwork Games

All rights reserved.

Authors:
Dylan Wilson

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CraftworkGames.Core
{
    public class TextureAtlas
    {
        public TextureAtlas(string name, Texture2D texture)
        {
            Name = name;
            Texture = texture;
            _regions = new Dictionary<string,ITextureRegion>();
        }

        public string Name { get; private set; }
        public Texture2D Texture { get; private set; }

        private Dictionary<string,ITextureRegion> _regions;
        public IEnumerable<ITextureRegion> Regions
        {
            get
            {
                return _regions.Values;
            }
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Filename { get; private set; }

        public ITextureRegion AddRegion(string name, int x, int y, int width, int height)
        {
            var region = new TextureRegion(Texture, x, y, width, height);
            _regions.Add(name, region);
            return region;
        }

        public void RemoveRegion(string name)
        {
            _regions.Remove(name);
        }

        public ITextureRegion GetRegion(string name)
        {
            ITextureRegion textureRegion;

            if (_regions.TryGetValue(name, out textureRegion))
                return textureRegion;

            return null;
        }

        public static Texture2D LoadTexture(GraphicsDevice graphicsDevice, ContentManager contentManager, string textureName)
        {
            if (graphicsDevice != null)
            {
                string path = textureName;

                using (var stream = TitleContainer.OpenStream(path))
                {
                    var texture = Texture2D.FromStream(graphicsDevice, stream);
                    texture.Name = Path.GetFileName(textureName);
                    return texture;
                }
            }

            return contentManager.Load<Texture2D>(textureName);
        }

        public static TextureAtlas Load(GraphicsDevice graphicsDevice, ContentManager contentManager, string filename)
        {
            try
            {
                string path = filename;
                string textureDirectory = Path.GetDirectoryName(filename);

                using (var stream = TitleContainer.OpenStream(path))
                {
                    var xmlReader = XmlReader.Create(stream);

                    if (xmlReader.ReadToFollowing("TextureAtlas"))
                    {
                        var textureName = xmlReader.GetAttribute("imagePath");
                        var texturePath = Path.Combine(textureDirectory, textureName);
                        var texture = LoadTexture(graphicsDevice, contentManager, texturePath);
                        var textureAtlas = new TextureAtlas(textureName, texture);
                        textureAtlas.Filename = filename;
                        textureAtlas.Width = int.Parse(xmlReader.GetAttribute("width"));
                        textureAtlas.Height = int.Parse(xmlReader.GetAttribute("height"));

                        while (xmlReader.ReadToFollowing("sprite"))
                        {
                            var name = xmlReader.GetAttribute("n");
                            var x = int.Parse(xmlReader.GetAttribute("x"));
                            var y = int.Parse(xmlReader.GetAttribute("y"));
                            var w = int.Parse(xmlReader.GetAttribute("w"));
                            var h = int.Parse(xmlReader.GetAttribute("h"));

                            textureAtlas.AddRegion(name, x, y, w, h);
                        }

                        return textureAtlas;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log an error
                return null;
            }
        }

        public static TextureAtlas Load(ContentManager contentManager, string filename)
        {
            return Load(null, contentManager, filename);
        }
    }
}

