using escape_corona.Interfaces;

namespace escape_corona.Models
{
  class Game : IGame
  {
    public IPlayer CurrentPlayer { get; set; }
    public IRoom CurrentRoom { get; set; }

    ///<summary>Initalizes data and establishes relationships</summary>
    public Game()
    {
      // NOTE ALL THESE VARIABLES ARE REMOVED AT THE END OF THIS METHOD
      // We retain access to the objects due to the linked list


      // NOTE Create all rooms
      Room lobby = new Room("Lobby", "Main entrance to this grocery store");
      Room toiletries = new Room("Toiletries", "items used in washing and taking care of one's body, such as soap, shampoo, and toothpaste.");
      Room travel = new Room("Travel", "See a travel agent to help book you next vaction");
      Room pharmacy = new Room("Pharmacy", "items use for fighting sickness, headaches, and schedule a vaccination");
      Room foods = new Room("Foods", "All food items for cooking breakfast lunch and supper");
      Room checkout = new Room("Checkout", "Use the self-serve registers or wait in line for help");
      EndRoom outside = new EndRoom("Outside", "You made it out and have finished your shopping adventure", true, "Now go home and stay inside for 21 days.  Governor's orders");
      EndRoom vaction = new EndRoom("Vacation to Italy", "You and your spouse will be flown for an all expense trip to Italy", false, "Italy has the most people infected with COVID-19 -- you lose");

      // NOTE Create all Items
      Item tp = new Item("Toilet Paper", "Make sure to get a lot of this, something about fear that makes you poop more");
      Item hs = new Item("Hand Sanatizer", "This is a product that is used to remove germs from the skin of the hands.");
      Item eggs = new Item("Eggs", "Make sure to get a lot of these, something about fear that makes you eat more eggs.  Maybe that is why the need for so much toilet paper?");
      Item vaccination = new Item("Vaccination", "Make sure to get a vacciantion.  It may be your only chance for survival");
      Item appt = new Item("Appointment", "May need to have an appointment in order to get a vaccination");

      // NOTE Make Room Relationships
      lobby.Exits.Add("east", toiletries);
      lobby.Exits.Add("north", checkout);

      toiletries.Exits.Add("west", lobby);
      toiletries.Exits.Add("north", foods);
      toiletries.Exits.Add("east", travel);

      foods.Exits.Add("south", toiletries);
      foods.Exits.Add("west", checkout);
      foods.Exits.Add("east", pharmacy);

      travel.Exits.Add("north", pharmacy);
      travel.Exits.Add("west", toiletries);
      travel.Exits.Add("east", vaction);

      pharmacy.Exits.Add("west", foods);
      pharmacy.Exits.Add("south", travel);

      checkout.Exits.Add("east", foods);
      checkout.Exits.Add("south", lobby);
      checkout.Exits.Add("west", outside);


      foods.AddLockedRoom(appt, "east", pharmacy);
      travel.AddLockedRoom(appt, "north", pharmacy);
      checkout.AddLockedRoom(vaccination, "west", outside);


      // NOTE put Items in Rooms
      toiletries.Items.Add(tp);
      toiletries.Items.Add(hs);
      travel.Items.Add(appt);
      pharmacy.Items.Add(vaccination);
      foods.Items.Add(eggs);


      CurrentRoom = lobby;
    }
  }
}