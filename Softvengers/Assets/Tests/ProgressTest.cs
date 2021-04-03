using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ProgressTest
    {
        // A Test behaves as an ordinary method
        private Player player;
        private Navigation navigation;

        [Test]
        public void ProgressTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ProgressTestNewPlanetUnlocked()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            player = ScriptableObject.CreateInstance<Player>();
            navigation = ScriptableObject.CreateInstance<Navigation>();

            player.universePogress = 0;
            player.solarSystemProgress = 0;
            player.planetProgress = 0;

            navigation.universeSelected = 0;
            navigation.solarSystemSelected = 0;
            navigation.planetSelected = 0;

            var gameObject = new GameObject();

            gameObject.AddComponent<ResultManager>();

            gameObject.GetComponent<ResultManager>().UpDatePlayerProgress(player, navigation);

            yield return new WaitForSeconds(2.0f);

            Assert.AreEqual(player.universePogress, 0);
            Assert.AreEqual(player.solarSystemProgress, 0);
            Assert.AreEqual(player.planetProgress, 1);
        }

        [UnityTest]
        public IEnumerator ProgressTestNewSolarSystemUnlocked()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            player = ScriptableObject.CreateInstance<Player>();
            navigation = ScriptableObject.CreateInstance<Navigation>();

            player.universePogress = 0;
            player.solarSystemProgress = 0;
            player.planetProgress = 3;

            navigation.universeSelected = 0;
            navigation.solarSystemSelected = 0;
            navigation.planetSelected = 3;

            var gameObject = new GameObject();

            gameObject.AddComponent<ResultManager>();

            gameObject.GetComponent<ResultManager>().UpDatePlayerProgress(player, navigation);

            yield return new WaitForSeconds(2.0f);

            Assert.AreEqual(player.universePogress, 0);
            Assert.AreEqual(player.solarSystemProgress, 1);
            Assert.AreEqual(player.planetProgress, 0);
        }
    }
}
