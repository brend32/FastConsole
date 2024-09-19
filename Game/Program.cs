using System.Diagnostics;
using Game;

class Program
{
	public static void Main()
	{
		SceneManager.OpenScene(new MenuScene());
		SceneManager.Run();
	}
}