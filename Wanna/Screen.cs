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
    abstract class Screen
    {
        public virtual void LoadContent(ContentManager Content)
        {
           
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public abstract int ScreenChange();
        
            
        
        

        

        public virtual void Reset(int lvl)
        {

        }
        
    }
}
