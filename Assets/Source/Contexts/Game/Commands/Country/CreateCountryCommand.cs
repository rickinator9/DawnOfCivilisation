using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Core.IoC;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.Country
{
    public struct CountryCreationParams
    {
        public IHexTile InitialLocation;
        public string Name;
    }

    public class CreateCountrySignal : Signal<CountryCreationParams>
    {
        
    }

    public class CreateCountryCommand : Command
    {
        [Inject]
        public CountryCreationParams CountryCreationParams { get; set; }
        
        [Inject(CustomContextKeys.NewInstance)]
        public ICountry NewCountry { get; set; }

        [Inject]
        public ICountries Countries { get; set; }

        [Inject]
        public ICountryNames CountryNames { get; set; }

        [Inject]
        public IMovables Movables { get; set; }

        public override void Execute()
        {
            NewCountry.Name = CountryNames.RandomName;
            NewCountry.Location = CountryCreationParams.InitialLocation;

            Countries.Add(NewCountry);
            Movables.Add(NewCountry);
        }
    }
}