using System;
using System.Threading;
using bb_mvc.Models;
using escape_corona.Interfaces;
using escape_corona.Models;
using escape_corona.Services;

namespace escape_corona.Controllers
{
  class GameController : IGameController
  {
    private GameService _gs { get; set; }
    private bool _running { get; set; } = true;
    public void Run()
    {
      Console.WriteLine("Hello there what is your name?");
      // NOTE Gets string from readline and passes is as the player name
      _gs = new GameService(Console.ReadLine());
      string greeting = "Welcome to MiniMart Game, you are here to get the essentials for the COVID-19 scare, good luck!                ";
      foreach (char letter in greeting)
      {
        Console.Write(letter);
        Thread.Sleep(100);
      }
      Console.WriteLine();
      Print();
      while (_running)
      {
        GetUserInput();
        Print();
      }
    }
    public void GetUserInput()
    {
      Console.WriteLine("What would you like to do?");
      string input = Console.ReadLine().ToLower() + " "; //go north ;take toilet paper ;look 
      string command = input.Substring(0, input.IndexOf(" ")); //go; take; look
      string option = input.Substring(input.IndexOf(" ") + 1).Trim();//north; toilet paper;''

      switch (command)
      {
        case "quit":
        case "q":
          _running = false;
          _gs.Messages.Add(new Message("Good-bye", ConsoleColor.White));
          break;
        case "reset":
          _gs.Reset();
          break;
        case "look":
          _gs.Look();
          break;
        case "help":
          _gs.Help();
          break;
        case "inventory":
        case "inv":
          _gs.Inventory();
          break;
        case "checkout":

          if (!_gs.Checkout())
          {
            _running = false;
            _gs.Messages.Add(new Message("Good-bye", ConsoleColor.White));
          };

          break;
        case "go":
          _running = _gs.Go(option);
          break;
        case "take":
        case "get":
          _gs.Take(option);
          break;
        case "use":
          _gs.Use(option);
          break;
        default:
          _gs.Messages.Add(new Message("Not a recognized command", ConsoleColor.Red));
          _gs.Look();
          break;
      }
    }

    public void Print()
    {
      Console.Clear();
      _gs.Messages.Insert(0, new Message(Utils.MiniMart, ConsoleColor.Green));
      //itterate over messages and print each one
      foreach (Message message in _gs.Messages)
      {
        Console.ForegroundColor = message.Color;
        Console.WriteLine(message.Body);
      }
      _gs.Messages.Clear();
    }

  }
}