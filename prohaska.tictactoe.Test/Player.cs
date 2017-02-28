using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prohaska.tictactoe.Core;

namespace prohaska.tictactoe.Test
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void WhenCreateANewPlayerItShouldHaveAnIcon()
        {
            IPlayer player = new Player();
            Assert.IsNull(player.Icon);
        }

        [TestMethod]
        public void WhenIGetThePlayerIconItShoulHaveOneCharactere()
        {
            int maxLenght = 1;
            IPlayer player = new Player();
            player.Icon = "XxX";
            Assert.AreEqual(maxLenght, player.Icon.Length);
        }
    }
}
