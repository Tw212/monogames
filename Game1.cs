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
    Rectangle ball = new Rectangle(390,230,20,20);
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
        ball.Y += (int)velocityY;
        ball.X += (int)velocityX;
        if(ball.Intersects(paddleright) || ball.Intersects(paddleleft)){
            velocityX *= -1.1f;
            velocityY *= 1.1f;
        }
        ball.X += (int)velocityX;
        if(ball.Intersects(paddleright) || ball.Intersects(paddleleft)){
            velocityY *= -1.1f;
            
        }

        if(ball.Y <= 0 || ball.Y + ball.Height>= 480){
            velocityY *= -1.1f;
        }
        if(ball.X <= 0 || ball.X + ball.Width >= 800){
            ball.X=390;
            ball.Y=230;
            velocityX = 2;
            velocityY = 2;
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
        _spriteBatch.Draw(pixel,ball,Color.LightGoldenrodYellow);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
