using Assets.Source.Contexts.Game.Commands;
using Assets.Source.Contexts.Game.Commands.Army;
using Assets.Source.Contexts.Game.Commands.City;
using Assets.Source.Contexts.Game.Commands.Initialisation;
using Assets.Source.Contexts.Game.Commands.Input;
using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Contexts.Game.Mediators;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Contexts.Game.Model.Pathfinding;
using Assets.Source.Contexts.Game.UI;
using Assets.Source.Contexts.Game.UI.Typed.Panels;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;
using Assets.Source.Model;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using UnityEngine;
using UnityEngine.EventSystems;

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
            commandBinder.Bind<CreateArmyMovementPathSignal>().To<CreateArmyMovementPathCommand>().Pooled();
            commandBinder.Bind<ProcessDateTickSignal>().To<ProcessDateTickCommand>();
            injectionBinder.Bind<TimePanelWaitSignal>().ToSingleton();
            injectionBinder.Bind<TimePanelResumeSignal>().ToSingleton();

            commandBinder.Bind<ShowUiPanelExclusivelySignal>().To<ShowUiPanelExclusivelyCommand>();
            injectionBinder.Bind<ShowUiPanelSignal>().ToSingleton();
            injectionBinder.Bind<HideAllUiPanelsSignal>().ToSingleton();

            injectionBinder.Bind<IArmy>().To<Army>().ToName(CustomContextKeys.NewInstance);
            injectionBinder.Bind<IArmies>().To<Armies>().ToSingleton();
            injectionBinder.Bind<IMovement>().To<Movement>().ToName(CustomContextKeys.NewInstance);
            injectionBinder.Bind<IMovementPath>().To<MovementPath>().ToName(CustomContextKeys.NewInstance);
            injectionBinder.Bind<OnCreateArmySignal>().ToSingleton();

            injectionBinder.Bind<IPlayers>().To<Players>().ToSingleton();
            injectionBinder.Bind<IPlayer>().To<Player>().ToName(CustomContextKeys.NewInstance);
            injectionBinder.Bind<ILocalPlayer>().To<LocalPlayer>().ToName(CustomContextKeys.NewInstance);

            injectionBinder.Bind<IHexMap>().To<HexMap>().ToName(CustomContextKeys.NewInstance);
            injectionBinder.Bind<ILandTile>().To<LandTile>().ToName(CustomContextKeys.NewInstance);
            injectionBinder.Bind<IWaterTile>().To<WaterTile>().ToName(CustomContextKeys.NewInstance);
            injectionBinder.Bind<IPathfinding>().To<Dijkstra>().ToSingleton();
            injectionBinder.Bind<OnInitialiseHexMapSignal>().ToSingleton();

            injectionBinder.Bind<ICountry>().To<Country>().ToName(CustomContextKeys.NewInstance);

            injectionBinder.Bind<ICity>().To<City>().ToName(CustomContextKeys.NewInstance);
            injectionBinder.Bind<ICities>().To<Cities>().ToSingleton();
            commandBinder.Bind<CreateCitySignal>().To<CreateCityCommand>();
            injectionBinder.Bind<OnCreateCitySignal>().ToSingleton();
            mediationBinder.Bind<CitiesView>().To<CitiesMediator>();
            mediationBinder.Bind<CityView>().To<CityMediator>();
            mediationBinder.Bind<CityWorldPanelsView>().To<CityWorldPanelsMediator>();
            mediationBinder.Bind<CityWorldPanelView>().To<CityWorldPanelMediator>();

            injectionBinder.Bind<IDate>().To<Date>().ToName(CustomContextKeys.NewInstance);
            injectionBinder.Bind<IDateManager>().To<DateManager>().ToSingleton();
            injectionBinder.Bind<OnCurrentDateChangeSignal>().ToSingleton();

            mediationBinder.Bind<ArmyView>().To<ArmyMediator>();
            mediationBinder.Bind<ArmiesView>().To<ArmiesMediator>();
            mediationBinder.Bind<InputView>().To<InputMediator>();

            mediationBinder.Bind<HexPanelView>().To<HexPanelMediator>();
            mediationBinder.Bind<CountryPanelView>().To<CountryPanelMediator>();
            mediationBinder.Bind<ArmyPanelView>().To<ArmyPanelMediator>();
            mediationBinder.Bind<CountrySelectionView>().To<CountrySelectionMediator>();
            mediationBinder.Bind<TimePanelView>().To<TimePanelMediator>();
            mediationBinder.Bind<UpdateView>().To<UpdateMediator>();
            injectionBinder.Bind<UpdateSignal>().ToSingleton();

            mediationBinder.Bind<HexGridView>().To<HexGridMediator>();
        }
    }
}