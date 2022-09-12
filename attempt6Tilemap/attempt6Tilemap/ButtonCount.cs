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
    internal class ButtonCount
    {
        public int count { get; set; }
        public ButtonCount()
        {

        }
        public ButtonCount(int count)
        {
            this.count = count;
        }
    }
}
