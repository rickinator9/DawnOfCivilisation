using Assets.Source.Model.Background;
using Assets.Source.Model.Background.Impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Controllers
{
    public class TimePanelController : MonoBehaviour
    {
        private const char PlaySymbol = '►';
        private const char PauseSymbol = '║';
        private const float SecondsPerTick = 1.0f;

        private bool _isPlaying;
        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                _isPlaying = value;
                switch (_isPlaying)
                {
                    case true:
                        OnPlay();
                        break;
                    default:
                        OnPause();
                        break;
                }
            }
        }

        private int _tick;
        public int Tick
        {
            get { return _tick; }
            set
            {
                _tick = value;
                OnTickChange();
            }
        }

        public Text TimerText;
        public Text ButtonText;

        private float NextTickTime { get; set; }
        private IBackgroundTaskManager TaskManager { get; set; }

        private bool HasTimeSurpassedTickTime
        {
            get { return Time.time > NextTickTime; }
        }

        void Start()
        {
            IsPlaying = false;
            TaskManager = BackgroundTaskManager.Instance;
        }

        void Update()
        {
            if (IsPlaying && TaskManager.AreTasksComplete && HasTimeSurpassedTickTime)
            {
                Tick++;
                RefreshTickTime();
            }
        }

        // Event listener for the buttons.
        public void OnPlayButtonPressed()
        {
            IsPlaying = !IsPlaying;
        }

        private void OnPlay()
        {
            ButtonText.text = PlaySymbol.ToString();
            RefreshTickTime();
        }

        private void OnPause()
        {
            ButtonText.text = PauseSymbol.ToString();
        }

        private void OnTickChange()
        {
            TimerText.text = Tick.ToString();
            TaskManager.SubmitTask(new TestTask());
            TaskManager.ExecuteTasks();
        }

        private void RefreshTickTime()
        {
            NextTickTime = Time.time + SecondsPerTick;
        }
    }
}
