# FastConsole
The project created to simplify rendering in a console application. It provides zero flikering fast rendering solution.

![](/Images/Image_1.png)

## Technics
- Buffers - library renders content into a buffer and then outputs changed parts to console. It ensures console updates only parts that changed
- string.Create - combines buffer lines into single string to minimize calls to Console.Write.
- Elements hierarchy - add elements to list and build hierarchy to achive modern app expirience.
- Extended color palete - extends possible colors space for supported terminals.

## Repo structure
- FastConsole - library project
- FastConsole.Examples - examples how to use the library
- Game - example how to create a game using the library

## Examples

![](/Images/Image_2.png)

![](/Images/Image_3.png)

![](/Images/Image_4.png)

![](/Images/Image_5.png)

![](/Images/Image_6.png)
