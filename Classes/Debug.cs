using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ultimate_Platformer {
    class TextObject {
        public SpriteFont font;
        public Vector2 position;
        public string text;
        public Color color;

        /// <summary>
        /// An object to save information for a DrawText().
        /// </summary>
        /// <param name="font">The font to show</param>
        /// <param name="position">The position on the screen in pixels</param>
        /// <param name="text">The string to draw</param>
        /// <param name="color">The foreground color</param>
        public TextObject(SpriteFont font, Vector2 position, string text, Color color) {
            this.font = font;
            this.position = position;
            this.text = text;
            this.color = color;
        }
    }

    static class Debug {
        private static SpriteBatch spriteBatch = Game1.SpriteBatch;
        private static SpriteFont font = Game1.DebugFont;
        private static List<TextObject> textObjects = new List<TextObject>();

        /// <summary>
        /// Draws text at the specified position.
        /// </summary>
        /// <param name="position">The position on the screen in pixels</param>
        /// <param name="text">The text to draw</param>
        /// <param name="color">The foreground color</param>
        public static void DrawText(Vector2 position, string text, Color? color = null) {
            textObjects.Add(new TextObject(font, position, text, color ?? Color.WhiteSmoke));
        }

        /// <summary>
        /// NOTE: ONLY use this function in the Game.Draw() function after SpriteBatch has started drawing.
        /// 
        /// This draws all the DrawText() calls all at once on the screen and then clears the queue.
        /// </summary>
        public static void Draw() {
            if (textObjects.Count <= 0) return;

            foreach (TextObject textObject in textObjects) {
                spriteBatch.DrawString(textObject.font, textObject.text, textObject.position, textObject.color);
            }

            textObjects.Clear();
        }
    }
}
