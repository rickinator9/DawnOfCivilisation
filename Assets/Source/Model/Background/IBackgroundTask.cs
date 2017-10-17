namespace Assets.Source.Model.Background
{
    public interface IBackgroundTask
    {
        IDate ExecutionDate { get; }

        void Execute();
    }
}