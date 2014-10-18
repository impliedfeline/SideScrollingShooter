using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SideScrollingShooter
{
    class Enemy : GameObject
    {

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Enemy(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
        }

        public bool CheckOffScreen()
        {
            Constants constants = new Constants();

            if (position.X >= constants.ScreenWidth)
            {
                velocity = Vector2.Zero;
                return true;
            }
            return false;
        }

        public void MoveOffScreen()
        {
            Constants constants = new Constants();

            position.X = constants.ScreenWidth;
        }
    }
}
