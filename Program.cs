using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

bool _packIsFull = false;

Console.WriteLine("**** Creating a new pack ****");

var _maxItemCount = (int)GetUserInput("Enter a max item count: ");
var _maxVolume = GetUserInput("Enter a max volume: ");
var _maxWieght = GetUserInput("Enter a max weight: ");

Console.Clear();

Pack _pack = new Pack(_maxItemCount, _maxVolume, _maxWieght);

Console.WriteLine($"You created a new pack with a\nmax item count of {_maxItemCount},\nmax volume of {_maxVolume},\nand a max weight of {_maxWieght}.");

Console.Write("Hit any key to continue...");
Console.ReadKey();
Console.Clear();

var _menuSelection = 0;
do
{
    ShowMenu();
    Console.Write("Select an item to add to your pack: ");
    _menuSelection = Int16.Parse(Console.ReadLine());
    Console.Clear();
    switch (_menuSelection)
    {
        case 1:
            _pack.Add(new Arrow());
            break;
        case 2:
            _pack.Add(new Bow());
            break;
        case 3:
            _pack.Add(new Rope());
            break;
        case 4:
            _pack.Add(new Food());
            break;
        case 5:
            _pack.Add(new Water());
            break;
        case 6:
            _pack.Add(new Sword());
            break;
        default:
            _menuSelection = 0;
            break;
    }
    _pack.ShowPack();

    if ((_pack.CurrentItemCount == _pack.MaxItemCount) || (_pack.CurrentVolume == _pack.MaxVolume) || (_pack.CurrentWeight == _pack.MaxWeight))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"The pack is full");
        Console.ForegroundColor = ConsoleColor.White;
        _packIsFull = true;
    }

} while (_menuSelection != 0 && !_packIsFull);

Console.WriteLine("Thanks for playing! (press any key to continue)");
Console.ReadKey();

#region PRIVATE METHODS
void ShowMenu()
{
    Console.WriteLine("Available items:\n1. Arrow (V:0.05 W:0.1)\n2. Bow (V:4 W:1)\n3. Rope (V:1.5 W:1)\n4. Food (V:3 W:2)\n5. Water (V:0.5 W:1)\n6. Sword (V:3 W:5)");
}

float GetUserInput(string message)
{
    Console.Write(message);
    Task.Delay(1000);
    string input = Console.ReadLine();
    float value = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);

    return value;
}
#endregion

#region CLASSES
public class Pack
{
    public int MaxItemCount { get; }
    public float MaxVolume { get; }
    public float MaxWeight { get; }
    public int CurrentItemCount { get; private set; }
    public float CurrentVolume { get; private set; }
    public float CurrentWeight { get; private set; }
    public bool PackIsFull { get; private set; }

    ObservableCollection<InventoryItem> inventoryItems = new ObservableCollection<InventoryItem>();


    #region CONSTRUCTOR
    public Pack(int maxItemCount, float maxVolume, float maxWeight)
    {
        MaxItemCount = maxItemCount;
        MaxVolume = maxVolume;
        MaxWeight = maxWeight;
    }
    #endregion

    #region METHODS
    public bool Add(InventoryItem item)
    {

        if ((CurrentItemCount < MaxItemCount) && ((item.Volume + CurrentVolume) <= MaxVolume) && ((item.Weight + CurrentWeight) <= MaxWeight))
        {

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Adding a {item.GetType()}");
            Console.ForegroundColor = ConsoleColor.White;
            CurrentItemCount++;
            CurrentVolume += item.Volume;
            CurrentWeight += item.Weight;

            return true;
        }
        
        
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Cannot add {item.GetType()}. The pack is too full");
            Console.ForegroundColor = ConsoleColor.White;
            return false;
        
    }

    public void ShowPack()
    {
        Console.WriteLine("********** Pack Stats **********");
        Console.WriteLine($"Current Item Count: {CurrentItemCount} of {MaxItemCount}.");
        Console.WriteLine($"Current Volume:     {CurrentVolume} of {MaxVolume} max.");
        Console.WriteLine($"Current Weight:     {CurrentWeight} of {MaxWeight} max.");
        Console.WriteLine("********************************");
    }

    #endregion

}


public class InventoryItem
{
    public float Weight { get; set; }
    public float Volume { get; set; }

    public InventoryItem(float weight, float volume)
    {
        Weight = weight;
        Volume = volume;
    }
}

public class Arrow : InventoryItem
{
    public Arrow() : base(0.1f, 0.05f)
    {

    }
}

public class Bow : InventoryItem
{
    public Bow() : base(1, 4)
    {

    }
}

public class Rope : InventoryItem
{
    public Rope() : base(1, 1.5f)
    {

    }
}

public class Water : InventoryItem
{
    public Water() : base(2, 3)
    {

    }
}

public class Food : InventoryItem
{
    public Food() : base(1, 0.5f)
    {

    }
}

public class Sword : InventoryItem
{
    public Sword() : base(5, 3)
    {

    }
}
#endregion