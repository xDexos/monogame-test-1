using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Test1;

public class MainMenu : IScene
{
    private Game1 game1;
    private GraphicsDeviceManager graphicsDeviceManager;
    private ContentManager contentManager;
    private SceneManager sceneManager;

    Texture2D mainMenuBackgroundTexture;

    StartButton startButton;
    Texture2D startButtonTexture;
    ExitButton exitButton;

    SpriteFont font;
    const string GAME_NAME = "Karp'O'Killer";

    public MainMenu(Game1 game1, GraphicsDeviceManager graphicsDeviceManager, ContentManager contentManager, SceneManager sceneManager)
    {
        this.game1 = game1;
        this.graphicsDeviceManager = graphicsDeviceManager;
        this.contentManager = contentManager;
        this.sceneManager = sceneManager;
    }

    public void Load()
    {
        font = contentManager.Load<SpriteFont>("Fonts/font1");
        mainMenuBackgroundTexture = contentManager.Load<Texture2D>("N_concept_1");
        startButtonTexture = contentManager.Load<Texture2D>("startButton");
        Vector2 startButtonPosition = new Vector2(graphicsDeviceManager.PreferredBackBufferWidth / 2, (graphicsDeviceManager.PreferredBackBufferHeight / 2) - 20);
        startButton = new(game1, graphicsDeviceManager, contentManager, sceneManager, startButtonTexture, startButtonPosition, font, "Start");
        Vector2 exitButtonPosition = new Vector2(graphicsDeviceManager.PreferredBackBufferWidth / 2, (graphicsDeviceManager.PreferredBackBufferHeight / 2) + (startButtonTexture.Height) - 10);
        exitButton = new(game1, graphicsDeviceManager, contentManager, sceneManager, startButtonTexture, exitButtonPosition, font, "Exit");
    }

    public void Update(GameTime gameTime)
    {
        startButton.Update(gameTime);
        exitButton.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(mainMenuBackgroundTexture, new Rectangle(0, 0, graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight), Color.White);

        Vector2 gameNamePosition = new Vector2(graphicsDeviceManager.PreferredBackBufferWidth / 2, (graphicsDeviceManager.PreferredBackBufferHeight / 2) - 100);
        Vector2 gameNameSize = font.MeasureString(GAME_NAME);
        Vector2 gameNameOrigin = new Vector2(gameNameSize.X / 2, gameNameSize.Y / 2);
        spriteBatch.DrawString(
            font,
            GAME_NAME,
            gameNamePosition,
            Color.White,
            0f,
            gameNameOrigin,
            new Vector2(1.3f, 1.3f),
            SpriteEffects.None,
            0f
        );

        startButton.Draw(spriteBatch);
        exitButton.Draw(spriteBatch);

    }
}