using System;
using System.Collections.Generic;

namespace Goban.Model
{
    /// <summary>
    /// Represents a coordinate on the game board.
    /// </summary>
	[Serializable]
    public struct Position
    {
        private int _column;
        private int _row;

    	public Position(int column, int row)
    	{
    		_column = column;
    		_row = row;
    	}

    	public int Column
        {
            get { return _column; }
        }

        public int Row
        {
            get { return _row; }
        }

        /// <summary>
        /// Does this position bound the provided position?
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
    	public bool Bounds(Position pos)
    	{
			return Math.Abs(pos.Row - _row)
				   + Math.Abs(pos.Column - _column) == 1;
    	}
    	
        /// <summary>
        /// Get the neighbors of this position based on coordinate increment.
        /// </summary>
        /// <returns></returns>
    	public IList<Position> GetNeighbors()
    	{
			return new Position[] { 
				new Position(_column - 1, _row), 
				new Position(_column + 1, _row), 
				new Position(_column, _row - 1), 
				new Position(_column, _row + 1)
    		};
    	}
    	
    	public override string ToString()
		{
		    return string.Format("#Position {{ Column:{0}, Row:{1} }}", _column, _row);
		}
    }
}
