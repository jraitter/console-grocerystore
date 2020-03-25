using System.Collections.Generic;

namespace escape_corona.Interfaces
{
    interface IPlayer
    {
        string Name { get; set; }
        List<IItem> Inventory { get; set; }
    }
}