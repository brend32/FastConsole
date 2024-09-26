using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;

namespace Game;

public class SettingsScene : Scene
{
	public SettingsScene()
	{
		Elements.Add(new Text()
		{
			Size = new Size(50, 12),
			Value = "Adventure game \nVersion: 0.1 \n \nShowcase for GoIteens \n \nPress E to go back...",
			Alignment = Alignment.Center
		});
	}
	
	public override void Update()
	{
		
	}
}