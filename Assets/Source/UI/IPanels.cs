namespace Assets.Source.UI
{
    public interface IPanels
    {
        IArmyPanel ArmyPanel { get; }

        ICountryPanel CountryPanel { get; }
        
        IHexPanel HexPanel { get; }

        void HideAll();
    }
}