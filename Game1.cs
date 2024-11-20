using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogames;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D pixel;

    Rectangle paddleleft = new Rectangle(10,200,20,100);
    Rectangle paddleright = new Rectangle(770,200,20,100);
    Rectangle ball = new Rectangle(390,230,20,20);
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
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        KeyboardState kState= Keyboard.GetState();
        if(kState.IsKeyDown(key:Keys.W)){
            paddleleft.Y-= 10;
        }
        if(kState.IsKeyDown(key:Keys.S)){
            paddleleft.Y+= 10;
        }
        if(kState.IsKeyDown(key:Keys.Up)){
            paddleright.Y-= 10;
        }
        if(kState.IsKeyDown(key:Keys.Down)){
            paddleright.Y+= 10;
        }
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(pixel,paddleleft,Color.HotPink);
        _spriteBatch.Draw(pixel,paddleright,Color.Blue);
        _spriteBatch.Draw(pixel,ball,Color.LightGoldenrodYellow);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
