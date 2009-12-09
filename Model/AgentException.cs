using System;

namespace Goban.Model
{
    /// <summary>
    /// Exception indicating an error in the calculations of an agent.
    /// </summary>
    public class AgentException : Exception
    {
        public AgentException(string message) : base(message)
        {
        }
    }
}