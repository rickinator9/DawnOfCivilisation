using strange.extensions.context.impl;

namespace Assets.Source.Contexts.Game
{
    public class GameRoot : ContextView
    {
        void Awake()
        {
            var gameContext = new GameContext(this, true);
            gameContext.Start();
        }
    }
}