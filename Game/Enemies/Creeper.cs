namespace Game.Enemies;

public class Creeper : EnemyBase, IFightTurnEndListener
{
	public int TimeToExplosion { get; set; } = 4;

	public Creeper()
	{
		Health = 200;
		MaxHealth = 200;
		Damage = 350;
	}
	
	protected override void OnRender()
	{
		
	}

	public override void ReceiveDamage(int amount)
	{
		if (Random.Shared.NextDouble() > 0.90)
		{
			ResetExplosionTimer();
		}
		
		base.ReceiveDamage(amount);
	}

	public void ResetExplosionTimer()
	{
		TimeToExplosion = 4;
	}

	public override Decision MakeTurn(FightingArea fightingArea)
	{
		if (TimeToExplosion <= 0)
		{
			return new Decision("Explode", () =>
			{
				Kill();

				fightingArea.Player.ReceiveDamage(Damage);
				foreach (EnemyBase enemy in fightingArea.Enemies.Where(enemy => enemy != this && enemy.IsAlive))
				{
					enemy.ReceiveDamage(Damage);
				}
			});
		}

		return new Decision("Skip", () => { });
	}

	public void OnTurnEnded()
	{
		if (TimeToExplosion > 0)
		{
			TimeToExplosion--;
		}
	}
}