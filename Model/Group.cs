using System;
using System.Collections.Generic;
using System.Text;
using Lambda.Collections.Generic;

namespace Goban.Model
{
    /// <summary>
    /// A set of positions, with implementation of IGroup based on the contained positions.
    /// </summary>
	[Serializable]
    public class Group : Set<Position>, IGroup
    {
        private Stone _stone;

    	private Group()
    	{
			// for serialization
    	}

    	public Group(Stone stone, Position position)
    	{
    		_stone = stone;
			Add(position);
    	}

       	public Group(Stone stone, IEnumerable<Position> positions)
    	{
    		_stone = stone;
			AddRange(positions);
    	}
    	
    	public Stone Stone
    	{
    		get { return _stone; }
    	}

    	public bool CanSurround
    	{
			get { return true; }
    	}

    	public bool Bounds(Position pos)
    	{
    		foreach (Position p in this)
    		{
				if (p.Bounds(pos)) return true;
    		}
			return false;
    	}
    	   	
    	public void Accept(IPositionVisitor visitor)
		{
			foreach (Position p in this) visitor.Visit(p, this);
		}

        /// <summary>
        /// Get neighbors of all the contained positions.
        /// </summary>
        /// <returns></returns>
		public Set<Position> GetNeighbors()
		{
			Set<Position> neighbors = new Set<Position>();
			foreach (Position p in this) neighbors.AddRange(p.GetNeighbors());
			return neighbors.FindAll(delegate(Position p) { return !Contains(p); });
		}
		
		public override string ToString()
        {
        	StringBuilder sb = new StringBuilder();
        	sb.Append(string.Format("#Group {{ Color:{0} Positions: {{", _stone));
        	foreach(Position p in this) sb.Append(p);
			sb.Append("}}");
            return sb.ToString();
        }
    }
}
