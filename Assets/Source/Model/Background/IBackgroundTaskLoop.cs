using System.Collections.Generic;

namespace Assets.Source.Model.Background
{
    public interface IBackgroundTaskLoop
    {
        bool AreTasksCompleted { get; }

        void ProcessTasks(IList<IBackgroundTask> tasks);
    }
}