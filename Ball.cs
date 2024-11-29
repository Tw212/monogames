using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogames
{
    public class Ball
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle = new Rectangle(390,230,20,20);
        private float velocityX = 1;
        private float velocityY = 1;
        public Rectangle Rectangle{
            get{return rectangle;}
        }
        public Ball(Texture2D t){
            texture = t;
            position = new Vector2();
            position.X = rectangle.X;
            position.Y = rectangle.Y;
        }

        public void Reset() {
            position.X = 390;
            position.Y = 230;
            velocityX = 2;
            velocityY = 2;
        }

        public void Bounce(){
            velocityX *= -1.1f;
        }
        public void Update(){
            position.X += velocityX;
            position.Y += velocityY;

            rectangle.X = (int) position.X;
            rectangle.Y = (int) position.Y;
            rectangle.Y += (int)velocityY;
            rectangle.X += (int)velocityX;
        
        if(Rectangle.Y <= 0 || Rectangle.Y + Rectangle.Height>= 480){
            velocityY *= -1.1f;

        }
        }
        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture,Rectangle,Color.LightGoldenrodYellow);
        }
    }
}