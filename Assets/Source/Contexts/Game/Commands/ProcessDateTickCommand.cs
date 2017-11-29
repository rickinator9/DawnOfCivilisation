using System.Threading;
using Assets.Source.Contexts.Game.Mediators;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Map;
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
        public IMovables Movables { get; set; }

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
            foreach (var movable in Movables.AllMovables)
            {
                if (movable.IsMoving)
                {
                    var path = movable.MovementPath;
                    var next = path.NextMovement;
                    next.DecrementMovementTime();
                    if (next.HasArrived)
                    {
                        OnArrival(movable, next.Destination);
                        path.SetMovementComplete();
                        if (path.IsComplete)
                        {
                            movable.IsMoving = false;
                        }
                    }
                }
            }

            Finished = true;
        }

        private void OnArrival(IMovable movable, IHexTile tile)
        {
            movable.Location = tile;
            movable.OnArrivalInTile(tile);
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