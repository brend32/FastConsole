using System.Drawing;

namespace Game.Obstacles;

public class Rock : Obstacle
{
	public override void Update()
	{
		base.Update();
		Canvas.Fill(Color.Azure);
	}
}