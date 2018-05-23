using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Bath
{
    class Father
    {
        Texture2D[] fatherTex = new Texture2D[2];
        Vector2 position;
        int width, height;
        int speed;
        Rectangle collisionBox;
        float scale;
        float time;
        int currentFrame = 0;

        public Father(int positionX, Vector2 res, float scale, Texture2D fatherTex, Texture2D fatherTex2, int lvl)
        {
            this.scale = scale;
            position.X = positionX;
            width = (int)res.X / 8;
            height = width * fatherTex.Height / fatherTex.Width;
            position.Y = -height;
            this.fatherTex[0] = fatherTex;
            this.fatherTex[1] = fatherTex2;
            speed = (int)(res.Y / (1.9f-lvl*0.1));
        }

       

        public void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(time > 0.15f)
            {
                time = 0;
                if (currentFrame == 0)
                    currentFrame = 1;
                else
                    currentFrame = 0;
            }

            collisionBox.X = (int)position.X + width / 4;
            collisionBox.Width = width/2;
            collisionBox.Y = (int)position.Y + height / 6;
            collisionBox.Height = 2 * height / 3;

            position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(fatherTex[currentFrame], position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public int GetX()
        {
            return (int)position.X;
        }
        
        public Rectangle getRect()
        {
            return collisionBox;
        }
    }
}
