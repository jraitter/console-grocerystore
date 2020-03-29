namespace escape_corona.Models
{
  static class Utils
  {
    public static string MiniMart = @"
        _       _                         _   
  /\/\ (_)_ __ (_)       /\/\   __ _ _ __| |_ 
 /    \| | '_ \| |_____ /    \ / _` | '__| __|
/ /\/\ \ | | | | |_____/ /\/\ \ (_| | |  | |_ 
\/    \/_|_| |_|_|     \/    \/\__,_|_|   \__|
                                              
";

    public static string HelpMessage = @"
 go <direction> --- is used to travel.  Direction is north/east/south/west.
 take <item>    --- is used to gather items from the room they are in.
                    Must be in same room to get them.  'get' also works.
 use <item>     --- This will invoke the function of an item in your inventory.
                    example: some items are needed to unlock doors.

 look      ---  This will give you details about your current location in the mart.
 inventory ---  Will let you know what you have with you. 'inv' also works.
 checkout  ---  Must be at checkout stand, do this to pay for your items and get receipt.
 reset     ---  Will reset the game.  Used to start over.
 quit      ---  Will exit the game.  'q' also works.
 help      ---  Shows this menu.

 (hint: to find out where you are, type 'look')
";
    public static string ObjectiveMessage = @"
YEAR:  2020

SITUATION:  A pandemic is occuring with the outbreak of COVID-19, know as Corona Virus.

OBJECTIVE:  Enter the mini-mart and gather items needed to survive a possible lock-down.

start by typing 'help'

";

  }//endof class
}//endof namespace