using System;
using System.Collections.Generic;
using Lambda.Collections.Generic;

namespace Goban.Model
{
    /// <summary>
    /// Represents the board boundary. Contains no positions but can surround a group.
    /// </summary>
	[Serializable]
	public class BoardBoundary : IGroup
	{
        /// <summary>
        /// The boundary can participate in the surrounding of another group.
        /// </summary>
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