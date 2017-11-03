using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Views
{
    public class UpdateView : View
    {
        public Signal OnUpdate = new Signal();

        void Update()
        {
            OnUpdate.Dispatch();
        }
    }
}