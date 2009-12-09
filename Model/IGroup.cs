using System.Collections.Generic;
using Lambda.Collections.Generic;

namespace Goban.Model
{
    /// <summary>
    /// Represents a logical group of positions. Provides operations for making calculations
    /// based on contained and adjacent positions.
    /// </summary>
	public interface IGroup
	{
        /// <summary>
        /// Does the group contain this position?
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
		bool Contains(Position position);

        /// <summary>
        /// Can this group surround a position for capture?
        /// </summary>
		bool CanSurround { get; }

        /// <summary>
        /// Color of the stones in this group, if appropriate.
        /// </summary>
		Stone Stone { get; }

        /// <summary>
        /// Does this group bound the given position?
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
		bool Bounds(Position pos);

        /// <summary>
        /// Apply visitor to contained positions.
        /// </summary>
        /// <param name="visitor"></param>
		void Accept(IPositionVisitor visitor);

        /// <summary>
        /// Get all of the neighboring positions.
        /// </summary>
        /// <returns></returns>
		Set<Position> GetNeighbors();

        /// <summary>
        /// Merge contained positions with the provided positions into a new set.
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
		Set<Position> Union(IEnumerable<Position> positions);
	}
}