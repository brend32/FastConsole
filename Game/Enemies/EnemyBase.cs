using FastConsole.Engine.Elements;

namespace Game.Enemies;

public abstract class EnemyBase : Element
{
	public int Health { get; protected set; }
	public int MaxHealth { get; protected set; }
	public int Damage { get; protected set; }

	public bool IsAlive => Health > 0;

	public virtual void Heal(int amount)
	{
		Health += amount;
		Health = Math.Min(Health, MaxHealth);
	}

	public virtual void ReceiveDamage(int amount)
	{
		Health -= amount;
	}

	public void Kill()
	{
		Health = 0;
	}

	public abstract Decision MakeTurn(FightingArea fightingArea);
}