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

    Rectangle paddleleft = new Rectangle(10,200,20,100);
    Rectangle paddleright = new Rectangle(770,200,20,100);

    Ball ball;
   
    float velocityX = 1;
    float velocityY = 1;
    int ScoreRightPlayer = 0;
    int ScoreLeftPlayer = 0;
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
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        KeyboardState kState= Keyboard.GetState();
        if(kState.IsKeyDown(key:Keys.W) && paddleleft.Y > 0){
            paddleleft.Y-= 10;
        }
        if(kState.IsKeyDown(key:Keys.S) && paddleleft.Y + paddleleft.Height < 480){
            paddleleft.Y+= 10;
        }
        if(kState.IsKeyDown(key:Keys.Up) && paddleright.Y > 0){
            paddleright.Y-= 10;
        }
        if(kState.IsKeyDown(key:Keys.Down) && paddleright.Y + paddleright.Height< 480){
            paddleright.Y+= 10;
        }
        // TODO: Add your update logic here

        base.Update(gameTime);
        if(ball.Rectangle.Intersects(paddleright) || ball.Rectangle.Intersects(paddleleft)){
            velocityX *= -1.1f;
            velocityY *= 1.1f;
        }
       
        if(ball.Rectangle.Intersects(paddleright) || ball.Rectangle.Intersects(paddleleft)){
            velocityY *= -1.1f;
            
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
        if(kState.IsKeyDown(key:Keys.Q)){
            ScoreLeftPlayer+=1;
            ball.Reset();
        }
    }     

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.DrawString(fontscore,ScoreLeftPlayer.ToString(),new Vector2(10,10), Color.Black);
        _spriteBatch.DrawString(fontscore,ScoreRightPlayer.ToString(),new Vector2(720,10), Color.BlueViolet);
        _spriteBatch.Draw(pixel,paddleleft,Color.HotPink);
        _spriteBatch.Draw(pixel,paddleright,Color.Blue);
        ball.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
