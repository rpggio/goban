using System;
using System.Collections.Generic;
using Lambda.Collections.Generic;

namespace Goban.Model
{
	[Serializable]
	public class BoardBoundary : IGroup
	{
		public bool CanSurround
		{
			get { return true; }
		}

		public bool Contains(Position position)
		{
			throw new NotImplementedException();
		}

		public Stone Stone
		{
			get { throw new NotImplementedException(); }
		}

		public bool Bounds(Position pos)
		{
			throw new NotImplementedException();
		}

		public void Accept(IPositionVisitor visitor)
		{
			throw new NotImplementedException();
		}

		public Set<Position> GetNeighbors()
		{
			return new Set<Position>();
		}

		public Set<Position> Union(IEnumerable<Position> positions)
		{
			throw new NotImplementedException();
		}
	}
}