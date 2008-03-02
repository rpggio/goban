using System;
using System.Windows.Forms;

namespace Goban
{
	class Program
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			MainForm form = new MainForm();
			new ApplicationController(form).Start();
		}
	}
}
