using Microsoft.VisualStudio.TestTools.UnitTesting;
using prohaska.tictactoe.Core;
using System;

namespace prohaska.tictactoe.Test
{
    [TestClass]
    public class BoardTest
    {
      

        [TestInitialize]
        public void SetUp() {
           
        }
        
        [TestMethod]
        public void WhenICreateTheBoardItShoudHaveNoPlayerSigned()
        {
            IBoard board = new Board();
            Assert.IsNull(board.PlayerOne);
            Assert.IsNull(board.PlayerTwo);
        }

        [TestMethod]
        public void WhenIHaveNoPlayerSignedAndICheckCanStartValueItShouldBeFalse()
        {
            IBoard board = new Board();
            board.PlayerOne = new Core.Player() { Name = "Thiago" };
            Assert.AreEqual(false, board.ReadyToStart());
        }

        [TestMethod]
        public void WhenIHaveTwoPlayerSignedAndICheckCanStartValueItShouldBeTrue()
        {
            IBoard board = new Board();
            board.PlayerOne = new Core.Player() { Name = "Thiago" };
            board.PlayerTwo = new Core.Player() { Name = "Thiago 2" };
            Assert.AreEqual(true, board.ReadyToStart());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "We need two players to start.")]
        public void WhenIHaveNoPlayerSignedAndIStartItShouldThrowAError()
        {
            IBoard board = new Board();
            board.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),"We need two players to start.")]
        public void WhenIHaveOnePlayerSignedAndIStartItShouldThrowAError()
        {
            IBoard board = new Board();
            board.PlayerOne = new Core.Player() { Name = "Thiago" };            
            board.Start();
        }

        [TestMethod]
        public void WhenItStartsAllSpotSholdBeEmpty()
        {
            IBoard board = new Board();
            board.PlayerOne = new Core.Player() { Name = "Thiago" };
            board.PlayerTwo = new Core.Player() { Name = "Thiago 2" };

            board.Start();

            Assert.IsNull(board.Spot["A1"]);
            Assert.IsNull(board.Spot["B1"]);
            Assert.IsNull(board.Spot["C1"]);

            Assert.IsNull(board.Spot["A2"]);
            Assert.IsNull(board.Spot["B2"]);
            Assert.IsNull(board.Spot["C2"]);

            Assert.IsNull(board.Spot["A3"]);
            Assert.IsNull(board.Spot["B3"]);
            Assert.IsNull(board.Spot["C3"]);
        }

        [TestMethod]
        public void WhenAPlayerSelectAnEmptySpotItShouldBeAbleToGetThatSpot()
        {
            const string Spot = "A1";
            IBoard board = new Board();
            board.PlayerOne = new Core.Player() { Name = "Thiago" };
            board.PlayerTwo = new Core.Player() { Name = "Thiago 2" };

            board.Start();
            board.SetSpot(Spot, board.PlayerOne);

            Assert.IsNotNull(board.Spot[Spot]);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),"This spot is not available.")]
        public void WhenAPlayerSelectAnOcupiedSpotItShouldThrowsAnError()
        {
            const string Spot = "A1";
            IBoard board = new Board();
            board.PlayerOne = new Core.Player() { Name = "Thiago" };
            board.PlayerTwo = new Core.Player() { Name = "Thiago 2" };

            board.Start();
            board.SetSpot(Spot, board.PlayerOne);
            board.SetSpot(Spot, board.PlayerTwo);
        }

        [TestMethod]       
        public void WhenIGetTheValidRowsItShouldHaveEightPossibilities()
        {
            IBoard board = new Board();

            var validRows = board.GetValidRows();

            Assert.AreEqual(8, validRows.Count);           
        }

        [TestMethod]
        public void WhenIGetTheValidRowsItShouldHaveAllValidPossibilities()
        {
            IBoard board = new Board();

            var validRows = board.GetValidRows();

            Assert.IsNotNull(validRows.Find(x => x.Contains("A1") && x.Contains("A2") && x.Contains("A3")));
            Assert.IsNotNull(validRows.Find(x => x.Contains("B1") && x.Contains("B2") && x.Contains("B3")));
            Assert.IsNotNull(validRows.Find(x => x.Contains("C1") && x.Contains("C2") && x.Contains("C3")));

            Assert.IsNotNull(validRows.Find(x => x.Contains("A1") && x.Contains("B1") && x.Contains("C1")));
            Assert.IsNotNull(validRows.Find(x => x.Contains("A2") && x.Contains("B2") && x.Contains("C2")));
            Assert.IsNotNull(validRows.Find(x => x.Contains("A3") && x.Contains("B3") && x.Contains("C3")));

            Assert.IsNotNull(validRows.Find(x => x.Contains("A1") && x.Contains("B2") && x.Contains("C3")));
            Assert.IsNotNull(validRows.Find(x => x.Contains("A3") && x.Contains("B2") && x.Contains("C1")));
        }

        [TestMethod]        
        public void WhenAPlayFullFillAnRowTheGameShouldBeFinished()
        {
            IBoard board = new Board();
            board.PlayerOne = new Core.Player() { Name = "Thiago" };
            board.PlayerTwo = new Core.Player() { Name = "Thiago 2" };

            board.Start();

            board.SetSpot("A1", board.PlayerOne);
            board.SetSpot("B1", board.PlayerTwo);
            board.SetSpot("A2", board.PlayerOne);
            board.SetSpot("B3", board.PlayerTwo);
            board.SetSpot("A3", board.PlayerOne);

            Assert.AreEqual(true, board.IsFinished);
        }

        [TestMethod]
        public void WhenAPlayNOTFullFillAnRowTheGameShouldNOTBeFinished()
        {
            IBoard board = new Board();
            board.PlayerOne = new Core.Player() { Name = "Thiago" };
            board.PlayerTwo = new Core.Player() { Name = "Thiago 2" };

            board.Start();

            board.SetSpot("A1", board.PlayerOne);
            board.SetSpot("C1", board.PlayerTwo);
            board.SetSpot("A2", board.PlayerOne);
            board.SetSpot("C2", board.PlayerTwo);
            board.SetSpot("B1", board.PlayerOne);

            Assert.AreEqual(false, board.IsFinished);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "It is not your turn.")]
        public void WhenAPlayerDoAMoveTwiceItShouldThrowsAnError()
        {           
            IBoard board = new Board();
            board.PlayerOne = new Core.Player() { Name = "Thiago" };
            board.PlayerTwo = new Core.Player() { Name = "Thiago 2" };

            board.Start();
            board.SetSpot("A1", board.PlayerOne);
            board.SetSpot("A2", board.PlayerOne);
        }

        [TestMethod]
        public void WhenAPlayNOTFullFillAnRowTheGameShouldNOTDefineItAsWonPlayer()
        {
            IBoard board = new Board();
            board.PlayerOne = new Core.Player() { Name = "Thiago" };
            board.PlayerTwo = new Core.Player() { Name = "Thiago 2" };

            board.Start();

            board.SetSpot("A1", board.PlayerOne);
            board.SetSpot("C1", board.PlayerTwo);
            board.SetSpot("A2", board.PlayerOne);
            board.SetSpot("C2", board.PlayerTwo);
            
            Assert.IsNull(board.GetWonPlayer());
        }

        [TestMethod]
        public void WhenAPlayFullFillAnRowTheGameShouldDefineItAsWonPlayer()
        {
            IBoard board = new Board();
            board.PlayerOne = new Core.Player() { Name = "Thiago" };
            board.PlayerTwo = new Core.Player() { Name = "Thiago 2" };

            board.Start();

            board.SetSpot("A1", board.PlayerOne);
            board.SetSpot("C1", board.PlayerTwo);
            board.SetSpot("A2", board.PlayerOne);
            board.SetSpot("C2", board.PlayerTwo);
            board.SetSpot("A3", board.PlayerOne);

            Assert.AreEqual(board.PlayerOne,board.GetWonPlayer());
        }

        [TestMethod]
        public void WhenAPlayWonAnEventInformingThePlayerShouldBeRaise()
        {           
            IBoard board = new Board();
            board.PlayerOne = new Core.Player() { Name = "Thiago" };
            board.PlayerTwo = new Core.Player() { Name = "Thiago 2" };
            board.Start();

            board.OnPlayerWon += (e) => {               
                Assert.AreEqual(board.PlayerOne, e.Player);
            };

            board.SetSpot("A1", board.PlayerOne);
            board.SetSpot("B1", board.PlayerTwo);
            board.SetSpot("A2", board.PlayerOne);
            board.SetSpot("B2", board.PlayerTwo);
            board.SetSpot("A3", board.PlayerOne);    
        }
    }
}
