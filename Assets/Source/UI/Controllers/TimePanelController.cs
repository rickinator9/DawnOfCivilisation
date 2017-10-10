using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Controllers
{
    public class TimePanelController : MonoBehaviour
    {
        private const char PlaySymbol = '►';
        private const char PauseSymbol = '║';

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
                        ButtonText.text = PlaySymbol.ToString();
                        break;
                    default:
                        ButtonText.text = PauseSymbol.ToString();
                        break;
                }
            }
        }

        private int _time;
        public int Time
        {
            get { return _time; }
            set
            {
                _time = value;
                TimerText.text = _time.ToString();
            }
        }

        public Text TimerText;
        public Text ButtonText;

        void Start()
        {
            IsPlaying = false;
        }

        void Update()
        {
            if (IsPlaying) Time++;
        }

        public void OnPlayButtonPressed()
        {
            IsPlaying = !IsPlaying;
        }
    }
}
