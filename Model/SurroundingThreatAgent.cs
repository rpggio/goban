namespace Goban.Model
{
    /// <summary>
    /// An agent that selects a play based on the most-threatened group, whether it is self or opponent. 
    /// Group threat is calculated as the ratio of ( breath / total surface ), so a small value represents
    /// high threat. A random open position surrounding the threatened group is selected.
    /// </summary>
	public class SurroundingThreatAgent : IAgent
	{
		public Position SelectPlay(Board board)
		{
			Group threatened = FindMinBreath(board);
			if (threatened == null)
			{
				return new RandomAgent().SelectPlay(board);
			}
			foreach(Position pos in threatened.GetNeighbors())
			{
				if (!board.IsOccupied(pos)) return pos;
			}
			throw new AgentException("Failed to select play");
		}
		
		private static Group FindMinBreath(Board board)
		{
			float minBreathRatio = 1;
			Group minBreathGroup = null;
			foreach(Group group in board)
			{
				int boundary = group.GetNeighbors().Count;
				int breath = board.GetBreath(group);
				float breathRatio = (float)(breath) / boundary;
				if (breathRatio < minBreathRatio)
				{
					minBreathRatio = breathRatio;
					minBreathGroup = group;
				}
			}
			return minBreathGroup;
		}
	}
}
