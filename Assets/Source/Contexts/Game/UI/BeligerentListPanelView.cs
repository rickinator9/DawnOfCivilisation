using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Core.IoC;
using UnityEngine;

namespace Assets.Source.Contexts.Game.UI
{
    public class BeligerentListPanelView : ChildCreatorView<BeligerentPanelView, BeligerentPanelMediator, ICountry> 
    {
        protected override void OnMediatorCreated(BeligerentPanelMediator mediator, ICountry country)
        {
            mediator.Initialise(country);
        }

        public void ResetAndSpawn(ICountry[] countries)
        {
            Reset();
            SpawnCountries(countries);
        }

        public void Reset()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void SpawnCountries(ICountry[] countries)
        {
            foreach (var country in countries)
            {
                CreateViewForObject(country);
            }
        }
    }
}