using System.Threading;
using Assets.Source.Contexts.Game.Mediators;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Core.IoC;
using Assets.Source.Model;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Commands
{
    public class ProcessDateTickSignal : Signal
    {
        
    }

    /// <summary>
    /// IDate: The new current date.
    /// </summary>
    public class OnCurrentDateChangeSignal : Signal<IDate>
    {
        
    }

    public class ProcessDateTickCommand : Command
    {
        [Inject]
        public IArmies Armies { get; set; }

        [Inject]
        public TimePanelResumeSignal TimePanelResumeDispatcher { get; set; }

        [Inject]
        public TimePanelWaitSignal TimePanelWaitDispatcher { get; set; }

        [Inject]
        public OnCurrentDateChangeSignal OnCurrentDateChangeDispatcher { get; set; }

        [Inject]
        public UpdateSignal UpdateSignal { get; set; }

        [Inject]
        public IDateManager DateManager { get; set; }

        private bool Finished { get; set; }

        public override void Execute()
        {
            Retain();

            UpdateSignal.AddListener(OnUpdate);
            OnStart();

            var thread = new Thread(ProcessMultithreaded);
            thread.Start();
        }

        private void OnUpdate()
        {
            if(Finished) OnFinish();
        }

        private void ProcessMultithreaded()
        {
            foreach (var army in Armies.AllArmies)
            {
                if (army.IsMoving)
                {
                    var path = army.MovementPath;
                    var next = path.NextMovement;
                    next.DecrementMovementTime();
                    if (next.HasArrived)
                    {
                        OnArmyArrival(army, next.Destination);
                        path.SetMovementComplete();
                        if (path.IsComplete)
                        {
                            army.IsMoving = false;
                        }
                    }
                }
            }

            Finished = true;
        }

        private void OnArmyArrival(IArmy army, IHexTile tile)
        {
            army.Location = tile;
            tile.Country = army.Country;
        }

        private void OnStart()
        {
            Debug.Log("OnStart");
            TimePanelWaitDispatcher.Dispatch();

            //ProcessMultithreaded();
        }

        private void OnFinish()
        {
            Release();
            UpdateSignal.RemoveListener(OnUpdate);

            var currentDate = DateManager.CurrentDate;
            var tentativeNextDate = DateManager.AddDays(currentDate, 1);
            Debug.Log("OnFinish");
            var nextDate = DateManager[tentativeNextDate];
            if (nextDate == null)
            {
                nextDate = injectionBinder.GetInstance<IDate>(CustomContextKeys.NewInstance);
                nextDate.Initialise(tentativeNextDate);
                DateManager[tentativeNextDate] = nextDate;
            }

            DateManager.CurrentDate = nextDate;
            OnCurrentDateChangeDispatcher.Dispatch(nextDate);

            TimePanelResumeDispatcher.Dispatch();

            Debug.Log("OnFinish");
        }
    }
}