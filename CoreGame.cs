using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Test1;

public class CoreGame : IScene
{
    private GraphicsDeviceManager graphicsDeviceManager;
    private ContentManager contentManager;
    private SceneManager sceneManager;

    SpriteFont font;
    Texture2D targetTexture;
    Texture2D gameBackgorundTexture;

    Vector2 targetPosition = Vector2.Zero;
    string scoreText = "Wynik:";
    Vector2 textSize;
    const int TARGET_RAFIUS = 50;

    MouseState mState;
    bool mReleased = true;
    int score = 0;
    int timeText = 60;
    float timeFollow = 0;

    Random rand = new Random();

    public CoreGame(GraphicsDeviceManager graphicsDeviceManager, ContentManager contentManager, SceneManager sceneManager)
    {
        this.graphicsDeviceManager = graphicsDeviceManager;
        this.contentManager = contentManager;
        this.sceneManager = sceneManager;
    }

    public void Load()
    {
        font = contentManager.Load<SpriteFont>("Fonts/font1");

        targetPosition.X = rand.Next(TARGET_RAFIUS, graphicsDeviceManager.PreferredBackBufferWidth - TARGET_RAFIUS);
        targetPosition.Y = rand.Next(TARGET_RAFIUS, graphicsDeviceManager.PreferredBackBufferHeight - TARGET_RAFIUS);

        targetTexture = contentManager.Load<Texture2D>("target");
        gameBackgorundTexture = contentManager.Load<Texture2D>("oobraz_1");
    }

    public void Update(GameTime gameTime)
    {
        textSize = font.MeasureString(scoreText + score);

        mState = Mouse.GetState();

        if (mState.LeftButton == ButtonState.Pressed && mReleased == true && timeText > 0)
        {
            float mouseTargetDist = Vector2.Distance(targetPosition, mState.Position.ToVector2());
            if (mouseTargetDist < TARGET_RAFIUS)
            {
                score++;
                targetPosition.X = rand.Next(TARGET_RAFIUS, graphicsDeviceManager.PreferredBackBufferWidth - TARGET_RAFIUS);
                targetPosition.Y = rand.Next(TARGET_RAFIUS, graphicsDeviceManager.PreferredBackBufferHeight - TARGET_RAFIUS);
            }
            mReleased = false;
        }
        else if (mState.LeftButton == ButtonState.Released && mReleased == false)
        {
            mReleased = true;
        }

        timeFollow += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timeFollow >= 1)
        {
            timeText--;
            timeFollow = 0f;
            if (timeText < 0)
            {
                timeText = 0;
            }
        }

        if (timeText <= 0)
        {
            // Przełącz na ekran końcowy
            sceneManager.AddScene(new GameOverScene(graphicsDeviceManager, contentManager, sceneManager, score));
            return;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            gameBackgorundTexture,
            new Rectangle(0, 0, graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight),
            Color.White
        );

        spriteBatch.Draw(
            targetTexture,
            targetPosition,
            null,
            Color.White,
            0f,
            new Vector2(targetTexture.Width / 2, targetTexture.Height / 2),
            Vector2.One,
            SpriteEffects.None,
            0.5f
        );

        Vector2 textPosition = new Vector2(
            graphicsDeviceManager.PreferredBackBufferWidth / 2, // Szerokość okna / 2
            30 // Wysokość okna / 2
        );
        Vector2 textOrigin = textSize / 2;
        float textScale = 1f + (score / 100f);
        spriteBatch.DrawString(
            font,
            scoreText + score,
            textPosition,
            Color.White,
            0f,
            textOrigin,
            textScale,
            SpriteEffects.None,
            0f
        );

        Vector2 timeTextPosition = new Vector2(textPosition.X, textPosition.Y + 30);
        Vector2 timeTextOrigin = textSize / 2;
        float timeTextScale = 1f + (score / 500f);
        spriteBatch.DrawString(
            font,
            "Czas:" + timeText,
            timeTextPosition,
            Color.White,
            0f,
            timeTextOrigin,
            timeTextScale * 0.5f,
            SpriteEffects.None,
            0.1f
        );
    }
}