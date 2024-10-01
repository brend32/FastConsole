using FastConsole.Engine.Elements;
using Game.Enemies;

namespace Game;

public interface IFightTurnEndListener
{
	void OnTurnEnded();
}

public class FightingArea : Element
{
	public Player Player { get; set; }
	public List<EnemyBase> Enemies { get; set; }
	public bool IsFighting { get; set; }
	public bool IsPlayerWin { get; set; }
	
	private int TurnsInCycle => 1 + Enemies.Count;
	private int _participantIndex;
	
	public FightingArea(Player player)
	{
		Player = player;
		Enemies = new List<EnemyBase>();
	}
	
	public override void Update()
	{
		
	}

	protected override void OnRender()
	{
		
	}

	public void StartFight()
	{
		IsFighting = true;
		_participantIndex = 0;

		Enemies.Clear();
		int enemiesAmount = Random.Shared.Next(1, 3);
		for (int i = 0; i < enemiesAmount; i++)
		{
			Enemies.Add(GetRandomEnemy());
		}

		// TODO: Refactor
		while (IsFighting)
		{
			ProcessCycle();
		}
	}

	private EnemyBase GetRandomEnemy()
	{
		return Random.Shared.Next(0, 3) switch
		{
			0 => new Zombie(),
			1 => new Skeleton(),
			2 => new Creeper()
		};
	}

	private void ProcessCycle()
	{
		if (_participantIndex >= TurnsInCycle)
		{
			EndTurn();
		}
		else
		{
			MakeTurn();
			_participantIndex++;
		}
	}

	private void MakeTurn()
	{
		if (_participantIndex == 0)
		{
			Player.MakeTurn(this).PerformAction();
		}
		else
		{
			Enemies[_participantIndex - 1].MakeTurn(this).PerformAction();
		}
	}

	private void EndTurn()
	{
		if (Player is IFightTurnEndListener player)
		{
			player.OnTurnEnded();
		}

		foreach (EnemyBase enemy in Enemies)
		{
			if (enemy is IFightTurnEndListener listener)
			{
				listener.OnTurnEnded();
			}
		}

		_participantIndex = 0;
		TryEndFight();
	}

	private void TryEndFight()
	{
		if (Player.IsAlive == false)
		{
			IsFighting = false;
			IsPlayerWin = false;
		}
		else if (Enemies.All(enemy => enemy.IsAlive == false))
		{
			IsFighting = false;
			IsPlayerWin = true;
		}
	}
}