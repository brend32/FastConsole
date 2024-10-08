using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;
using Game.Elements;
using Game.Enemies;

namespace Game;

public interface IFightTurnEndListener
{
	void OnTurnEnded();
}

public interface IFightParticipant
{
	bool IsAlive { get; }
	bool Highlighted { get; set; }
	Decision MakeTurn(FightingArea area);
} 

public class FightingArea : Element
{
	public Player Player { get; set; }
	public List<EnemyBase> Enemies { get; set; }
	public bool IsFighting { get; set; }
	public bool IsPlayerWin { get; set; }

	public IFightParticipant Participant
	{
		get
		{
			if (_participantIndex >= TurnsInCycle)
				return null;

			if (_participantIndex == 0)
				return Player;

			return Enemies[_participantIndex - 1];
		}
	}

	private FlexBox _enemiesFlexBox;
	private FlexBox _playerFlexBox;
	private FlexBox _arenaFlexBox;
	private Text _madeDecision;
	private double _timeForNextUpdate;
	
	private int TurnsInCycle => 1 + Enemies.Count;
	private int _participantIndex;
	
	public FightingArea(Player player)
	{
		Player = player;
		player.FightingArea = this;
		Enemies = new List<EnemyBase>();

		_enemiesFlexBox = new FlexBox()
		{
			GrowDirection = GrowDirection.Horizontal,
			Alignment = Alignment.Center,
			Spacing = 1,
			AlwaysRecalculate = true
		};
		_playerFlexBox = new FlexBox()
		{
			GrowDirection = GrowDirection.Horizontal,
			Alignment = Alignment.Center,
			Spacing = 1,
			AlwaysRecalculate = true
		};

		_arenaFlexBox = new FlexBox()
		{
			GrowDirection = GrowDirection.Vertical,
			Alignment = Alignment.Center,
			AlwaysRecalculate = true
		};

		_madeDecision = new Text()
		{
			Size = new Size(16, 3),
			Alignment = Alignment.Center,
			Foreground = Color.Coral
		};
		
		_playerFlexBox.Children.Add(player.AtArenaRenderer);
		_arenaFlexBox.Children.Add(_enemiesFlexBox);
		_arenaFlexBox.Children.Add(_madeDecision);
		_arenaFlexBox.Children.Add(_playerFlexBox);
	}
	
	public override void Update()
	{
		_playerFlexBox.Size = new Size(Size.Width, 7);
		_enemiesFlexBox.Size = new Size(Size.Width, 7);

		_arenaFlexBox.Spacing = 2;

		_arenaFlexBox.Size = Size;
		_arenaFlexBox.Position = Position;
		
		_arenaFlexBox.Update();

		if (IsFighting && Time.NowSeconds > _timeForNextUpdate)
		{
			ProcessCycle();
		}
	}

	protected override void OnRender()
	{
		_arenaFlexBox.Render();
	}

	public void StartFight()
	{
		IsFighting = true;
		_participantIndex = 0;

		Enemies.Clear();
		int enemiesAmount = 3;//Random.Shared.Next(1, 3);
		for (int i = 0; i < enemiesAmount; i++)
		{
			Enemies.Add(GetRandomEnemy());
		}

		_enemiesFlexBox.Children.Clear();
		foreach (EnemyBase enemy in Enemies)
		{
			_enemiesFlexBox.Children.Add(enemy);
			enemy.Size = new Size(24, 4);
		}

		_timeForNextUpdate = -1;
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
		SyncHighlightState();
		if (_participantIndex >= TurnsInCycle)
		{
			EndTurn();
		}
		else
		{
			var participant = Participant;
			if (participant == null)
			{
				_participantIndex++;
				return;
			}

			if (participant.IsAlive == false)
			{
				_participantIndex++;
				return;
			}
			
			var decision = participant.MakeTurn(this);
			if (decision == null)
			{
				_madeDecision.Value = "Waiting for turn...";
				return;
			}

			_madeDecision.Value = decision.Name;
			decision.PerformAction();
			_timeForNextUpdate = Time.NowSeconds + 3;
			_participantIndex++;
		}
	}

	private void SyncHighlightState()
	{
		Player.Highlighted = _participantIndex == 0;

		for (int i = 0; i < Enemies.Count; i++)
		{
			Enemies[i].Highlighted = i == _participantIndex - 1;
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
			_madeDecision.Value = "Enemies win";
		}
		else if (Enemies.All(enemy => enemy.IsAlive == false))
		{
			IsFighting = false;
			IsPlayerWin = true;
			_madeDecision.Value = "Player win";
		}
	}

	public void HandleInput(ConsoleKey key)
	{
		if (Participant is Player player)
		{
			player.HandleInput(key);
		}
	}
}