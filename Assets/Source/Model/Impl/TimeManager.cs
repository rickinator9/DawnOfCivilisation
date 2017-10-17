using System.Collections.Generic;
using Assets.Source.Model.Background;
using Assets.Source.Model.Background.Impl;
using UnityEditorInternal;

namespace Assets.Source.Model.Impl
{
    public class TimeManager : ITimeManager
    {
        private static ITimeManager _instance;
        public static ITimeManager Instance
        {
            get { return _instance ?? (_instance = new TimeManager()); }
        }

        public IList<DateCallback> CurrentDateChangeCallbacks { get; private set; }
        public IDate CurrentDate { get; private set; }

        private IDateManager DateManager { get; set; }

        private IBackgroundTaskManager TaskManager { get; set; }

        public void Initialise(IDate startDate)
        {
            CurrentDateChangeCallbacks = new List<DateCallback>();
            CurrentDate = startDate;
            DateManager = Impl.DateManager.Instance;
            TaskManager = BackgroundTaskManager.Instance;
        }

        public void OnTick()
        {
            CurrentDate = DateManager.AddDays(CurrentDate, 1);
            foreach (var callback in CurrentDateChangeCallbacks)
            {
                callback(CurrentDate);
            }

            TaskManager.ExecuteTasks(CurrentDate);
        }
    }
}