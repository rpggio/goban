namespace Goban.Model
{
    /// <summary>
    /// A visitor that can visit a position within a group.
    /// </summary>
    public interface IPositionVisitor
    {
        /// <summary>
        /// Visit a position
        /// </summary>
        /// <param name="position"></param>
        /// <param name="group">Group that contains the position.</param>
        void Visit(Position position, Group group);
    }
}