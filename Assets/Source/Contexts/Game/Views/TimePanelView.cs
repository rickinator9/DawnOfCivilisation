using Assets.Source.Model;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.Views
{
    public class TimePanelView : View
    {
        private const char PlaySymbol = '►';
        private const char PauseSymbol = '║';
        private const float SecondsPerTick = 1.0f;

        public Signal OnTickSignal = new Signal();

        private bool _isPlaying;
        private bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                _isPlaying = value;
                switch (_isPlaying)
                {
                    case true:
                        ButtonText.text = PlaySymbol.ToString();
                        RefreshTickTime();
                        break;
                    default:
                        ButtonText.text = PauseSymbol.ToString();
                        break;
                }
            }
        }
        
        [SerializeField]
        private Text TimerText;
        [SerializeField]
        private Text ButtonText;

        private float NextTickTime { get; set; }

        private bool HasTimeSurpassedTickTime
        {
            get { return Time.time > NextTickTime; }
        }

        public bool AreTasksComplete { get; set; }

        private void OnTick()
        {
            Debug.Log("OnTick");
            OnTickSignal.Dispatch();
        }

        private void RefreshTickTime()
        {
            NextTickTime = Time.time + SecondsPerTick;
        }

        public void SetDate(IDate date)
        {
            TimerText.text = date.ToString();
        }

        #region Unity Methods
        protected override void Start()
        {
            base.Start();

            IsPlaying = false;
            AreTasksComplete = true;
        }

        void Update()
        {
            if (IsPlaying && AreTasksComplete && HasTimeSurpassedTickTime)
            {
                OnTick();
                RefreshTickTime();
            }
        }

        // Event listener for the buttons.
        public void OnPlayButtonPressed()
        {
            IsPlaying = !IsPlaying;
        }
        #endregion
    }
}