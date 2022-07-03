# ReaderXML


This is a simple reader XML.

You can use it to have a tree of objects into your execution. (Simple and efficient if your structure of your file XML can be modified at any time !)

<br/><br/>

Here an example with readerXML:

<br/><br/><br/>

### File XML:

```xml

<?xml version="1.0" encoding="utf-8"?>

<players>
    <player name="Seroths">
        <pos x="0.54321" y="5.52" z="-7777.777"></pos>
    </player>

    <player name="Tronic">
        <pos x="-574.54321" y="43.55" z="-342.22"></pos>
    </player>

    <player name="Logic">
        <pos x="1000.591" y="34.79" z="654.579"></pos>
    </player>

    <player name="Romeo">
        <pos x="0.54321" y="5000.346" z=""></pos>
    </player>

    <player name="ravageur">
        <pos x="999999.999" y="500.555" z="0.0"></pos>
    </player>

</players>


```


### File C#:

```cs

public struct Position
{
    public decimal x = 0.0;
    public decimal y = 0.0;
    public decimal z = 0.0;

    public Position(decimal x, decimal y, decimal z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public class Player
{
    public string Name { get; private set; }
    public Position Position { get; private set; }

    public Player(string name, Position position)
    {
        this.Name = name;
        this.Position = position;
    }

    public Player(string name, decimal x, decimal y, decimal z)
    {
        this.Name = name;
        this.Position = new Position(x, y, z);
    }
}

public class Main
{
    public static void main(string args[])
    {
        List<Player> players = new();

        ElementXML elementXML = new Reader().ReadFile("players.xml").ElementsXML[0].ElementsXML[0];

        foreach(ElementXML elementXMLTemp in elementXML.ElementsXML)
        {
            players.Add(new(elementXMLTemp.Name));
        }
    }
}

```