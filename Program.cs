using System;
using System.IO;
using System.Windows.Forms;
using Goban.Model;

namespace Goban
{
	class Program
	{
		private const string SaveFile = "game.sav";
		private const int BoardSize = 19;
		private const bool DebugMode = true;

		private static Board _board;

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			MainForm form = InitGame();

			if (!DebugMode)
			{
				Application.ThreadException += HandleThreadException;
			}
			Application.ApplicationExit += HandleApplicationExit;
			Application.Run(form);
		}

		private static MainForm InitGame()
		{
			MainForm form = new MainForm();
			BoardControl boardControl = new BoardControl();
			form.SetMainControl(boardControl);

			if (File.Exists(SaveFile))
			{
				try
				{
					using (FileStream fileIn = new FileStream(SaveFile, FileMode.Open, FileAccess.Read))
					{
						_board = new BoardStore().Load(fileIn);
					}
				}
				catch(Exception ex)
				{
					LogError(ex);
				}
			}
			if (_board == null)
			{
				_board = new Board(BoardSize);
			}

			new BoardController(_board, boardControl);
			return form;
		}

		static void HandleThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
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

		static void HandleApplicationExit(object sender, EventArgs e)
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
