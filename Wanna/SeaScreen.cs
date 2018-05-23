using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;


namespace Bath
{
    class SeaScreen : Screen
    {
        Bath bath;
        List<Father> listOfFathers = new List<Father>();
        Texture2D seaTex;
        Texture2D fatherTex;
        Texture2D fatherTex2;
        Vector2 res;
        float scale;
        float time;
        float fireDaddy;
        Random rand;
        SoundEffect themeMusic;
        SoundEffect[] ohhdaddy = new SoundEffect[2];
        SoundEffect[] daddy = new SoundEffect[2];
        SoundEffectInstance themeInstance;
        static int licznik = 0;
        int level = 0;

        public SeaScreen(Vector2 res)
        {
            this.res = res;
            bath = new Bath(res);
            rand = new Random();
            fireDaddy = rand.Next(2, 4);
        }

        public override void LoadContent(ContentManager Content)
        {
            seaTex = Content.Load<Texture2D>("background");
            fatherTex = Content.Load<Texture2D>("flyingFatherS");
            fatherTex2 = Content.Load<Texture2D>("flyingFather2S");
            scale = res.Y / seaTex.Height;
            bath.LoadContent(Content);
            themeMusic = Content.Load<SoundEffect>("music");
            ohhdaddy[0] = Content.Load<SoundEffect>("ohhdaddy");
            ohhdaddy[1] = Content.Load<SoundEffect>("ohhdaddy2");
            daddy[0] = Content.Load<SoundEffect>("daddy1");
            daddy[1] = Content.Load<SoundEffect>("daddy3");
            themeInstance = themeMusic.CreateInstance();
            themeInstance.IsLooped = true;
            themeInstance.Play();
            themeInstance.Pause();
        }

        public override void Update(GameTime gameTime)
        {
            fireDaddy -= (float)gameTime.ElapsedGameTime.TotalSeconds; 
            if(fireDaddy < 0)
            {
                licznik++;
                daddy[licznik % 2].Play();
                fireDaddy = rand.Next(2, 4);
            }
            themeInstance.Resume();
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(time > (0.66 - (level*0.04)))
            {
                time = 0;
                listOfFathers.Add(new Father((int)(rand.Next(0,8) * res.X) / 8, res, scale, fatherTex, fatherTex2,level));
            }
            bath.Update(gameTime);
            for (int i = 0; i < listOfFathers.Count(); i++)
            {
                listOfFathers[i].Update(gameTime);
                if (listOfFathers[i].GetX() > res.X)
                    listOfFathers.Remove(listOfFathers[i]);

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(seaTex, Vector2.Zero, null,
                Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            bath.Draw(spriteBatch, listOfFathers);
            

        }

        public override int ScreenChange()
        {
            if (bath.CheckCollision(listOfFathers))
            {
                ohhdaddy[rand.Next(0, 2)].Play();
                themeInstance.Pause();
                return 1;
            }
            else
                return 0;
        }

        public override void Reset(int lvl)
        {
            this.level = lvl;
            listOfFathers.Clear();
            time = 0;
            bath.Reset();

        }
    }
}
