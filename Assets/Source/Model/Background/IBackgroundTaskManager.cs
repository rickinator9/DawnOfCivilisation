namespace Assets.Source.Model.Background
{
    public interface IBackgroundTaskManager
    {
        bool AreTasksComplete { get; }

        void SubmitTask(IBackgroundTask task); // TODO: Add date.

        void ExecuteTasks(); // TODO: Add date.
    }
}