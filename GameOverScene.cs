using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Test1;

public class GameOverScene : IScene
{
    private GraphicsDeviceManager graphicsDeviceManager;
    private ContentManager contentManager;
    private SceneManager sceneManager;

    private SpriteFont font;
    private string finalScoreText;
    private Texture2D buttonTexture;
    private Vector2 buttonPosition;
    private Rectangle buttonRectangle;
    private Color buttonColor = Color.White;

    private MouseState mouseState;
    private bool mouseReleased = true;
    Texture2D gameOverBackgroundTexture;

    public GameOverScene(GraphicsDeviceManager graphicsDeviceManager, ContentManager contentManager, SceneManager sceneManager, int finalScore)
    {
        this.graphicsDeviceManager = graphicsDeviceManager;
        this.contentManager = contentManager;
        this.sceneManager = sceneManager;

        this.finalScoreText = $"Twoj wynik: {finalScore}";
    }

    public void Load()
    {
        font = contentManager.Load<SpriteFont>("Fonts/font1");
        buttonTexture = contentManager.Load<Texture2D>("startButton");
        gameOverBackgroundTexture = contentManager.Load<Texture2D>("N_concept_1");

        // Ustawienie pozycji przycisku restartu
        buttonPosition = new Vector2(
            graphicsDeviceManager.PreferredBackBufferWidth / 2 - buttonTexture.Width / 2,
            graphicsDeviceManager.PreferredBackBufferHeight / 2 + 50
        );

        buttonRectangle = new Rectangle((int)buttonPosition.X, (int)buttonPosition.Y, buttonTexture.Width, buttonTexture.Height);
    }

    public void Update(GameTime gameTime)
    {
        mouseState = Mouse.GetState();
        Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

        // Sprawdź, czy mysz najeżdża na przycisk
        if (mouseRectangle.Intersects(buttonRectangle))
        {
            buttonColor = Color.Gray;

            if (mouseState.LeftButton == ButtonState.Pressed && mouseReleased)
            {
                mouseReleased = false;
                // Restart gry
                sceneManager.AddScene(new CoreGame(graphicsDeviceManager, contentManager, sceneManager));
            }
            else if (mouseState.LeftButton == ButtonState.Released)
            {
                mouseReleased = true;
            }
        }
        else
        {
            buttonColor = Color.White;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(gameOverBackgroundTexture, new Rectangle(0, 0, graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight), Color.White);

        // Wyświetlenie wyniku
        Vector2 scorePosition = new Vector2(
            graphicsDeviceManager.PreferredBackBufferWidth / 2,
            graphicsDeviceManager.PreferredBackBufferHeight / 2 - 50
        );

        Vector2 scoreOrigin = font.MeasureString(finalScoreText) / 2;
        spriteBatch.DrawString(
            font,
            finalScoreText,
            scorePosition,
            Color.White,
            0f,
            scoreOrigin,
            1.5f,
            SpriteEffects.None,
            0f
        );

        // Rysowanie przycisku
        spriteBatch.Draw(
            buttonTexture,
            buttonPosition,
            buttonColor
        );

        // Dodanie napisu na przycisku
        string buttonText = "Restart";
        Vector2 buttonTextSize = font.MeasureString(buttonText);
        Vector2 buttonTextPosition = buttonPosition + new Vector2(buttonTexture.Width / 2, buttonTexture.Height / 2);

        Vector2 buttonTextOrigin = new Vector2(buttonTextSize.X / 2, buttonTextSize.Y / 2);
        spriteBatch.DrawString(
            font,
            buttonText,
            buttonTextPosition,
            Color.White,
            0f,
            buttonTextOrigin,
            0.8f,
            SpriteEffects.None,
            0f
        );
    }
}
