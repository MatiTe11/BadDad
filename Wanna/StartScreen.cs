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
    class StartScreen : Screen
    {
        Vector2 res;
        Texture2D startTex;
        float scale;
        KeyboardState keyboardState;

        public StartScreen(Vector2 res)
        {
            this.res = res;
            scale = res.X / 1920;
        }

        public override void LoadContent(ContentManager Content)
        {
            startTex = Content.Load<Texture2D>("Startscreen");
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(startTex, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }


        public override int ScreenChange()
        {
            if (keyboardState.IsKeyDown(Keys.Space))
                return 0;
            else
                return 3;
        }
    }
}
