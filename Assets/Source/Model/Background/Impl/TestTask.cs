using UnityEngine;

namespace Assets.Source.Model.Background.Impl
{
    public class TestTask : IBackgroundTask
    {
        public IDate ExecutionDate { get; private set; }

        public TestTask(IDate date)
        {
            ExecutionDate = date;
        }

        public void Execute()
        {
            Debug.Log("Testing the tasks");
        }
    }
}