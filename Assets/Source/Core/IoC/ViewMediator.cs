using strange.extensions.mediation.impl;

namespace Assets.Source.Core.IoC
{
    public class ViewMediator<T> : Mediator where T : View
    {
         [Inject]
         public T View { get; set; }
    }
}