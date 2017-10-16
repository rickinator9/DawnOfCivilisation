using Assets.Source.Model;

namespace Assets.Source.UI
{
    public interface ICountryPanel : IPanel
    {
        ICountry Country { get; set; }

        void ShowForCountry(ICountry country);
    }
}