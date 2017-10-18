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

        private IDate _currentDate;

        public IDate CurrentDate
        {
            get { return _currentDate; }
            set
            {
                _currentDate = value;
                foreach (var callback in CurrentDateChangeCallbacks)
                {
                    callback(CurrentDate);
                }
            }
        }

        private IDateManager DateManager { get; set; }

        private IBackgroundTaskManager TaskManager { get; set; }

        private TimeManager()
        {
            CurrentDateChangeCallbacks = new List<DateCallback>();
            DateManager = Impl.DateManager.Instance;
            TaskManager = BackgroundTaskManager.Instance;
        }

        public void Initialise(IDate startDate)
        {
            CurrentDate = startDate;
        }

        public void OnTick()
        {
            CurrentDate = DateManager.AddDays(CurrentDate, 1);
            TaskManager.ExecuteTasks(CurrentDate);
        }
    }
}