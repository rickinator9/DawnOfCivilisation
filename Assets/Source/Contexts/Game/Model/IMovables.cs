using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Contexts.Game.Model
{
    public interface IMovables
    {
        IMovable[] AllMovables { get; }

        void Add(IMovable movable);
        void Remove(IMovable movable);
    }

    public class Movables : IMovables
    {
        private IList<IMovable> _movables = new List<IMovable>(); 
        public IMovable[] AllMovables { get { return _movables.ToArray(); } }

        public void Add(IMovable movable)
        {
            _movables.Add(movable);
        }

        public void Remove(IMovable movable)
        {
            _movables.Remove(movable);
        }
    }
}