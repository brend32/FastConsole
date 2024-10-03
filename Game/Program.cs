using System.Diagnostics;
using FastConsole.Engine.Core;
using Game;

class Program
{
	public static void Main()
	{
		Windows.ForceUpgradeToAnsi();
		
		SceneManager.OpenScene(new MenuScene());
		SceneManager.Run();
	}
}