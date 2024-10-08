using System.Drawing;
using FastConsole.Engine.Elements;
using Game.Elements;
using Game.Enemies;

namespace Game;

public class Player : Element, IFightTurnEndListener, IEntity, IFightParticipant
{
	public int Health { get; set; }
	public int MaxHealth { get; set; }
	public int Damage { get; set; }
	public int ShieldLifeTime { get; set; }
	public bool HasShield => ShieldLifeTime > 0;
	public bool Highlighted { get; set; }
	public FightingArea FightingArea { get; set; }

	public PlayerAtArena AtArenaRenderer => _playerAtArena;
	
	public bool IsAlive => Health > 0;

	private Canvas _canvas;
	private Map _map;
	private PlayerAtArena _playerAtArena;

	public Player(int health, int damage, Map map)
	{
		_map = map;
		Health = health;
		MaxHealth = health;
		Damage = damage;
		
		_playerAtArena = new PlayerAtArena(this)
		{
			Size = new Size(16, 4)
		};

		_canvas = new Canvas(new Size(1, 1))
		{
			CellWidth = 2,
		};
		_canvas.Fill(Color.Purple, Color.FromArgb(153, 222, 35), '@');
	}

	public void Heal(int amount)
	{
		Health += amount;
		Health = Math.Min(Health, MaxHealth);
	}

	public void ReceiveDamage(int amount)
	{
		if (HasShield)
		{
			amount = (int)Math.Ceiling(0.35 * amount);
		}

		Health -= amount;
	}

	public void ActivateShield()
	{
		ShieldLifeTime = 3;
	}

	public Decision MakeTurn(FightingArea fightingArea)
	{
		return _playerAtArena.MakeTurn(fightingArea);
	}

	public void Move(Point delta)
	{
		Point newPosition = new Point(Position.X + delta.X, Position.Y + delta.Y);

		if (_map.IsPointInsideMap(newPosition))
		{
			Position = newPosition;
		}
	}

	public override void Update()
	{
		_canvas.Position = new Point(Position.X * 2, Position.Y);
		_canvas.Update();
	}

	protected override void OnRender()
	{
		_canvas.Render();
	}

	public void OnTurnEnded()
	{
		if (ShieldLifeTime > 0)
		{
			ShieldLifeTime--;
		}
	}

	public void HandleInput(ConsoleKey key)
	{
		if (FightingArea?.IsFighting == true)
		{
			_playerAtArena.HandleInput(key);
		}
	}
}