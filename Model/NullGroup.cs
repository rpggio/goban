using System;
using System.Collections.Generic;
using Lambda.Collections.Generic;

namespace Goban.Model
{
    /// <summary>
    /// A group that contains no positions and cannot surround.
    /// </summary>
	[Serializable]
	public class NullGroup : IGroup
	{
		public bool CanSurround
		{
			get { return false; }
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
