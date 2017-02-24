using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prohaska.tictactoe.UI
{
    public class Board : IBoard
    {
        public IPlayer PlayerOne { get; set; }

        public IPlayer PlayerTwo { get; set; }

        public Dictionary<string, IPlayer> Spot { get; set; }

        public int GetPositions()
        {
            return 9;
        }

        public bool ReadyToStart()
        {
            return PlayerOne != null && PlayerTwo != null;
        }

        public void SetSpot(string spot, IPlayer playerOne)
        {
            if (Spot[spot] == null)
            {
                Spot[spot] = playerOne;
            }
            else
            {
                throw new Exception("This spot is not available.");
            }
        }

        public void Start()
        {
            if (PlayerOne == null || PlayerTwo == null)
                throw new Exception("We need two players to start.");

            Spot = new Dictionary<string, IPlayer>();

            Spot.Add("A1", null);
            Spot.Add("B1", null);
            Spot.Add("C1", null);

            Spot.Add("A2", null);
            Spot.Add("B2", null);
            Spot.Add("C2", null);

            Spot.Add("A3", null);
            Spot.Add("B3", null);
            Spot.Add("C3", null);
        }
               
    }
}
