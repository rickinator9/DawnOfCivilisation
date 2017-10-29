using strange.extensions.mediation.impl;

namespace Assets.Source.Contexts.Game.UI.Typed
{
    public abstract class TypedUiView<T> : View
    {
        public abstract void UpdateValues(T obj);

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void ShowForObject(T obj)
        {
            UpdateValues(obj);

            Show();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}