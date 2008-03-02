using System;
using System.Collections.Generic;

namespace Goban.Model
{
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

    	public bool Bounds(Position pos)
    	{
			return Math.Abs(pos.Row - _row)
				   + Math.Abs(pos.Column - _column) == 1;
    	}
    	
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
	
	public interface IPositionVisitor
    {
        void Visit(Position position, Group group);
    }
}
