using Assets.Source.Contexts.Game.Commands.Army;
using Assets.Source.Contexts.Game.Commands.Initialisation;
using Assets.Source.Contexts.Game.Commands.Input;
using Assets.Source.Contexts.Game.Mediators;
using Assets.Source.Contexts.Game.Mediators.UI;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Signals.Armies;
using Assets.Source.Contexts.Game.Signals.Initialisation;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Contexts.Game.Views.UI;
using Assets.Source.Core.IoC;
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

        public override void Launch()
        {
            base.Launch();

            injectionBinder.GetInstance<InitialiseHexMapSignal>().Dispatch(new HexMapDimension
            {
                Width = 20,
                Height = 20
            });
        }

        protected override void mapBindings()
        {
            commandBinder.Bind<CreateArmySignal>().To<CreateArmyCommand>().Pooled();
            commandBinder.Bind<LeftMouseClickSignal>().To<LeftMouseClickCommand>().Pooled();
            commandBinder.Bind<RightMouseClickSignal>().To<RightMouseClickCommand>().Pooled();
            commandBinder.Bind<InitialiseHexMapSignal>().To<InitialiseHexMapCommand>();

            injectionBinder.Bind<IArmy>().To<Army>().ToName(CustomContextKeys.NewInstance);
            injectionBinder.Bind<IArmies>().To<Armies>().ToSingleton();
            injectionBinder.Bind<OnCreateArmySignal>().ToSingleton();

            injectionBinder.Bind<IHexMap>().To<HexMap>().ToName(CustomContextKeys.NewInstance);
            injectionBinder.Bind<OnInitialiseHexMapSignal>().ToSingleton();

            mediationBinder.Bind<ArmyView>().To<ArmyMediator>();
            mediationBinder.Bind<ArmiesView>().To<ArmiesMediator>();
            mediationBinder.Bind<InputView>().To<InputMediator>();

            mediationBinder.Bind<HexPanelView>().To<HexPanelMediator>();
            mediationBinder.Bind<HexGridView>().To<HexGridMediator>();
        }
    }
}