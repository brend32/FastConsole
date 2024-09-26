using System.Drawing;
using FastConsole.Engine.Elements;

namespace Game;

public class Player : Element
{
	public int Health { get; set; }
	public int MaxHealth { get; set; }
	public int Damage { get; set; }

	public bool IsAlive => Health > 0;

	private Canvas _canvas;

	public Player(int health, int damage)
	{
		Health = health;
		MaxHealth = health;
		Damage = damage;

		_canvas = new Canvas(new Size(1, 1))
		{
			CellWidth = 2,
		};
		_canvas.Fill(Color.Purple, Color.FromArgb(153, 222, 35), '@');
	}

	public override void Update()
	{
		_canvas.Position = Position;
		_canvas.Update();
	}

	protected override void OnRender()
	{
		_canvas.Render();
	}
}