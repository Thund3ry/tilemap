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
    internal class Sprite
    {
        public Color spriteColor { get; set; }
        public Vector2 spritePosition { get; set; }
        public Vector2 spriteSize { get; }
        public Texture2D spriteTexture { get; set; }

        public Texture2D tileMapTex;

        public Sprite()
        {

        }
        public Sprite(Texture2D tex, Vector2 pos, Vector2 size)
        {
            this.spriteTexture = tex;
            this.spritePosition = pos;
            this.spriteSize = size;
            spriteColor = Color.White;
        }
        public Sprite(Vector2 spritePosition, Vector2 spriteSize, Texture2D tileMapTex)
        {
            this.spritePosition = spritePosition;
            this.spriteSize = spriteSize;
            this.spriteTexture = spriteTexture;
            this.tileMapTex = tileMapTex;
        }

        public Rectangle playerRectangle
        {
            get { return new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteTexture.Width, spriteTexture.Height); }
        }
        public void DrawSprite(SpriteBatch s, Texture2D t)
        {
            spriteTexture = t;

            s.Begin();

            s.Draw(spriteTexture, new Rectangle((int)spritePosition.X, (int)spritePosition.Y, (int)spriteSize.X, (int)spriteSize.Y), spriteColor);

            s.End();
        }
        public void DrawSpriteM(SpriteBatch s, Texture2D t, Camera m)
        {
            spriteTexture = t;

            s.Begin(transformMatrix: m.transform) ;

            s.Draw(spriteTexture, new Rectangle((int)spritePosition.X, (int)spritePosition.Y, (int)spriteSize.X, (int)spriteSize.Y), spriteColor);

            s.End();
        }
        public void DraWSpriteTileMap(SpriteBatch s, Rectangle slow1) //to draw the level
        {
            s.Begin();
            s.Draw(tileMapTex, new Rectangle((int)spritePosition.X, (int)spritePosition.Y, (int)spriteSize.X, (int)spriteSize.Y), slow1, Color.White);
            s.End();
        }
    }
}
