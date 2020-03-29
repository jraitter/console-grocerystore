using System;
using System.Collections.Generic;
using System.Linq;
using bb_mvc.Models;
using escape_corona.Interfaces;
using escape_corona.Models;

namespace escape_corona.Services
{
  class GameService : IGameService
  {
    public List<Message> Messages { get; set; }
    private IGame _game { get; set; }

    //constructor
    public GameService(string playerName)
    {
      Messages = new List<Message>();
      _game = new Game();
      _game.CurrentPlayer = new Player(playerName);
      Messages.Add(new Message(Utils.ObjectiveMessage, ConsoleColor.Red));

      //Look();
    }

    public bool Go(string direction)
    {
      //if the current room has that direction on the exits dictionary
      if (_game.CurrentRoom.Exits.ContainsKey(direction))
      {
        // set current room to the exit room
        _game.CurrentRoom = _game.CurrentRoom.Exits[direction];
        // populate messages with room description
        Messages.Add(new Message($"You traveled {direction}, and discover: "));
        Look();
        EndRoom end = _game.CurrentRoom as EndRoom;
        if (end != null)
        {
          Messages.Add(new Message(end.Narrative));
          return false;
        }
        return true;
      }
      //no exit in that direction
      Messages.Add(new Message("No Room in that direction"));
      Look();
      return true;
    }

    public void Help()
    {
      Messages.Add(new Message(Utils.HelpMessage, ConsoleColor.DarkGreen));
    }

    public void Inventory()
    {
      Messages.Add(new Message("Current Inventory: "));
      foreach (var item in _game.CurrentPlayer.Inventory)
      {
        Messages.Add(new Message($"{item.Name} - {item.Description}"));
      }
    }
    public void Checkout()
    {
      int foundCount = 0;

      if ((string)_game.CurrentRoom.Name != "Checkout")
      {
        Messages.Add(new Message("Must be at cash register to checkout"));
        return;
      }

      //inventory must contain tp, hs, eggs, vacc to check out.
      foreach (var item in _game.CurrentPlayer.Inventory)
      {
        if ((string)item.Name == "Toilet Paper" || (string)item.Name == "Hand Sanatizer" || (string)item.Name == "Eggs" || (string)item.Name == "Vaccination")
        {
          foundCount++;
        }
      }//endof foreach

      if (foundCount < 4)
      {
        Messages.Add(new Message("You do not have all the proper items in your invetory, please continue to shop"));
        return;
      }
      //otherwise hide receipt in inventory to be used
      var found = _game.CurrentRoom.HiddenItems.Find(i => i.Name == "Receipt");
      if (found == null)
      {
        Messages.Add(new Message("crash and burn on null reference"));
        return;
      }
      _game.CurrentPlayer.Inventory.Add(found);

    }//endof checkout


    public void Look()
    {
      Messages.Add(new Message("You are in " + _game.CurrentRoom.Name + "\n"));
      Messages.Add(new Message(_game.CurrentRoom.Description));
      if (_game.CurrentRoom.Items.Count > 0)
      {
        Messages.Add(new Message("There may be a few things in this room:"));
        foreach (var item in _game.CurrentRoom.Items)
        {
          Messages.Add(new Message("     " + item.Name));
        }
      }
      string exits = string.Join(", ", _game.CurrentRoom.Exits.Keys);
      Messages.Add(new Message("There are exits to the " + exits));

      string lockedExits = "";
      foreach (var lockedRoom in _game.CurrentRoom.LockedExits.Values)
      {
        lockedExits += lockedRoom.Key;
      }
      if (lockedExits != "")
      {
        Messages.Add(new Message("There are locked exits to the " + lockedExits));
      }

    }

    public void Reset()
    {
      string name = _game.CurrentPlayer.Name;
      _game = new Game();
      _game.CurrentPlayer = new Player(name);
    }

    public void Take(string itemName)
    {
      IItem found = _game.CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName);
      if (found != null)
      {
        _game.CurrentPlayer.Inventory.Add(found);
        _game.CurrentRoom.Items.Remove(found);
        Messages.Add(new Message($"You have taken the {itemName}"));
        return;
      }
      Messages.Add(new Message("Cannot find item by that name"));
    }

    public void Use(string itemName)
    {
      //check if item is in current inventory
      var found = _game.CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName);
      if (found != null)
      {
        //call Use method in Room,  pass item, return string
        Messages.Add(new Message(_game.CurrentRoom.Use(found)));
        return;
      }
      // check if item is in room like a switch
      Messages.Add(new Message("You don't have that Item"));
    }

  }//endof class
}//endof namespace