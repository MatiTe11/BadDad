using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;


namespace Bath
{
    class Bath
    {
        Vector2 res;
        Vector2 position;
        Vector2 wave1, wave2, wave3;
        float delta = 0;
        int waveSpeed;
        //Vector2 positionDraw;
        int width, height;
        int speed;
        float angle;
        Texture2D bathTex1, bathTex2;
        Texture2D water;
        float scale;
        KeyboardState keyboardState;
        float a, b, c;
        float la, lb;

        public Bath(Vector2 res)
        {
            this.res = res;
            position.X = res.X / 2;
            speed = (int)(res.X / 4);
            a = -res.Y/(res.X*res.X);
            b = res.Y / res.X;
            c = res.Y/2;

            waveSpeed = (int)res.X / 30;
            
            
        }

        public void LoadContent(ContentManager Content)
        {
            bathTex1 = Content.Load<Texture2D>("bathwithbaby");
            bathTex2 = Content.Load<Texture2D>("bathtube3");
            water = Content.Load<Texture2D>("water");
            scale = (res.X / 5) / bathTex1.Width;
            width = (int)(bathTex1.Width * scale);
            height = (int)(bathTex1.Height * scale);

        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            // chodzenie lewo-prawo
            if (keyboardState.IsKeyDown(Keys.Right))
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (keyboardState.IsKeyDown(Keys.Left))
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(position.X < width/2 )
            {
                position.X = width/2;
            }
            else if(position.X + width/2> res.X)
            {
                position.X = res.X - width/2;
            }
            position.Y = a * position.X * position.X + b * position.X + c;
            //positionDraw.X = position.X - width/2;
            //positionDraw.Y = position.Y - height/2;
            la = (2 * a * position.X + b);
            angle = (float)Math.Atan( la );
            lb = position.Y - (la) * position.X;
            lb -= height / 3; 

            wave1 = position;
            delta +=  waveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (delta > (res.Y /45) || delta < -(res.Y /45))
                waveSpeed = waveSpeed * -1;
            
            wave2 = new Vector2(position.X + (float)Math.Cos(angle) * res.X / 2, position.Y + (float)Math.Sin(angle) * res.X / 2);
            //wave2 = new Vector2(position.X + res.X, position.Y);

            wave3 = new Vector2(position.X - (float)Math.Cos(angle) * res.X / 2, position.Y - (float)Math.Sin(angle) * res.X / 2);


        }

        public void Draw(SpriteBatch spriteBatch, List<Father> listOfFathers)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bathTex1, position, null, Color.White, angle, new Vector2(bathTex2.Width / 2, bathTex2.Height / 2), scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(bathTex2, position, null, Color.White, angle, new Vector2(bathTex2.Width / 2, bathTex2.Height / 2), scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            foreach (Father f in listOfFathers)
            {
                f.Draw(spriteBatch);
            }
            spriteBatch.Begin();
            spriteBatch.Draw(water, new Vector2(wave1.X,wave1.Y + delta), null, Color.White, angle, new Vector2(water.Width / 2, 0), scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(water, new Vector2(wave2.X, wave2.Y + delta), null, Color.White, angle, new Vector2(0, 0), scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(water, new Vector2(wave3.X, wave3.Y + delta), null, Color.White, angle, new Vector2(water.Width, 0), scale, SpriteEffects.None, 0f);

            spriteBatch.End();
        }

        public void WaterMovement()
        {
            
            wave1 = position;
            wave2 = new Vector2(position.X + (float)Math.Cos(angle) * res.X / 2, position.Y + (float)Math.Sin(angle) * res.X / 2);
            wave3 = new Vector2(position.X - (float)Math.Cos(angle) * res.X / 2, position.Y - (float)Math.Sin(angle) * res.X / 2);
        }

        public bool CheckCollision(List<Father> listOfFathers)
        {

            bool collision = false; ;
            foreach (Father f in listOfFathers)
            {

                if (f.getRect().Bottom > la * f.getRect().Center.X + lb && f.getRect().Top < la * f.getRect().Center.X + lb && f.getRect().Left < position.X + width / 2 && f.getRect().Right > position.X - width / 2)
                {
                    collision = true;
                    break;
                }                 
            }
            return collision;
        }

        public void Reset()
        {
            position.X = res.X / 2;
        }
    }




}

