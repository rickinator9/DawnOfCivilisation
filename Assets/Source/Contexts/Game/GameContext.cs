using Assets.Source.Contexts.Commands.Army;
using Assets.Source.Contexts.Mediators;
using Assets.Source.Contexts.Mediators.UI;
using Assets.Source.Contexts.Model;
using Assets.Source.Contexts.Signals.Armies;
using Assets.Source.Contexts.Views;
using Assets.Source.Contexts.Views.UI;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game
{
    public class GameContext : MVCSContext
    {
        public GameContext() { }

        public GameContext(MonoBehaviour view, bool autoStartup) : base(view, autoStartup) { }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        protected override void mapBindings()
        {
            commandBinder.Bind<CreateArmySignal>().To<CreateArmyCommand>();

            injectionBinder.Bind<IArmy>().To<Army>();
            injectionBinder.Bind<OnCreateArmySignal>().ToSingleton();

            mediationBinder.Bind<ArmyView>().To<ArmyMediator>();
            mediationBinder.Bind<ArmiesView>().To<ArmiesMediator>();
            mediationBinder.Bind<HexPanelView>().To<HexPanelMediator>();
        }
    }
}