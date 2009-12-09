using System;
using System.Collections.Generic;
using Lambda.Collections.Generic;

namespace Goban.Model
{
    /// <summary>
    /// Contains the player groups. Unoccupied positions and boundaries are represented by 
    /// IGroup instances. Provides methods for making group calculations and handling captures.
    /// </summary>
	[Serializable]
	public class Board : List<IGroup>
	{
		private int _size;

		private Board()
		{
			// for serialization
		}
		
		public Board(int size)
		{
			_size = size;
		}

        /// <summary>
        /// Width / height of the square board.
        /// </summary>
		public int Size
		{
			get { return _size; }
		}

        /// <summary>
        /// Place a stone on the board. Evaluate capture rules, removing groups as appropriate.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="stone"></param>
		public void Place(Position pos, Stone stone)
		{
			Set<Position> newGroup = new Set<Position>();
			newGroup.Add(pos);
			List<IGroup> mergeList = FindAll(delegate(IGroup g) { return g.Stone == stone && g.Bounds(pos); });
			foreach(IGroup defunctGroup in mergeList)
			{
				Remove(defunctGroup);
				newGroup = newGroup.Union((IEnumerable<Position>)defunctGroup);
			}
			Add(new Group(stone, newGroup));

			CheckEnclosedBy(pos);
			CheckEnclosed(pos);
		}

        /// <summary>
        /// Find the group at the given position.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
		public IGroup FindGroup(Position pos)
		{
			if (!IsInPlayArea(pos)) return new BoardBoundary();
			foreach (IGroup g in this)
			{
				if (g.Contains(pos)) return g;
			}
			return new NullGroup();
		}
	
        /// <summary>
        /// Is this position occupied by a valid group?
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
		public bool IsOccupied(Position pos)
		{
			return !(FindGroup(pos) is NullGroup);
		}
		
        /// <summary>
        /// Is this position in the playable area?
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
		public bool IsInPlayArea(Position pos)
		{
			return pos.Row >= 0 && pos.Row < _size
					&& pos.Column >= 0 && pos.Column < _size;
		}
		
        /// <summary>
        /// Apply visitor to all positions in the contained groups.
        /// </summary>
        /// <param name="visitor"></param>
		public void Accept(IPositionVisitor visitor)
		{
			foreach (IGroup g in this) g.Accept(visitor);
		}

        /// <summary>
        /// Get the breath for a group, which is the number of open positions surrounding a group.
        /// When a group has zero breath, it will be removed from the board.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public int GetBreath(IGroup group)
        {
            int breath = 0;
            foreach(Position p in group.GetNeighbors())
            {
                if (!FindGroup(p).CanSurround) breath++;
            }
            return breath;
        }

        protected void CheckEnclosedBy(Position position)
		{
			foreach(Position p in position.GetNeighbors())
			{
				IGroup group = FindGroup(p);
				if (IsEnclosed(group)) Remove(group);
			}
		}

        protected void CheckEnclosed(Position pos)
		{
			IGroup group = FindGroup(pos);
			if (IsEnclosed(group)) Remove(group);
		}

        protected bool IsEnclosed(IGroup group)
		{
			foreach(Position p in group.GetNeighbors())
			{
				if (!FindGroup(p).CanSurround) return false;
			}
			return true;
		}
	}
}
