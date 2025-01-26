using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Test1;

public class StartButton
{
    private Game1 game1;
    private GraphicsDeviceManager graphicsDeviceManager;
    private ContentManager contentManager;
    private SceneManager sceneManager;

    Texture2D buttonTexture;
    Vector2 buttonPosition;
    readonly Rectangle rectangle;
    Color color = Color.White;
    SpriteFont font;
    string buttonText;

    MouseState mState;
    bool mReleased = true;

    public StartButton(Game1 game1, GraphicsDeviceManager graphicsDeviceManager, ContentManager contentManager, SceneManager sceneManager, Texture2D buttonTexture, Vector2 buttonPosition, SpriteFont font, string buttonText)
    {
        this.game1 = game1;
        this.graphicsDeviceManager = graphicsDeviceManager;
        this.contentManager = contentManager;
        this.sceneManager = sceneManager;
        this.buttonTexture = buttonTexture;
        this.buttonPosition = buttonPosition;
        this.rectangle = new((int)buttonPosition.X - (buttonTexture.Width / 2), (int)buttonPosition.Y - (buttonTexture.Height / 2), buttonTexture.Width, buttonTexture.Height);
        this.font = font;
        this.buttonText = buttonText;
    }

    public void Update(GameTime gameTime)
    {
        mState = Mouse.GetState();
        Rectangle cursorRectangle = new((int)mState.X, (int)mState.Y, 1, 1);

        if (cursorRectangle.Intersects(rectangle))
        {
            color = Color.DarkGray;
            if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
            {
                mReleased = false;
                sceneManager.AddScene(new CoreGame(graphicsDeviceManager, contentManager, sceneManager));
            }
            if (mState.LeftButton == ButtonState.Released && mReleased == false)
            {
                mReleased = true;
            }
        }
        else
        {
            color = Color.White;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Vector2 buttonOrigin = new Vector2(buttonTexture.Width / 2, buttonTexture.Height / 2);
        spriteBatch.Draw(
            buttonTexture,
            buttonPosition,
            null,
            color,
            0f,
            buttonOrigin,
            Vector2.One,
            SpriteEffects.None,
            0f
        );

        Vector2 textSize = font.MeasureString(buttonText);
        Vector2 textPosition = new Vector2(buttonPosition.X + (textSize.X / 4), buttonPosition.Y + (textSize.Y / 2));
        spriteBatch.DrawString(
            font,
            buttonText,
            textPosition,
            Color.White,
            0f,
            buttonOrigin,
            Vector2.One,
            SpriteEffects.None,
            0f
        );
    }
}