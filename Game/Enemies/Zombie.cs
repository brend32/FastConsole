namespace Game.Enemies;

public class Zombie : EnemyBase
{
	public Zombie()
	{
		Health = 100;
		MaxHealth = 100;
		Damage = 25;
	}
	
	protected override void OnRender()
	{
		
	}

	public override Decision MakeTurn(FightingArea area)
	{
		return new Decision("Attack", () =>
		{

		});
	}
}