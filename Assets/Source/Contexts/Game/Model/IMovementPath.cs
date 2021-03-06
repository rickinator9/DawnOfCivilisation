﻿using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Contexts.Game.Model
{
    public interface IMovementPath
    {
        IMovement NextMovement { get; }

        IMovement[] Movements { get; }

        bool IsComplete { get; }

        void Initialise(IList<IMovement> movements);

        void SetMovementComplete();
    }

    public class MovementPath : IMovementPath
    {
        private int _movementIndex;
        public IMovement NextMovement
        {
            get { return _movements[_movementIndex]; }
        }

        private IList<IMovement> _movements;
        public IMovement[] Movements
        {
            get { return _movements.ToArray(); }
        }

        public bool IsComplete
        {
            get { return _movements.Count <= _movementIndex; }
        }

        public void Initialise(IList<IMovement> movements)
        {
            _movementIndex = 0;
            _movements = movements;
        }

        public void SetMovementComplete()
        {
            _movementIndex++;
        }
    }
}