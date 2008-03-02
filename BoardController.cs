using System;
using System.Threading;
using Goban.Model;

namespace Goban
{
	class BoardController
	{
		private const int AgentSleepMilliseconds = 50;
		private Board _board;
		private BoardControl _boardControl;
		private Stone _current = Stone.White;
		private IAgent _agent = new SurroundingThreatAgent();

		public BoardController(Board board, BoardControl boardControl)
		{
			_board = board;
			_boardControl = boardControl;
			_boardControl.PositionClick += HandlePositionClick;
			_boardControl.PositionDeleteCommand += HandlePositionDeleteCommand;
			_boardControl.SetModel(board);
		}

		private void RotateColor()
		{
			_current = _current == Stone.White
				? Stone.Black
				: Stone.White;
		}
		
		void HandlePositionDeleteCommand(Position position)
		{
			IGroup group = _board.FindGroup(position);
			if (group is Group)
			{
			    _board.Remove(group);
			    _boardControl.Refresh();
			}
		}

		void HandlePositionClick(Position position)
		{
			if (_board.FindGroup(position) is NullGroup)
			{
				_board.Place(position, _current);
				_boardControl.Refresh();
				RotateColor();
				
				Thread.Sleep(AgentSleepMilliseconds);
				_board.Place(_agent.SelectPlay(_board), _current);
				_boardControl.Refresh();
				RotateColor();
			}
		}
	}
}
