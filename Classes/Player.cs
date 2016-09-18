using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ultimate_Platformer {
    class Player {
        private GraphicsDeviceManager graphics = Game1.Graphics;
        private SpriteBatch spriteBatch = Game1.SpriteBatch;
        private KeyboardState keyboardState = Game1.KeyboardState;
        private MouseState mouseState = Game1.MouseState;
        private Animator animator;

        private const float velocity = 1f;
        private const float friction = 1.5f;
        private const float maxSpeed = 10;
        private const float jumpVelocity = 15f;
        private const float maxFallingSpeed = 20f;
        private const float gravity = 1f;
        private int facing;

        public const int MaxHealth = 1000;
        public bool falling;
        public int Health;
        public Texture2D Sprite;
        public Vector2 Position;
        public Vector2 Speed;

        public Player(Texture2D sprite) {
            this.Sprite = sprite;

            animator = new Animator(sprite, 64, 64, 11);
        }

        /// <summary>
        /// Initialize the player's variables
        /// </summary>
        public void Init() {
            Health = MaxHealth;
            falling = false;
            facing = 0;
            Position = new Vector2(0, 0);

            animator.AddAnim("IdleLeft", new[] { 0, 1 }, 48);
            animator.AddAnim("IdleRight", new[] { 2, 3 }, 48);
            animator.AddAnim("WalkLeft", new[] { 4, 5, 6, 7 }, 8);
            animator.AddAnim("WalkRight", new[] { 8, 9, 10, 11 }, 8);
            animator.AddAnim("JumpLeft", new[] { 12 }, 8);
            animator.AddAnim("JumpRight", new[] { 13 }, 8);

            animator.StartAnim("IdleRight");
        }

        /// <summary>
        /// Draw the player and it's appurtenants
        /// </summary>
        public void Draw() {
            animator.Animate();

            // Draw player Sprite
            spriteBatch.Draw(animator.Frames[animator.CurrentFrameID], Position, Color.White);
        }

        /// <summary>
        /// Updates the player's controls, movement and actions etc.
        /// </summary>
        public void Update() {
            keyboardState = Game1.KeyboardState; mouseState = Game1.MouseState;

            if (!falling) Speed.Y = 1;

            // ================================================ Player Input & Movement ================================================

            bool isSpacePressed = false;
            bool isWPressed = false;
            bool isAPressed = false;
            bool isSPressed = false;
            bool isDPressed = false;

            // Get keyboard input and handle player controls
            Keys[] keys = keyboardState.GetPressedKeys();
            if (keys.Length > 0) {
                foreach (Keys key in keys) {
                    switch (key) {
                        default:
                            break;
                        case Keys.Space:
                            isSpacePressed = true;
                            break;
                        case Keys.W:
                            isWPressed = true;

                            if (!falling) Speed.Y -= jumpVelocity;
                            break;
                        case Keys.A:
                            isAPressed = true;
                            facing = 0;

                            if (!falling) animator.StartAnim(facing == 0 ? "WalkLeft" : "WalkRight");

                            Speed.X -= velocity;
                            break;
                        case Keys.S:
                            isSPressed = true;

                            Speed.Y += velocity;
                            break;
                        case Keys.D:
                            isDPressed = true;
                            facing = 1;

                            if (!falling) animator.StartAnim(facing == 0 ? "WalkLeft" : "WalkRight");

                            Speed.X += velocity;
                            break;
                    }
                }
            }

            if (!isSpacePressed && !isWPressed && !isAPressed && !isSPressed && !isDPressed && !falling) animator.StartAnim(facing == 0 ? "IdleLeft" : "IdleRight");
            if (falling) animator.StartAnim(facing == 0 ? "JumpLeft" : "JumpRight");

            // If player is not moving, slow down Speed with friction
            if (Speed.X < 0 && !keyboardState.IsKeyDown(Keys.A))
                Speed.X += friction;
            if (Speed.X > 0 && !keyboardState.IsKeyDown(Keys.D))
                Speed.X -= friction;

            // If Speed is inbetween 0 and friction, fix Speed to 0
            if (!keyboardState.IsKeyDown(Keys.A) && Speed.X > -friction && Speed.X < 0) Speed.X = 0;
            if (!keyboardState.IsKeyDown(Keys.D) && Speed.X < friction && Speed.X > 0) Speed.X = 0;

            // Update gravity
            if (falling) Speed.Y += gravity;

            // Limit Speed to maxSpeed
            if (Speed.X < -maxSpeed) Speed.X = -maxSpeed;
            if (Speed.X > maxSpeed) Speed.X = maxSpeed;
            if (Speed.Y > maxFallingSpeed) Speed.Y = maxFallingSpeed;

            Debug.DrawText(new Vector2(5, 5), Speed.ToString(), Color.LightGreen);

            // Update Position
            Position += Speed;

            // Limit sprite to screen borders
            if (Position.X < 0) Position.X = 0;
            if (Position.X + animator.Frames[0].Width > graphics.GraphicsDevice.Viewport.Width) Position.X = graphics.GraphicsDevice.Viewport.Width - animator.Frames[0].Width;
            if (Position.Y < 0) Position.Y = 0;
            if (Position.Y + animator.Frames[0].Height > graphics.GraphicsDevice.Viewport.Height) {
                Position.Y = graphics.GraphicsDevice.Viewport.Height - animator.Frames[0].Height;
                falling = false;
            } else falling = true;
        }

    }
}
