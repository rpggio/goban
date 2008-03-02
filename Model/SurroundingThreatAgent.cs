namespace Goban.Model
{
	class SurroundingThreatAgent : IAgent
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
		
		private Group FindMinBreath(Board board)
		{
			float minBreathRatio = 1;
			Group minBreathGroup = null;
			foreach(Group chain in board)
			{
				int boundary = chain.GetNeighbors().Count;
				int breath = board.GetBreath(chain);
				float breathRatio = (float)(breath) / boundary;
				if (breathRatio < minBreathRatio)
				{
					minBreathRatio = breathRatio;
					minBreathGroup = chain;
				}
			}
			return minBreathGroup;
		}
	}
}
