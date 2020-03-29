using System.Collections.Generic;
using escape_corona.Interfaces;

namespace escape_corona.Models
{
  class Room : IRoom
  {
    //properties
    public string Name { get; set; }
    public string Description { get; set; }
    public List<IItem> Items { get; set; }
    public List<IItem> HiddenItems { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public Dictionary<IItem, KeyValuePair<string, IRoom>> LockedExits { get; set; }

    //constructor
    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<IItem>();
      HiddenItems = new List<IItem>();
      Exits = new Dictionary<string, IRoom>();
      LockedExits = new Dictionary<IItem, KeyValuePair<string, IRoom>>();
    }
    public void AddLockedRoom(IItem key, string direction, IRoom room)
    {
      var lockedRoom = new KeyValuePair<string, IRoom>(direction, room);
      LockedExits.Add(key, lockedRoom);
    }

    // this is being called from game services
    public string Use(IItem item)
    {
      if (LockedExits.ContainsKey(item))
      {
        //unlock with item, remove from one dict, place in other
        Exits.Add(LockedExits[item].Key, LockedExits[item].Value);
        LockedExits.Remove(item);
        string actionMessage = "";
        switch (item.Name.ToLower())
        {
          case "appointment":
            actionMessage = @"You have unlocked the door to the pharmacy.
    Now that it is your appointment time,enter the pharmacy and get a vaccination";
            break;
          case "receipt":
            actionMessage = @"You have unlocked the door to leave the store.
    Now that you have checked out, you may leave";
            break;
          default:
            actionMessage = "You have unlocked a room";
            break;
        }
        return actionMessage;
      }
      //maybe have else if for local items like light switch
      return "No use for that here";
    }


  }
}