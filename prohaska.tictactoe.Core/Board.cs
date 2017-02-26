using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prohaska.tictactoe.Core
{
    public class Board : IBoard
    {
        private List<List<string>> _WinRows = new List<List<string>>();
        private IPlayer _playerTurn;
        private IPlayer _wonPlayer;

        public IPlayer PlayerOne { get; set; }
        public IPlayer PlayerTwo { get; set; }
        public Dictionary<string, IPlayer> Spot { get; set; }

        public Board()
        {
            _WinRows.Add(new List<string>() { "A1", "B1", "C1" });
            _WinRows.Add(new List<string>() { "A2", "B2", "C2" });
            _WinRows.Add(new List<string>() { "A3", "B3", "C3" });
            _WinRows.Add(new List<string>() { "A1", "A2", "A3" });
            _WinRows.Add(new List<string>() { "B1", "B2", "B3" });
            _WinRows.Add(new List<string>() { "C1", "C2", "C3" });
            _WinRows.Add(new List<string>() { "A1", "B2", "C3" });
            _WinRows.Add(new List<string>() { "A3", "B2", "C1" });
        }


        public int GetPositions()
        {
            return 9;
        }

        public bool ReadyToStart()
        {
            return PlayerOne != null && PlayerTwo != null;
        }

        public void SetSpot(string spot, IPlayer player)
        {
            IsPlayerTurn(player);
            if (Spot[spot] == null)
            {
                Spot[spot] = player;
                CheckItThePlayerWonTheGame(player);
                SetNextPlayerTurn(player);
            }
            else
            {
                throw new Exception("This spot is not available.");
            }
        }

        private void SetNextPlayerTurn(IPlayer currentPlayer)
        {
            _playerTurn = currentPlayer == PlayerOne ? PlayerTwo : PlayerOne;
        }

        private void IsPlayerTurn(IPlayer playerOne)
        {
            if (_playerTurn != playerOne)
                throw new Exception("It is not your turn.");
        }

        private void CheckItThePlayerWonTheGame(IPlayer player)
        {
            List<string> playerPositions = Spot.Where(x => x.Value == player).Select(x => x.Key).ToList();

            if (playerPositions.Count < 3)
            {
                IsFinished = false;
            }
            else
            {
                var RowWon = FindAPlayerWinRow(playerPositions);

                if (RowWon == null)
                    IsFinished = false;
                else
                {
                    IsFinished = true;
                    _wonPlayer = player;
                }
            }


        }

        private List<string> FindAPlayerWinRow(List<string> playerPositions)
        {
            List<List<string>> listOfSpotCombinations = GetPlayerSpotsCombinations(playerPositions);

            return GetWinRowIfThePlayerHasOne(listOfSpotCombinations);
        }

        private List<string> GetWinRowIfThePlayerHasOne(List<List<string>> listOfSpotCombinations)
        {
            List<string> result = null;
            foreach (var item in listOfSpotCombinations)
            {
                result = _WinRows.Find(x => x.Contains(item.First()) && x.Contains(item.Skip(1).First()) && x.Contains(item.Skip(2).First()));

                if (result != null)
                    return result;
            }

            return result;
        }

        private static List<List<string>> GetPlayerSpotsCombinations(List<string> playerPositions)
        {
            var listOfSpotCombinations = new List<List<string>>();
            var spotombinations = Permutation.GetItens(playerPositions, 3);


            foreach (var spots in spotombinations)
            {
                var possibleCombination = new List<string>();
                foreach (var spot in spots)
                {
                    possibleCombination.Add(spot);
                }
                listOfSpotCombinations.Add(possibleCombination);
            }

            return listOfSpotCombinations;
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

            _playerTurn = PlayerOne;
            _wonPlayer = null;
        }

        public List<List<string>> GetValidRows()
        {
            return _WinRows;
        }

        public IPlayer GetWonPlayer()
        {
            return _wonPlayer;
        }

        public bool IsFinished { get; private set; }

    }
}
