using System.Collections.Generic;
using System.Threading;

namespace Assets.Source.Model.Background.Impl
{
    public class BackgroundTaskLoop : IBackgroundTaskLoop
    {
        public bool AreTasksCompleted { get; private set; }

        private IList<IBackgroundTask> Tasks { get; set; }

        private Thread Thread { get; set; }

        public BackgroundTaskLoop()
        {
            AreTasksCompleted = true;
        }

        public void ProcessTasks(IList<IBackgroundTask> tasks)
        {
            Tasks = tasks;
            Thread = new Thread(ExecuteTasks);
            Thread.Start();
        }

        private void ExecuteTasks()
        {
            OnStartExecuteTasks();

            if (Tasks.Count > 0)
            {
                foreach (var task in Tasks)
                {
                    task.Execute();
                }
            }

            OnFinishExecuteTasks();
        }

        private void OnStartExecuteTasks()
        {
            AreTasksCompleted = false;
        }

        private void OnFinishExecuteTasks()
        {
            AreTasksCompleted = true;
            Tasks.Clear();
        }
    }
}