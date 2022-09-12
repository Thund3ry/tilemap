using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace attempt6Tilemap
{
    internal class TilleMap
    {
        private int[,] levelMap;

        public TilleMap()
        {

        }
        public TilleMap(int[,] map)
        {
            this.levelMap = map;
        }
        public void ReadLevel() //to put the txt file into the array
        {
            string line = "";
            char[] chr = new char[63];

            using (StreamReader sr = new StreamReader("Level1map.txt"))
            {
                for (int i = 0; i <= 15; i++)
                {
                    line = sr.ReadLine();
                    chr = line.ToCharArray();

                    for (int j = 0; j <= 63; j++)
                    {
                        levelMap[j, i] = chr[j];
                    }
                }
            }
        }
        public void DrawLevel1(Texture2D tileMap, SpriteBatch s, Camera m, Rectangle stone1, Rectangle stone2
            , Rectangle stone3, Rectangle stone4, Rectangle stone5) //to draw the level
        {
            //itterates throught the array, checks which texturre to draw
            //draws each tile, only draws tiles that appear in the background

            s.Begin(transformMatrix: m.transform);
            for (int i = 0; i < levelMap.GetLength(0); i++)
            {
                for (int j = 0; j < levelMap.GetLength(1); j++)
                {
                    if (levelMap[i, j] == 49)
                    {
                        s.Draw(tileMap, new Rectangle(i * 16, j * 16, 16, 16), stone1, Color.White);
                    }
                    if (levelMap[i, j] == 50)
                    {
                        s.Draw(tileMap, new Rectangle(i * 16, j * 16, 16, 16), stone2, Color.White);
                    }
                    if (levelMap[i, j] == 51)
                    {
                        s.Draw(tileMap, new Rectangle(i * 16, j * 16, 16, 16), stone3, Color.White);
                    }
                    if (levelMap[i, j] == 52)
                    {
                        s.Draw(tileMap, new Rectangle(i * 16, j * 16, 16, 16), stone4, Color.White);
                    }
                    if (levelMap[i, j] == 53)
                    {
                        s.Draw(tileMap, new Rectangle(i * 16, j * 16, 16, 16), stone5, Color.White);
                    }
                }
            }
            s.End();
        }
    }
}
