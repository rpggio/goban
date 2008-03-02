using System;
using System.Windows.Forms;

namespace Goban
{
	public partial class MainForm : Form
	{
		public event Action<EventArgs> ClearBoard;

		public MainForm()
		{
			InitializeComponent();
		}
		
		public void SetMainControl(Control control)
		{
			panelMain.Controls.Clear();
			control.Dock = DockStyle.Fill;
			panelMain.Controls.Add(control);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Refresh();
		}

		private void buttonClearBoard_Click(object sender, EventArgs e)
		{
			if (ClearBoard != null)
			{
				ClearBoard(e);
			}
		}
	}
}