namespace Assets.Source.Model.Background
{
    public interface IBackgroundTaskManager
    {
        bool AreTasksComplete { get; }

        void SubmitTask(IBackgroundTask task);

        void ExecuteTasks(IDate date);
    }
}