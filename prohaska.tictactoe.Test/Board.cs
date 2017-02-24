using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prohaska.tictactoe.UI;

namespace prohaska.tictactoe.Test
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void WhenICreateTheBoardItShoudHave3x3Positions()
        {
            IBoard board = new Board();
            Assert.AreEqual(9, board.GetPositions());
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
            board.PlayerOne = new Player() {Name = "Thiago" };
            Assert.AreEqual(false, board.ReadyToStart());
        }

        [TestMethod]
        public void WhenIHaveTwoPlayerSignedAndICheckCanStartValueItShouldBeTrue()
        {
            IBoard board = new Board();
            board.PlayerOne = new Player() { Name = "Thiago" };
            board.PlayerTwo = new Player() { Name = "Thiago 2" };
            Assert.AreEqual(true, board.ReadyToStart());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),"We need two players to start.")]
        public void WhenIHaveOnePlayerSignedAndIStartItShouldThrowAError()
        {
            IBoard board = new Board();
            board.PlayerOne = new Player() { Name = "Thiago" };            
            board.Start();
        }

        [TestMethod]
        public void WhenItStartsAllSpotSholdBeEmpty()
        {
            IBoard board = new Board();
            board.PlayerOne = new Player() { Name = "Thiago" };
            board.PlayerTwo = new Player() { Name = "Thiago 2" };

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
            board.PlayerOne = new Player() { Name = "Thiago" };
            board.PlayerTwo = new Player() { Name = "Thiago 2" };

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
            board.PlayerOne = new Player() { Name = "Thiago" };
            board.PlayerTwo = new Player() { Name = "Thiago 2" };

            board.Start();
            board.SetSpot(Spot, board.PlayerOne);
            board.SetSpot(Spot, board.PlayerTwo);
        }        
    }
}
