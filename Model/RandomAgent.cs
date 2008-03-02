using System;
using Goban.Model;

namespace Goban.Model
{
   public class RandomAgent : IAgent
   {
      private static Random random = new Random(DateTime.Now.Millisecond);
      
      public Position SelectPlay(Board board)
      {
         Position position;
         do
         {
            position = GetRandom(board.Size);
         }
         while (board.IsOccupied(position));
         return position;
      }
      
      private static Position GetRandom(int size)
      {
         return new Position(random.Next(0, size), random.Next(0, size));
      }
   }
}
