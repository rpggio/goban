using System;
using System.Windows.Forms;

namespace Goban
{
	public partial class MainForm : Form
	{
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
	}
}