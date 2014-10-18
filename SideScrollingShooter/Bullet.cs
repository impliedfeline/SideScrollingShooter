using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SideScrollingShooter
{
    class Bullet : GameObject
    {
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity
        {
            set { velocity = value; }
        }

        public Bullet(Texture2D texture, Vector2 position, Vector2 velocity)
            : base(texture, position)
        {
            this.velocity = velocity;
            acceleration = new Vector2(0, 0);
            friction = new Vector2(1, 1);
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
