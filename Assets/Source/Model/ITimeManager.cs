using System.Collections.Generic;

namespace Assets.Source.Model
{
    public interface ITimeManager
    {
        IList<DateCallback> CurrentDateChangeCallbacks { get; }

        IDate CurrentDate { get; }

        void Initialise(IDate startDate);

        void OnTick();
    }
}