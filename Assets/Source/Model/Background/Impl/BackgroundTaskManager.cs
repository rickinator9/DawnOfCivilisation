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
        private IDictionary<IDate, IList<IBackgroundTask>> TasksByDate { get; set; }

        private BackgroundTaskManager()
        {
            Loop = new BackgroundTaskLoop();
            TasksByDate = new Dictionary<IDate, IList<IBackgroundTask>>();
        }

        public void SubmitTask(IBackgroundTask task)
        {
            var tasks = GetOrCreateTaskList(task.ExecutionDate);
            tasks.Add(task);
        }

        public void ExecuteTasks(IDate date)
        {
            var tasks = GetOrCreateTaskList(date);
            Loop.ProcessTasks(tasks);
        }

        private IList<IBackgroundTask> GetOrCreateTaskList(IDate date)
        {
            if(!TasksByDate.ContainsKey(date)) TasksByDate[date] = new List<IBackgroundTask>();
            return TasksByDate[date];
        }
    }
}