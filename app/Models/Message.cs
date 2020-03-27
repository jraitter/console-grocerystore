using System;

namespace bb_mvc.Models
{
  class Message
  {
    public string Body { get; set; }
    public ConsoleColor Color { get; set; }

    public Message(string body, ConsoleColor color = ConsoleColor.Green)
    {
      Body = body;
      Color = color;
    }

  }
}