using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ultimate_Platformer {
    class Anim {
        private int frameCount;
        private int count;

        public int CurrentFrameID;
        public int[] FrameIDs;
        public int FrameSpeed;
        public bool Loop;

        public Anim(int[] frameIDs, int frameSpeed, bool loop) {
            CurrentFrameID = 0;
            frameCount = 0;
            count = 0;

            this.FrameIDs = frameIDs;
            this.FrameSpeed = frameSpeed;
            this.Loop = loop;
        }

        public int Start() {
            frameCount = 0;
            count = 0;

            return CurrentFrameID = FrameIDs[frameCount];
        }

        public int Animate() {
            if (frameCount > FrameIDs.Length - 1 && Loop) frameCount = 0;

            if (count++ < FrameSpeed) return -1;

            CurrentFrameID = FrameIDs[frameCount++];
            count = 0;
            return CurrentFrameID;
        }
    }

    class Animator {
        private Hashtable anims = new Hashtable();
        private Texture2D sprite;
        private string currentAnimName;

        public List<Texture2D> Frames = new List<Texture2D>();
        public int CurrentFrameID = 0;

        public Animator(Texture2D sprite, int width, int height, int frameCount) {
            this.sprite = sprite;
            frameCount = (int)Math.Floor((double)sprite.Width/width);

            for (int i = 0; i < frameCount; i++) {
                Texture2D newTexture = CreateTexture(sprite, width, height, i * width);
                Frames.Add( newTexture );
            }
        }

        private Texture2D CreateTexture(Texture2D sprite, int width, int height, int offset = 0) {
            Texture2D texture = new Texture2D(sprite.GraphicsDevice, width, height);

            Color[] originalColors = new Color[sprite.Width * sprite.Height];
            sprite.GetData(originalColors);
            
            int c = 0;
            Color[] newColors = new Color[width * height];
            for (int i = 0; i < width * height; i++) {
                if ((i - width * c) / (width) == 1)  c++;

                 newColors[i] = originalColors[i + sprite.Width * c - c * width + (i == width * height ? offset + width : offset)];
            }
            
            texture.SetData(newColors);

            return texture;
        }

        public bool AddAnim(string animName, int[] frameIDs, int frameSpeed, bool Loop = true) {
            try {
                anims.Add(animName, new Anim(frameIDs, frameSpeed, Loop));
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public void StartAnim(string animName) {
            if (currentAnimName != animName) {
                currentAnimName = animName;
                CurrentFrameID = ((Anim) anims[animName]).Start();
            }
        }

        public void Animate() {
            int result = ((Anim)anims[currentAnimName]).Animate();
            if (result > -1) CurrentFrameID = result;
        }
    }
}
