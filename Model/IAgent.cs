using System;

namespace Goban.Model
{
	public interface IAgent
	{
		Position SelectPlay(Board board);
	}

	public class AgentException : Exception
	{
		public AgentException(string message) : base(message)
		{
		}
	}
}
