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


    class Player : GameObject
    {

        private Texture2D bulletTexture;
        private List<Bullet> shotsFired; // kek
        public List<Bullet> ShotsFired
        {
            get { return shotsFired; }
        }
        private Vector2 bulletTrajectory;
        private Random bulletSpread;

        public Player(Texture2D texture, Texture2D bulletTexture, Vector2 position)
            : base(texture, position)
        {
            this.bulletTexture = bulletTexture;
            shotsFired = new List<Bullet>();
            bulletTrajectory = new Vector2(20, 0);
            bulletSpread = new Random();
            friction = new Vector2(1.05f, 1.05f);
        }

        public void CheckWallCollision()
        {
            Constants constants = new Constants();

            if (position.Y < 0)
            {
                position.Y = 0;
                velocity.Y = 0;
            }

            if (position.Y > constants.ScreenHeight - texture.Height)
            {
                position.Y = constants.ScreenHeight - texture.Height;
                velocity.Y = 0;
            }

            if (position.X < 0)
            {
                position.X = 0;
                velocity.X = 0;
            }

            if (position.X > constants.ScreenWidth - texture.Width)
            {
                position.X = constants.ScreenWidth - texture.Width;
                velocity.X = 0;
            }
        }

        public void Control(KeyboardState keyboardState)
        {
            float speed = 0.75f;

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                acceleration.Y -= speed;

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                acceleration.Y += speed;

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                acceleration.X -= speed;

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                acceleration.X += speed;

            if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
                Fire();

            if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
            {
                Vector2 one = new Vector2(1, 1);
                acceleration /= 5;
                velocity = (-acceleration * friction) / (one - friction);
            }
        }

        private void Fire()
        {
            Vector2 position = new Vector2(
                this.position.X + texture.Width * (float)bulletSpread.NextDouble(),
                this.position.Y + (texture.Height - (bulletTexture.Height * 2 )  * (float)bulletSpread.NextDouble()) / 2);

            foreach (Bullet bullet in shotsFired)
            {
                if (bullet.CheckOffScreen())
                {
                    BulletSpread();
                    bullet.Position = position;
                    bullet.Velocity = bulletTrajectory; // + velocity to make bullet.Velocity be affected by player movement
                    bulletTrajectory.Y = 0;
                    return;
                }
            }
            BulletSpread();
            Bullet newBullet = new Bullet(bulletTexture, position, bulletTrajectory);
            bulletTrajectory.Y = 0;
            shotsFired.Add(newBullet);
        }

        private void BulletSpread()
        {
            float angle = 0.5f;
            bulletTrajectory.Y = angle - (2 * angle) * (float)bulletSpread.NextDouble();
        }
    }
}
