namespace Goban.Model
{
    /// <summary>
    /// An agent capable of selecting a play based on current board state.
    /// </summary>
	public interface IAgent
	{
        /// <summary>
        /// Select a position for play.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
		Position SelectPlay(Board board);
	}
}
