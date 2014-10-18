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
    abstract class GameObject
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 acceleration;
        protected Vector2 friction;
        protected Rectangle hitBox;

        public Rectangle HitBox
        {
            get
            {
                return new Rectangle(
                    (int)position.X, (int)position.Y,
                    texture.Width, texture.Height);
            }
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            velocity = new Vector2(0, 0);
            acceleration = new Vector2(0, 0);
            friction = new Vector2(0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Move(GameTime gameTime)
        {
            //NOTE: Max speed can be calculated with the following formula:
            //(-acceleration*friction)/(1-friction)
            position += velocity;
            velocity += acceleration;
            velocity /= friction;
            acceleration = Vector2.Zero;
        }
    }
}
