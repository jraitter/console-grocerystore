namespace escape_corona.Interfaces
{
    interface IGame
    {
        IPlayer CurrentPlayer { get; set; }
        IRoom CurrentRoom { get; set; }
    }
}