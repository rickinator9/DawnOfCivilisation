using System.Collections.Generic;

namespace Assets.Source.Model.Background.Impl
{
    public class BackgroundTaskManager : IBackgroundTaskManager
    {
        private static IBackgroundTaskManager _instance;
        public static IBackgroundTaskManager Instance
        {
            get { return _instance ?? (_instance = new BackgroundTaskManager()); }
        }

        public bool AreTasksComplete
        {
            get { return Loop.AreTasksCompleted; }
        }

        private IBackgroundTaskLoop Loop { get; set; }
        private IList<IBackgroundTask> Tasks { get; set; } 

        private BackgroundTaskManager()
        {
            Loop = new BackgroundTaskLoop();
            Tasks = new List<IBackgroundTask>();
        }

        public void SubmitTask(IBackgroundTask task)
        {
            Tasks.Add(task);
        }

        public void ExecuteTasks()
        {
            Loop.ProcessTasks(Tasks);
        }
    }
}