#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace SideScrollingShooter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keyboardState;

        Player player;
        SpriteFont font;
        SpriteFont wtfont;

        Texture2D enemyTexture;
        List<Enemy> enemies;

        Random randomLocation;

        bool wtf = false;

        public Game1()
            : base()
        {
            Constants constants = new Constants();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = constants.ScreenHeight;
            graphics.PreferredBackBufferWidth = constants.ScreenWidth;
            enemies = new List<Enemy>();
            randomLocation = new Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Constants constants = new Constants();
            Texture2D playerTexture = Content.Load<Texture2D>("player");
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");
            enemyTexture = Content.Load<Texture2D>("enemy");
            Vector2 position = new Vector2(
                constants.ScreenWidth / 4, (constants.ScreenHeight - playerTexture.Height) / 2);

            player = new Player(playerTexture, bulletTexture, position);

            font = Content.Load<SpriteFont>("Fps");
            wtfont = Content.Load<SpriteFont>("WTF");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();

            Control();
            player.Move(gameTime);

            foreach (Bullet bullet in player.ShotsFired)
            {
                bullet.Move(gameTime);
                bullet.CheckOffScreen();
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.HitBox.Intersects(bullet.HitBox))
                    {
                        bullet.MoveOffScreen();
                        enemy.MoveOffScreen();
                    }
                }
            }
            player.CheckWallCollision();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            Constants constants = new Constants();

            spriteBatch.Begin();

            player.Draw(spriteBatch);

            foreach (Bullet bullet in player.ShotsFired)
            {
                bullet.Draw(spriteBatch);
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font,
                "" + (int)(1 / (float)gameTime.ElapsedGameTime.TotalSeconds),
                new Vector2(0, 0), Color.White);

            if (wtf)
            {
                spriteBatch.DrawString(wtfont,
                    "WTF", new Vector2(constants.ScreenWidth / 3.5f,
                        (constants.ScreenHeight - 100) / 2), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void Control()
        {
            player.Control(keyboardState);

            if (keyboardState.IsKeyDown(Keys.E))
            {
                SpawnEnemy();
            }

            if (keyboardState.IsKeyDown(Keys.Q))
            {
                wtf = true;
            }
        }

        private void SpawnEnemy()
        {
            Constants constants = new Constants();

            Vector2 enemyLocation = new Vector2(
                            (constants.ScreenWidth - enemyTexture.Width) -
                            (constants.ScreenWidth / 2) * (float)randomLocation.NextDouble(),
                            (constants.ScreenHeight - enemyTexture.Height) -
                            (constants.ScreenHeight) * (float)randomLocation.NextDouble());

            foreach (Enemy enemy in enemies)
                {

                    if (enemy.CheckOffScreen())
                    {
                        enemy.Position = enemyLocation;
                        return;
                    }
                }
                Enemy newEnemy = new Enemy(enemyTexture, enemyLocation);
                enemies.Add(newEnemy);
            }
        }
    }
