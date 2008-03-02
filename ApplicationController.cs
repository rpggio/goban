using System;
using System.IO;
using System.Windows.Forms;
using Goban.Model;

namespace Goban
{
	public class ApplicationController
	{
		private const string SaveFile = "game.sav";
		private const int BoardSize = 19;
		private const bool DebugMode = true;

		private Board _board;
		private MainForm _form;

		public ApplicationController(MainForm form)
		{
			_form = form;
			if (!DebugMode)
			{
				Application.ThreadException += HandleThreadException;
			}
			Application.ApplicationExit += HandleApplicationExit;

			InitBoard();
		}

		public void Start()
		{
			Application.Run(_form);
		}

		private void InitBoard()
		{
			BoardControl boardControl = new BoardControl();
			_form.SetMainControl(boardControl);
			_form.ClearBoard += HandleClearBoard;

			if (File.Exists(SaveFile))
			{
				try
				{
					using (FileStream fileIn = new FileStream(SaveFile, FileMode.Open, FileAccess.Read))
					{
						_board = new BoardStore().Load(fileIn);
					}
				}
				catch (Exception ex)
				{
					LogError(ex);
				}
			}
			if (_board == null)
			{
				_board = new Board(BoardSize);
			}

			new BoardController(_board, boardControl);
		}

		private void HandleClearBoard(EventArgs e)
		{
			_board.Clear();
			_form.Refresh();
		}

		private void HandleThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
            LogError(e.Exception);
		}

		static void LogError(Exception exception)
		{
			using (StreamWriter fileOut = new StreamWriter("last_error.txt"))
			{
				fileOut.WriteLine(exception.ToString());
			}
		}

		private void HandleApplicationExit(object sender, EventArgs e)
		{
			try
			{
				using (FileStream fileOut = new FileStream(SaveFile, FileMode.Create))
				{
					new BoardStore().Save(fileOut, _board);
				}
			}
			catch
			{
				File.Delete(SaveFile);
				throw;
			}
		}
	}
}
