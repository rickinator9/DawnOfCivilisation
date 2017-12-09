using Assets.Source.Contexts.Game.Commands;
using Assets.Source.Contexts.Game.Commands.ProcessCommands;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;
using Assets.Source.Model;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Mediators
{
    public class TimePanelWaitSignal : Signal
    {
        
    }

    public class TimePanelResumeSignal : Signal
    {
        
    }

    public class TimePanelMediator : ViewMediator<TimePanelView>
    {
        [Inject]
        public TimePanelWaitSignal TimePanelWaitSignal { get; set; }

        [Inject]
        public TimePanelResumeSignal TimePanelResumeSignal { get; set; }

        [Inject]
        public ProcessDateTickSignal ProcessDateTickDispatcher { get; set; }

        [Inject]
        public OnCurrentDateChangeSignal CurrentDateChangeSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            View.OnTickSignal.AddListener(OnTick);

            CurrentDateChangeSignal.AddListener(OnCurrentDateChange);
            TimePanelWaitSignal.AddListener(OnWait);
            TimePanelResumeSignal.AddListener(OnResume);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            View.OnTickSignal.RemoveListener(OnTick);

            CurrentDateChangeSignal.RemoveListener(OnCurrentDateChange);
            TimePanelWaitSignal.RemoveListener(OnWait);
            TimePanelResumeSignal.RemoveListener(OnResume);
        }

        private void OnCurrentDateChange(IDate date)
        {
            View.SetDate(date);
        }

        private void OnResume()
        {
            View.AreTasksComplete = true;
        }

        private void OnWait()
        {
            View.AreTasksComplete = false;
        }

        private void OnTick()
        {
            ProcessDateTickDispatcher.Dispatch();
        }
    }
}