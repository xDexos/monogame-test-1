using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Test1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D ballTexture;
    Vector2 ballPosition;
    float ballSpeed;

    SpriteFont font;

    Texture2D roomBackground;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
        ballSpeed = 100f;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        ballTexture = Content.Load<Texture2D>("ball");

        font = Content.Load<SpriteFont>("Fonts/font1");

        roomBackground = Content.Load<Texture2D>("room_1");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        float updateBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Pobieranie sygnałów wejścia
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.Up))
        {
            ballPosition.Y -= updateBallSpeed;
        }

        if (kstate.IsKeyDown(Keys.Down))
        {
            ballPosition.Y += updateBallSpeed;
        }

        if (kstate.IsKeyDown(Keys.Left))
        {
            ballPosition.X -= updateBallSpeed;
        }

        if (kstate.IsKeyDown(Keys.Right))
        {
            ballPosition.X += updateBallSpeed;
        }

        // Maksymalny zakres poruszanie się, ograniczenie pozycji
        if (ballPosition.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 2)
        {
            ballPosition.X = _graphics.PreferredBackBufferWidth - ballTexture.Width / 2;
        }
        else if (ballPosition.X < ballTexture.Width / 2)
        {
            ballPosition.X = ballTexture.Width / 2;
        }

        if (ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2)
        {
            ballPosition.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 2;
        }
        else if (ballPosition.Y < ballTexture.Height / 2)
        {
            ballPosition.Y = ballTexture.Height / 2;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin(
            SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp,
            DepthStencilState.None,
            RasterizerState.CullCounterClockwise
        );

        _spriteBatch.Draw(
            roomBackground,
            new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight),
            Color.White
        );

        _spriteBatch.Draw(
            ballTexture, // zaladowana textura
            ballPosition, // pozycja
            null, // rectangle?
            Color.White, // kolor
            0f, // rotacja
            new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), // punkt origin
            Vector2.One, // skala
            SpriteEffects.None, // efekt
            0f); // warstwa


        Vector2 textPosition = new Vector2(
            _graphics.PreferredBackBufferWidth / 2, // Szerokość okna / 2
            25 // Wysokość okna / 2
        );
        Vector2 textSize = font.MeasureString("Hello World");
        Vector2 textOrigin = textSize / 2;
        _spriteBatch.DrawString(
            font,
            "Hello World",
            textPosition,
            Color.White,
            0f,
            textOrigin,
            1f,
            SpriteEffects.None,
            0.5f);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
