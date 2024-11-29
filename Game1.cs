using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogames;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D pixel;
    SpriteFont fontscore;

    Paddle paddleleft;
    Paddle paddleright;
    Ball ball;
   
    float velocityX = 1;
    float velocityY = 1;
    int ScoreRightPlayer = 0;
    int ScoreLeftPlayer = 0;

    KeyboardState oldKState;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);


        // TODO: use this.Content to load your game content here
        pixel = Content.Load<Texture2D>(assetName:"pixel");
        fontscore = Content.Load<SpriteFont>("score");
        ball = new Ball(pixel);
        paddleleft = new Paddle(pixel, new Rectangle(10,200,20,100), Keys.W, Keys.S);
        paddleright = new Paddle(pixel, new Rectangle(770,200,20,100), Keys.Up, Keys.Down);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        paddleleft.Update();
        paddleright.Update();
        // TODO: Add your update logic here
         KeyboardState kState = Keyboard.GetState();
        base.Update(gameTime);
        

   
        if(paddleleft.Rectangle.Intersects(ball.Rectangle) || paddleright.Rectangle.Intersects(ball.Rectangle)){
            ball.Bounce();
        }

        if(ball.Rectangle.Y <= 0 || ball.Rectangle.Y + ball.Rectangle.Height>= 480){
            velocityY *= -1.1f;
        }
        

        ball.Update();
        if(ball.Rectangle.X <= 0 ){
            ball.Reset();
            ScoreRightPlayer++;
        }
        if(ball.Rectangle.X + ball.Rectangle.Width >= 800){
            ball.Reset();
            ScoreLeftPlayer++;
        }
        if(ScoreLeftPlayer>=10 || ScoreRightPlayer>=10){
            ScoreLeftPlayer=0;
            ScoreRightPlayer=0;
        }
       
        if(kState.IsKeyDown(key:Keys.Q) && oldKState.IsKeyUp(Keys.Q)){
            ScoreLeftPlayer++;
            ball.Reset();
        }
        if(kState.IsKeyDown(key:Keys.R)&& oldKState.IsKeyUp(Keys.R)){
            ball.Reset();
            ScoreLeftPlayer=0;
            ScoreRightPlayer=0;
        }
        
        oldKState = kState;
    }     

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.DrawString(fontscore,ScoreLeftPlayer.ToString(),new Vector2(10,10), Color.Purple);
        _spriteBatch.DrawString(fontscore,ScoreRightPlayer.ToString(),new Vector2(720,10), Color.Purple);
        ball.Draw(_spriteBatch);
        paddleleft.Draw(_spriteBatch);
        paddleright.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
