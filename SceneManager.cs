using System.Collections.Generic;

namespace Test1;

public class SceneManager
{
    private readonly Stack<IScene> sceneStack;

    public SceneManager()
    {
        this.sceneStack = new();
    }

    public void AddScene(IScene scene)
    {
        scene.Load();
        this.sceneStack.Push(scene);
    }

    public void RemoveScene()
    {
        this.sceneStack.Pop();
    }

    public IScene GetCurrentScene()
    {
        return this.sceneStack.Peek();
    }
}