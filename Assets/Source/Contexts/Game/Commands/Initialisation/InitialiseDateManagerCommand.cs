using Assets.Source.Core.IoC;
using Assets.Source.Model;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.Initialisation
{
    #region Signals
    public class InitialiseDateManagerSignal : Signal { }
    #endregion

    public class InitialiseDateManagerCommand : Command
    {
        [Inject]
        public IDateManager DateManager { get; set; }

        public override void Execute()
        {
            var date = injectionBinder.GetInstance<IDate>(CustomContextKeys.NewInstance);
            date.Initialise(1, 1, 1);
            DateManager[date.Day, date.Month, date.Year] = date;
            DateManager.CurrentDate = date;
        }
    }
}