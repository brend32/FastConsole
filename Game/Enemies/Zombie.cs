namespace Game.Enemies;

public class Zombie : EnemyBase
{
	public Zombie()
	{
		Health = 100;
		MaxHealth = 100;
		Damage = 25;
		
		_name.Value = "Zombie";
	}


	public override Decision MakeTurn(FightingArea area)
	{
		return new Decision("Attack", () =>
		{
			area.Player.ReceiveDamage(Damage);
		});
	}
}