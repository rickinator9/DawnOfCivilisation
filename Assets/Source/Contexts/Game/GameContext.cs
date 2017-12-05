using Assets.Source.Contexts.Game.Commands;
using Assets.Source.Contexts.Game.Commands.Army;
using Assets.Source.Contexts.Game.Commands.City;
using Assets.Source.Contexts.Game.Commands.Country;
using Assets.Source.Contexts.Game.Commands.Initialisation;
using Assets.Source.Contexts.Game.Commands.Input;
using Assets.Source.Contexts.Game.Commands.Map;
using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Contexts.Game.Commands.War;
using Assets.Source.Contexts.Game.Mediators;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Contexts.Game.Model.Map.MapMode;
using Assets.Source.Contexts.Game.Model.Pathfinding;
using Assets.Source.Contexts.Game.Model.Political;
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

            injectionBinder.GetInstance<InitialiseGameSignal>().Dispatch();
        }

        protected override void mapBindings()
        {
            commandBinder.Bind<CreateArmySignal>().To<CreateArmyCommand>().Pooled();
            commandBinder.Bind<CreateCountrySignal>().To<CreateCountryCommand>().Pooled();
            commandBinder.Bind<LeftMouseClickSignal>().To<LeftMouseClickCommand>().Pooled();
            commandBinder.Bind<RightMouseClickSignal>().To<RightMouseClickCommand>().Pooled();
            commandBinder.Bind<CreateMovementPathSignal>().To<CreateMovementPathCommand>().Pooled();
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
            injectionBinder.Bind<IMovables>().To<Movables>().ToSingleton();
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
            injectionBinder.Bind<ICountries>().To<Countries>().ToSingleton();
            injectionBinder.Bind<ICountryNames>().To<CountryNames>().ToSingleton();

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
            mediationBinder.Bind<CityPanelView>().To<CityPanelMediator>();
            mediationBinder.Bind<CountrySelectionView>().To<CountrySelectionMediator>();
            mediationBinder.Bind<TimePanelView>().To<TimePanelMediator>();
            mediationBinder.Bind<UpdateView>().To<UpdateMediator>();
            injectionBinder.Bind<UpdateSignal>().ToSingleton();

            mediationBinder.Bind<HexGridView>().To<HexGridMediator>();

            BindInitialisation();
            BindMapModes();
            BindWars();
        }

        private void BindInitialisation()
        {
            commandBinder.Bind<InitialiseGameSignal>().To<InitialiseGameCommand>();
            commandBinder.Bind<InitialiseHexMapSignal>().To<InitialiseHexMapCommand>();
            commandBinder.Bind<InitialiseDateManagerSignal>().To<InitialiseDateManagerCommand>();
            commandBinder.Bind<InitialisePlayerSignal>().To<InitialisePlayerCommand>();
        }

        private void BindMapModes()
        {
            commandBinder.Bind<SetMapModeSignal>().To<SetMapModeCommand>();
            injectionBinder.Bind<OnSetMapModeSignal>().ToSingleton();

            injectionBinder.Bind<IMapMode>().To<PoliticalMapMode>().ToName(MapMode.Political);
        }

        private void BindWars()
        {
            commandBinder.Bind<WarDeclarationSignal>().To<WarDeclarationCommand>();
            commandBinder.Bind<PlayerWarDeclarationSignal>().To<PlayerWarDeclarationCommand>();

            injectionBinder.Bind<IWar>().To<War>().ToName(CustomContextKeys.NewInstance);

            mediationBinder.Bind<WarsPanelView>().To<WarsPanelMediator>();
            mediationBinder.Bind<WarPanelView>().To<WarPanelMediator>();
            mediationBinder.Bind<WarOverviewPanelView>().To<WarOverviewPanelMediator>();

            mediationBinder.Bind<BeligerentPanelView>().To<BeligerentPanelMediator>();
            mediationBinder.Bind<BeligerentListPanelView>().To<BeligerentListPanelMediator>();
        }
    }
}