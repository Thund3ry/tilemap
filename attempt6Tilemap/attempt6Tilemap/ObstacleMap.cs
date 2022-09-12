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
    internal class ObstacleMap : TilleMap
    {
        public int[,] levelMapObst;

        public ObstacleMap(): base ()
        {

        }
        public ObstacleMap(int[,] obstMap) : base (obstMap)
        {
            this.levelMapObst = obstMap;
        }
        public void ReadLevelObst() //to put the txt file into the array
        {
            string line = "";
            char[] chr = new char[63];

            using (StreamReader sr = new StreamReader("Level1ObstMap.txt"))
            {
                for (int i = 0; i <= 15; i++)
                {
                    line = sr.ReadLine();
                    chr = line.ToCharArray();

                    for (int j = 0; j <= 63; j++)
                    {
                        levelMapObst[j, i] = chr[j];
                    }
                }
            }
        }
        public void DrawLevel1Obst(Texture2D tileMap, SpriteBatch s, Camera m, Rectangle obst1) //to draw the level
        {
            //itterates through the array and draws each obstacle

            s.Begin(transformMatrix: m.transform);
            for (int i = 0; i < levelMapObst.GetLength(0); i++)
            {
                for (int j = 0; j < levelMapObst.GetLength(1); j++)
                {
                    if (levelMapObst[i, j] == 49)
                    {
                        s.Draw(tileMap, new Rectangle(i * 16, j * 16, 16, 16), obst1, Color.White);
                    }           
                }
            }
            s.End();
        }
    }
}
