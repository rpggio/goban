using System.Collections.Generic;
using Lambda.Collections.Generic;

namespace Goban.Model
{
	public interface IGroup
	{
		bool Contains(Position position);
		bool CanSurround { get; }
		Stone Stone { get; }
		bool Bounds(Position pos);
		void Accept(IPositionVisitor visitor);
		Set<Position> GetNeighbors();
		Set<Position> Union(IEnumerable<Position> positions);
	}
}