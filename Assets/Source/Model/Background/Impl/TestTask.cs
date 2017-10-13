using UnityEngine;

namespace Assets.Source.Model.Background.Impl
{
    public class TestTask : IBackgroundTask
    {
        public void Execute()
        {
            Debug.Log("Testing the tasks");
        }
    }
}