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
            Room produce = new Room("Produce Section", @"
                 \|/
                 AXA
                /XXX\
                \XXX/
                 `^'
            Plenty of Fruit and Veggies, wonder why no on is stockpiling these yet");
            Room electronics = new Room("Electronics", "Lots of stuff still here, yet no webcams in sight.");
            Room frozenFoods = new Room("Frozen Foods", "Mostly empty shelves though the vegan chocolate hummus is still in stock for some reason");
            EndRoom checkout = new EndRoom("Checkout", "A stressed minimum wage employee stares out you with a thousand yard stare, he has seen too much these last few weeks", true, "You breeze through the checkout with your new found wealth!");
            EndRoom toiletPaperIsle = new EndRoom("Toiletries", "A hoarde of people are racing through this aisle with their weapons out", false, "You are trampled under foot and your name is lost to history");

            // NOTE Create all Items
            Item tp = new Item("Toilet Paper", "A Single Roll of precious paper, it must have fallen from a pack");

            // NOTE Make Room Relationships
            produce.Exits.Add("east", electronics);
            electronics.Exits.Add("west", produce);
            electronics.Exits.Add("north", frozenFoods);
            electronics.Exits.Add("east", toiletPaperIsle);
            frozenFoods.Exits.Add("south", electronics);

            frozenFoods.AddLockedRoom(tp, "west", checkout);
            checkout.Exits.Add("east", frozenFoods);


            // NOTE put Items in Rooms
            electronics.Items.Add(tp);


            CurrentRoom = produce;
        }
    }
}