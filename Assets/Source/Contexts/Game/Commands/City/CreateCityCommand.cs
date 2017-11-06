using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Core.IoC;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.City
{
    /// <summary>
    /// IHexTile: Location where the city will be created.
    /// </summary>
    public class CreateCitySignal : Signal<IHexTile>
    {
        
    }

    /// <summary>
    /// ICity: The city that just was created.
    /// </summary>
    public class OnCreateCitySignal : Signal<ICity>
    {
        
    }

    public class CreateCityCommand : Command
    {
        [Inject]
        public IHexTile Location { get; set; }

        [Inject(CustomContextKeys.NewInstance)]
        public ICity NewCity { get; set; }

        [Inject]
        public ICities Cities { get; set; }

        [Inject]
        public OnCreateCitySignal OnCreateCityDispatcher { get; set; }

        public override void Execute()
        {
            Cities.Add(NewCity);
            NewCity.Initialise(Location, "Babili");

            OnCreateCityDispatcher.Dispatch(NewCity);
        }
    }
}