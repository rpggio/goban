using System;
using System.Collections.Generic;
using Lambda.Collections.Generic;

namespace Goban.Model
{
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

		public int Size
		{
			get { return _size; }
		}

		public void Place(Position pos, Stone stone)
		{
			Set<Position> newChain = new Set<Position>();
			newChain.Add(pos);
			List<IGroup> mergeList = FindAll(delegate(IGroup g) { return g.Stone == stone && g.Bounds(pos); });
			foreach(IGroup defunctChain in mergeList)
			{
				Remove(defunctChain);
				newChain = newChain.Union((IEnumerable<Position>)defunctChain);
			}
			Add(new Group(stone, newChain));

			CheckEnclosedBy(pos);
			CheckEnclosed(pos);
		}

		public IGroup FindChain(Position pos)
		{
			if (!IsInPlayArea(pos)) return new BoardBoundary();
			foreach (IGroup g in this)
			{
				if (g.Contains(pos)) return g;
			}
			return new NullChain();
		}
	
		public bool IsOccupied(Position pos)
		{
			return !(FindChain(pos) is NullChain);
		}
		
		public bool IsInPlayArea(Position pos)
		{
			return pos.Row >= 0 && pos.Row < _size
					&& pos.Column >= 0 && pos.Column < _size;
		}
		
		public void Accept(IPositionVisitor visitor)
		{
			foreach (IGroup g in this) g.Accept(visitor);
		}

		protected void CheckEnclosedBy(Position position)
		{
			foreach(Position p in position.GetNeighbors())
			{
				IGroup group = FindChain(p);
				if (IsEnclosed(group)) Remove(group);
			}
		}
		
		protected void CheckEnclosed(Position pos)
		{
			IGroup group = FindChain(pos);
			if (IsEnclosed(group)) Remove(group);
		}
		
		protected bool IsEnclosed(IGroup group)
		{
			foreach(Position p in group.GetNeighbors())
			{
				if (!FindChain(p).CanSurround) return false;
			}
			return true;
		}
		
		public int GetBreath(IGroup group)
		{
			int breath = 0;
			foreach(Position p in group.GetNeighbors())
			{
				if (!FindChain(p).CanSurround) breath++;
			}
			return breath;
		}

	}
}
