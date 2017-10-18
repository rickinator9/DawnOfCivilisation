using Assets.Source.Model;
using Assets.Source.Model.Background;
using Assets.Source.Model.Background.Impl;
using Assets.Source.Model.Impl;
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
        
        public Text TimerText;
        public Text ButtonText;

        private float NextTickTime { get; set; }
        private IBackgroundTaskManager TaskManager { get; set; }
        private ITimeManager TimeManager { get; set; }

        private bool HasTimeSurpassedTickTime
        {
            get { return Time.time > NextTickTime; }
        }

        void Start()
        {
            IsPlaying = false;
            TaskManager = BackgroundTaskManager.Instance;

            TimeManager = Model.Impl.TimeManager.Instance;
            TimeManager.CurrentDateChangeCallbacks.Add(SetCurrentDate);
            var startDate = DateManager.Instance.GetDate(1, 1, 1);
            TimeManager.Initialise(startDate);
        }

        void Update()
        {
            if (IsPlaying && TaskManager.AreTasksComplete && HasTimeSurpassedTickTime)
            {
                OnTickChange();
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
            TimeManager.OnTick();
        }

        private void SetCurrentDate(IDate currentDate)
        {
            TimerText.text = currentDate.ToString();
        }

        private void RefreshTickTime()
        {
            NextTickTime = Time.time + SecondsPerTick;
        }
    }
}