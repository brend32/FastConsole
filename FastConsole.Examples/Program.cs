using FastConsole.Examples.Examples;

List<(string Name, Action Run)> examples = new List<(string, Action)>()
{
	("Hello world", E01_HelloWorld.Run),
    ("FPS", E02_Fps.Run),
    ("Text styles", E03_TextStyles.Run),
    ("Box", E04_Box.Run),
    ("Flex box", E05_FlexBox.Run),
    ("Custom element", E06_CustomElement.Run),
    ("Canvas", E07_Canvas.Run),
};

int selectedIndex = 0;

while (true)
{
	Console.Clear();
	
	for (int i = 0; i < examples.Count; i++)
	{
		if (i == selectedIndex)
		{
			Console.BackgroundColor = ConsoleColor.Green;
		}
		Console.WriteLine($"{i + 1}: {examples[i].Name}");
		Console.ResetColor();
	}

	ConsoleKeyInfo key = Console.ReadKey(true);
	switch (key.Key)
	{
		case ConsoleKey.UpArrow:
			selectedIndex = (selectedIndex - 1 + examples.Count) % examples.Count;
			break;
		
		case ConsoleKey.DownArrow:
			selectedIndex = (selectedIndex + 1) % examples.Count;
			break;
		
		case ConsoleKey.Enter:
			examples[selectedIndex].Run();
			break;
	}
}