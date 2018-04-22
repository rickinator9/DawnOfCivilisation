using strange.extensions.mediation.impl;

namespace Assets.Source.Core.IoC
{
    public class DOCView : View, IDOCView
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
