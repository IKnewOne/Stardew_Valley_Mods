using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Netcode;

namespace Omegasis.StardustCore.Animations
{
    /// <summary>
    /// A class used to store animation information for a single frame.
    /// </summary>
    public class AnimationFrame
    {
        /// <summary>The source rectangle on the texture to display.</summary>
        public Rectangle sourceRectangle;

        /// <summary>The duration of the frame in length.</summary>
        public int frameDuration;

        /// <summary>The duration until the next frame.</summary>
        private int frameCountUntilNextAnimation;

        public float scale = 1f;


        public AnimationFrame()
        {
            this.sourceRectangle = new Rectangle(0, 0, 16, 16);
            this.frameCountUntilNextAnimation = -1;
            this.frameDuration = -1;
            this.scale= 1f;
        }

        public AnimationFrame(int xPos, int yPos, int width, int height, float scale=1f)
        {
            this.sourceRectangle = new Rectangle(xPos, yPos, width, height);
            this.frameCountUntilNextAnimation = -1;
            this.frameDuration = -1;
            this.scale = scale;
        }
        public AnimationFrame(int xPos, int yPos, int width, int height, int existsForXFrames, float scale=1f)
        {
            this.sourceRectangle = new Rectangle(xPos, yPos, width, height);
            this.frameDuration = existsForXFrames;
            this.frameCountUntilNextAnimation = this.frameDuration;
            this.scale = scale;
        }

        /// <summary>Constructor that causes the animation frame count to be set to -1; This forces it to never change.</summary>
        /// <param name="SourceRectangle">The draw source for this animation.</param>
        public AnimationFrame(Rectangle SourceRectangle, float scale = 1f)
        {
            this.sourceRectangle = SourceRectangle;
            this.frameCountUntilNextAnimation = -1;
            this.frameDuration = -1;
            this.scale= scale;
        }

        /// <summary>Construct an instance.</summary>
        /// <param name="SourceRectangle">The draw source for this animation.</param>
        /// <param name="existForXFrames">How many on screen frames this animation stays for. Every draw frame decrements an active animation by 1 frame. Set this to -1 to have it be on the screen infinitely.</param>
        public AnimationFrame(Rectangle SourceRectangle, int existForXFrames, float scale=1f)
        {
            this.sourceRectangle = SourceRectangle;
            this.frameDuration = existForXFrames;
            this.frameCountUntilNextAnimation = this.frameDuration;
            this.scale= scale;
        }

        /// <summary>Decrements the amount of frames this animation is on the screen for by 1.</summary>
        public void tickAnimationFrame()
        {
            if (this.frameDuration == -1) return;
            this.frameCountUntilNextAnimation--;
        }

        /// <summary>This sets the animation frame count to be the max duration. I.E restart the timer.</summary>
        public void startAnimation()
        {
            this.frameCountUntilNextAnimation = this.frameDuration;
        }
        public bool isFinished()
        {
            if (this.frameDuration == -1) return true;
            return this.frameCountUntilNextAnimation == 0;
        }

        /// <summary>
        /// Resets the animation frame.
        /// </summary>
        public void reset()
        {
            this.frameCountUntilNextAnimation = this.frameDuration;
        }

        public virtual AnimationFrame Copy()
        {
            return new AnimationFrame(this.sourceRectangle.X, this.sourceRectangle.Y, this.sourceRectangle.Width,this.sourceRectangle.Height, this.frameDuration, this.scale);
        }

        public virtual AnimationFrame readAnimationFrame(BinaryReader reader)
        {
            this.sourceRectangle = reader.ReadRectangle();
            this.frameDuration = reader.ReadInt32();
            this.frameCountUntilNextAnimation = reader.ReadInt32();
            this.scale= reader.ReadSingle();
            return this;
        }

        public virtual void writeAnimationFrame(BinaryWriter writer)
        {
            writer.WriteRectangle(this.sourceRectangle);
            writer.Write(this.frameDuration);
            writer.Write(this.frameCountUntilNextAnimation);
            writer.Write(this.scale);
        }
    }
}
