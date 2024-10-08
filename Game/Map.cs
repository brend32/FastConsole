using System.Drawing;
using FastConsole.Engine.Elements;
using Game.Obstacles;

namespace Game;

public class Map : Element
{
	public Size MapSize => _canvas.CanvasSize;

	private List<Obstacle> _obstacles = new List<Obstacle>();
	private Canvas _canvas;

	public Map()
	{
		_canvas = new Canvas(new Size(60, 20))
		{
			CellWidth = 2
		};
		_canvas.Fill(Color.Green);
		
		SpawnObstacles();
	}

	private void SpawnObstacles()
	{
		int rocksAmount = 30;
		for (int i = 0; i < rocksAmount; i++)
		{
			_obstacles.Add(new Rock()
			{
				Position = new Point(Random.Shared.Next(0, _canvas.CanvasSize.Width), Random.Shared.Next(0, _canvas.CanvasSize.Height)),
				Size = new Size(1, 1)
			});
		}
	}

	public bool IsPointInsideMap(Point point)
	{
		if (point.X < 0 || point.Y < 0)
			return false;

		if (point.X >= MapSize.Width || point.Y >= MapSize.Height)
			return false;

		return true;
	}

	public bool IsPointWalkable(Point point)
	{
		foreach (Obstacle obstacle in _obstacles)
		{
			if (obstacle.Position.X <= point.X && obstacle.Position.X + obstacle.Size.Width - 1 >= point.X &&
			    obstacle.Position.Y <= point.Y && obstacle.Position.Y + obstacle.Size.Height - 1 >= point.Y)
			{
				return false;
			}
		}

		return true;
	}

	public override void Update()
	{
		_canvas.Position = Position;

		foreach (Obstacle obstacle in _obstacles)
		{
			obstacle.Update();
		}
		
		_canvas.Update();
	}

	protected override void OnRender()
	{
		_canvas.Render();
		
		foreach (Obstacle obstacle in _obstacles)
		{
			obstacle.Render();
		}
	}
}