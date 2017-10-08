using Assets.Source.Model;

namespace Assets.Source.Views
{
    public interface IArmyView
    {
        void Initialise(IArmy army);

        void Refresh();
    }
}