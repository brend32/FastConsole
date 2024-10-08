using System.Drawing;
using FastConsole.Engine.Elements;

namespace Game.Obstacles;

public abstract class Obstacle : Element
{
	public Canvas Canvas { get; set; }

	public Obstacle()
	{
		Canvas = new Canvas(new Size(1, 1));
	}

	public override void Update()
	{
		Canvas.CanvasSize = Size;
		Canvas.Position = new Point(Position.X * 2, Position.Y);
		
		Canvas.Update();
	}

	protected override void OnRender()
	{
		Canvas.Render();
	}
}