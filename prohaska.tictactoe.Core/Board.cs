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
        public event PlayerWon PlayerWon;
        public IPlayer PlayerOne { get; set; }
        public IPlayer PlayerTwo { get; set; }
        public Dictionary<string, IPlayer> Spot { get; set; }

        public bool IsFinished { get; private set; }

        public Board()
        {
            SetUpValidRowsToWin();
        }

        private void SetUpValidRowsToWin()
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

        public void SetSpot(string spot, IPlayer player)
        {
            IsPlayerTurn(player);
            if (IsTheSpotAvailable(spot))
            {
                MakeThePlayersMove(spot, player);
            }
            else
            {
                throw new NoAvailableSpotException();
            }
        }

        private void MakeThePlayersMove(string spot, IPlayer player)
        {
            Spot[spot] = player;
            CheckIfThePlayerWonTheGame(player);
            SetNextPlayerTurn(player);
        }

        private void CheckIfThePlayerWonTheGame(IPlayer player)
        {
            List<string> playerPositions = Spot.Where(x => x.Value == player).Select(x => x.Key).ToList();

            if (IsThereEnoughSpotOccupiedToWin(playerPositions))
            {
                IsFinished = false;
            }
            else
            {
                var RowWon = GetAWinRowOfAPlayer(playerPositions);

                if (RowWon == null)
                    IsFinished = false;
                else
                {
                    FinishGameWithAWinnerPlayer(player);
                }
            }
        }

        private void SetNextPlayerTurn(IPlayer currentPlayer)
        {
            _playerTurn = currentPlayer == PlayerOne ? PlayerTwo : PlayerOne;
        }

        private static bool IsThereEnoughSpotOccupiedToWin(List<string> playerPositions)
        {
            return playerPositions.Count < 3;
        }

        private List<string> GetAWinRowOfAPlayer(List<string> playerPositions)
        {
            List<List<string>> listOfSpotCombinations = GetPlayerSpotsCombinations(playerPositions);

            return GetWinRowIfThePlayerHasOne(listOfSpotCombinations);
        }

        private void FinishGameWithAWinnerPlayer(IPlayer player)
        {
            IsFinished = true;
            _wonPlayer = player;
            NotifyOnPlayerWon();
        }

        private void NotifyOnPlayerWon()
        {
            PlayerWon?.Invoke(new PlayerWonEventArgs { Player = _wonPlayer });
        }


        private void IsPlayerTurn(IPlayer playerOne)
        {
            if (_playerTurn != playerOne)
                throw new NotPlayerTurnException();
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
            var allSpotCombinations = Permutation.GetItens(playerPositions, 3);

            foreach (var spots in allSpotCombinations)
            {
                var possibleCombination = new List<string>();
                possibleCombination.AddRange(spots);
                listOfSpotCombinations.Add(possibleCombination);
            }

            return listOfSpotCombinations;
        }

        public void Start()
        {
            if (PlayerOne == null || PlayerTwo == null)
                throw new NotEnoughPlayersException();

            CreateEmptyBoard();
            DefinePlayOneTurn();
            _wonPlayer = null;
        }

        private void DefinePlayOneTurn()
        {
            _playerTurn = PlayerOne;
        }

        private void CreateEmptyBoard()
        {
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

        public List<List<string>> GetValidRows() => _WinRows;

        public IPlayer GetWonPlayer() => _wonPlayer;

        public bool ReadyToStart() => PlayerOne != null && PlayerTwo != null;

        private bool IsTheSpotAvailable(string spot) => Spot[spot] == null;
    }
}
