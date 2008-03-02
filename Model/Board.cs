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

		public IGroup FindGroup(Position pos)
		{
			if (!IsInPlayArea(pos)) return new BoardBoundary();
			foreach (IGroup g in this)
			{
				if (g.Contains(pos)) return g;
			}
			return new NullGroup();
		}
	
		public bool IsOccupied(Position pos)
		{
			return !(FindGroup(pos) is NullGroup);
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
		
		public int GetBreath(IGroup group)
		{
			int breath = 0;
			foreach(Position p in group.GetNeighbors())
			{
				if (!FindGroup(p).CanSurround) breath++;
			}
			return breath;
		}

	}
}
