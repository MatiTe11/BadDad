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
    class ScreenManager
    {
        Vector2 resolution;
        Vector2 scorePosition;
        Vector2 levelPosition;
        List<Screen> screens = new List<Screen>();
        int currentScreen;
        int prevScreen;
        SpriteFont font;
        float time = 0;
        string scoreString = " ";
        string levelString;
        int score, level;
        float scale;
        


        public ScreenManager(GraphicsDeviceManager graphics)
        {
            
            currentScreen = 3;
            resolution.X = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            resolution.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            scale = resolution.X / 1920;
            graphics.PreferredBackBufferHeight = (int)resolution.Y;
            graphics.PreferredBackBufferWidth = (int)resolution.X;
            graphics.IsFullScreen = true;


            screens.Add(new SeaScreen(resolution));
            screens.Add(new BattleScreen(resolution));
            screens.Add(new OverScreen(resolution));
            screens.Add(new StartScreen(resolution));

            score = 0;
            level = 1;

            
        }

        public void LoadContent(ContentManager Content)
        {
            foreach(Screen s in screens)
            {
                s.LoadContent(Content);
            }

            font = Content.Load<SpriteFont>("Score");

            scorePosition = new Vector2(resolution.X * 0.044f);
            levelPosition = new Vector2(scorePosition.X, scorePosition.Y + font.MeasureString(scoreString).Y);
        }


        public void Update(GameTime gameTime)
        {
            int screenChngeVar;

            if (currentScreen == 0)
            {
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            
            score = (int)time / 2;
            
            scoreString = "Score: " + score;
            levelString = "Level: " + level;

            screens[currentScreen].Update(gameTime);

            screenChngeVar = screens[currentScreen].ScreenChange();

            if(screenChngeVar != currentScreen)
            {
                prevScreen = currentScreen;
                currentScreen = screenChngeVar;
                if (prevScreen == 2 && currentScreen == 0)
                {
                    level = 1;
                    time = 0;
                }
                else if(prevScreen == 1 &&  currentScreen == 0 && level < 8)
                {
                    level++;
                }
                screens[prevScreen].Reset(level);
                screens[currentScreen].Reset(level);
                
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            screens[currentScreen].Draw(spriteBatch);
            if (currentScreen == 0 || currentScreen == 1 || currentScreen == 2)
            {
                spriteBatch.Begin();
                //spriteBatch.DrawString(font, scoreString, new Vector2(100, 100), Color.Black);
                //spriteBatch.DrawString(font, levelString, new Vector2(100, 200), Color.Red);
                spriteBatch.DrawString(font, scoreString, scorePosition, Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, levelString, levelPosition, Color.Red, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                spriteBatch.End();
            }
        }
    }
}
