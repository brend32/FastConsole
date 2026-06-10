using System.Drawing;
using FastConsole.Engine.Core;
using FastConsole.Engine.Elements;
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

List<Element> elements = new();
List<Text> options = new();

var mode = new Text()
{
    Size = new Size(20, 2),
    Foreground = Color.Brown,
    Value = "Mode: Fast renderer"
};

var flexBox = new FlexBox()
{
    Size = new Size(20, 40),
    GrowDirection = GrowDirection.Vertical,
    Spacing = 0,
    AlwaysRecalculate = true
};
flexBox.Children.Add(mode);

for (var i = 0; i < examples.Count; i++)
{
    var tuple = examples[i];
    var text = new Text()
    {
        Value = $"{i + 1}: {tuple.Name}",
        Size = new Size(20, 1),
        Foreground = Color.White
    };

    options.Add(text);
    flexBox.Children.Add(text);
}

flexBox.Children.Add(new Text()
{
    Value = "Press M to switch mode",
    Foreground = Color.Aqua,
    Size = new Size(25, 2),
    Alignment = Alignment.BottomLeft
});

elements.Add(flexBox);

int selectedIndex = 0;
bool isFastMode = true;

Windows.ForceUpgradeToAnsi();

while (true)
{
    if (isFastMode)
    {
        FastRenderer();
    }
    else
    {
        NonFastRenderer();
    }

    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        switch (key.Key)
        {
            case ConsoleKey.M:
                Renderer.ClearScreen();
                isFastMode ^= true;
                break;

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
}

void NonFastRenderer()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine("Mode: Console.WriteLine");
    Console.WriteLine();
    Console.ResetColor();

    for (int i = 0; i < examples.Count; i++)
    {
        if (i == selectedIndex)
        {
            Console.BackgroundColor = ConsoleColor.Green;
        }

        Console.WriteLine($"{i + 1}: {examples[i].Name}");
        Console.ResetColor();
    }
    
    Console.WriteLine();
    Console.WriteLine("Press M to switch mode");

    while (Console.KeyAvailable == false)
    {
        
    }
}

void FastRenderer()
{
    if (Time.TryUpdate())
    {
        for (int i = 0; i < options.Count; i++)
        {
            var option = options[i];

            option.Background = i == selectedIndex ? Color.Green : null;
        }
        
        Element.UpdateAndRender(elements);
    }   
}