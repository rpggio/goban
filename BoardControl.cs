using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Goban.Model;

namespace Goban
{
	class BoardControl : Control
	{
		private const float StoneDiameterFactor = 0.7f;
		private const float GridMarginFactor = 0.05f;

		private Board _board;
		private readonly Color LineColor = Color.Olive;
		private readonly Color BackgroundColor = Color.FromArgb(244, 214, 154);
		public event Action<Position> PositionClick;
		public event Action<Position> PositionDeleteCommand;
			
		public BoardControl()
		{
			Initialize();
		}
		
		private void Initialize()
		{
			BackColor = BackgroundColor;
			DoubleBuffered = true;
			Click += BoardControl_Click;
			KeyPress += BoardControl_KeyPress;
		}

		void BoardControl_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 'd')
			{
				Nullable<Position> position = FindPosition(PointToClient(Cursor.Position));
				if (position != null)
				{
					e.Handled = true;
					OnPositionDeleteCommand(position.Value);
				}
			}
		}

		void BoardControl_Click(object sender, EventArgs e)
		{
			Nullable<Position> position = FindPosition(PointToClient(Cursor.Position));
			if (position != null)
			{
				OnPositionClick(position.Value);
			}
		}

		private Nullable<Position> FindPosition(Point point)
		{
			Position position = GetNearest(point);
			if (GetStoneRegion(position).IsVisible(point))
			{
				return position;
			}
			return null;
		}
		
		private Position GetNearest(Point point)
		{
			int col = (int)Math.Round((point.X - GridBoundary.Left) / SquareSize);
			int row = (int)Math.Round((point.Y - GridBoundary.Top) / SquareSize);
			return new Position(col, row);
		}

		public void SetModel(Board board)
		{
			_board = board;
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			RectangleF gridBoundary = GridBoundary;

			float squareSize = SquareSize;
			System.Drawing.Color lineColor = LineColor;
			for(int i = 0 ; i < _board.Size ; i++)
			{
			    e.Graphics.DrawLine(
			        new Pen(lineColor),
			        gridBoundary.Left,
			        gridBoundary.Top + i * squareSize,
			        gridBoundary.Right,
			        gridBoundary.Top + i * squareSize);
			    e.Graphics.DrawLine(
			        new Pen(lineColor),
			        gridBoundary.Left + i * squareSize,
			        gridBoundary.Top,
			        gridBoundary.Left + i * squareSize,
			        gridBoundary.Bottom);
			}

			_board.Accept(new StoneDrawingVisitor(e.Graphics, DrawStone));
		}

		private float SquareSize
		{
			get { return GridBoundary.Height / (_board.Size - 1); }
		}

		private float GridMargin
		{
			get { return DrawingSize * GridMarginFactor; }
		}
		
		private int DrawingSize
		{
			get { return Math.Min(Width, Height);  }
		}
		
		private float StoneDiameter
		{
			get { return SquareSize * StoneDiameterFactor; }
		}
		
		private RectangleF GridBoundary
		{
			get
			{
				float margin = GridMargin;
				return new RectangleF(
					new PointF(
						Left + margin,
						Top + margin),
					new SizeF(
						DrawingSize - 2f * margin,
						DrawingSize - 2f * margin));
			}
		}

		protected void DrawStone(Graphics graphics, Position position, Group group)
		{
			Brush fill;
			switch(group.Stone)
			{
				case Stone.White:
					fill = new SolidBrush(Color.WhiteSmoke);
					break;
				case Stone.Black:
					fill = new SolidBrush(Color.Black);
					break;
				default:
					throw new InvalidOperationException("Unhandled color: " + group.Stone);
			}
			PointF center = GetPostionLoc(position);
			RectangleF region = new RectangleF(
				center.X - StoneDiameter * 0.5f,
				center.Y - StoneDiameter * 0.5f,
				StoneDiameter,
				StoneDiameter);
			graphics.FillEllipse(fill, region);
			graphics.DrawEllipse(new Pen(Color.Black), region);
		}

		protected GraphicsPath GetStoneRegion(Position position)
		{
			GraphicsPath path = new GraphicsPath();
			PointF loc = GetPostionLoc(position);
			path.AddEllipse(
				new RectangleF(
                	loc.X - StoneDiameter * 0.5f,
					loc.Y - StoneDiameter * 0.5f,
                	StoneDiameter, 
                	StoneDiameter));
			return path;
		}
		
		private PointF GetPostionLoc(Position position)
		{
			return new PointF(GridBoundary.Left + position.Column * SquareSize,
				GridBoundary.Top + position.Row * SquareSize);
		}
		
		protected void OnPositionClick(Position position)
		{
			if (PositionClick != null)
			{
				PositionClick(position);
			}
		}

		protected void OnPositionDeleteCommand(Position position)
		{
			if (PositionDeleteCommand != null)
			{
				PositionDeleteCommand(position);
			}
		}
	}

	delegate void VertexDraw(Graphics graphics, Position position, Group group);
	
	class StoneDrawingVisitor : IPositionVisitor
	{
		private Graphics _graphics;
		private VertexDraw _method;

		public StoneDrawingVisitor(Graphics graphics, VertexDraw method)
		{
			_graphics = graphics;
			_method = method;
		}

		public void Visit(Position position, Group group)
		{
			_method(_graphics, position, group);
		}
	}
}
