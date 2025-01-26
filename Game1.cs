using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Test1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SceneManager sceneManager;

    // SpriteFont font;
    // Texture2D targetTexture;
    // Texture2D gameBackgorundTexture;

    // Vector2 targetPosition = Vector2.Zero;
    // string scoreText = "Wynik:";
    // Vector2 textSize;
    // const int TARGET_RAFIUS = 50;

    // MouseState mState;
    // bool mReleased = true;
    // int score = 0;
    // int timeText = 60;
    // float timeFollow = 0;

    // Random rand = new Random();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        sceneManager = new();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        // targetPosition.X = rand.Next(TARGET_RAFIUS, _graphics.PreferredBackBufferWidth - TARGET_RAFIUS);
        // targetPosition.Y = rand.Next(TARGET_RAFIUS, _graphics.PreferredBackBufferHeight - TARGET_RAFIUS);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        sceneManager.AddScene(new MainMenu(this, _graphics, Content, sceneManager));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        sceneManager.GetCurrentScene().Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin(
            samplerState: SamplerState.PointClamp
        );

        sceneManager.GetCurrentScene().Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
