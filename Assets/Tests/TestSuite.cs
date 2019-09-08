using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        [UnityTest]
        public IEnumerator AsteroidsMovesDown()
        {
            GameObject Camera = GameObject.Instantiate(Resources.Load("Camera")) as GameObject;
            GameObject Spawner = GameObject.Instantiate(Resources.Load("Spawner")) as GameObject;
            GameObject Asteroid = Spawner.GetComponent<AsteroidSpawner>().SpawnAsteroid();
            float initialYPos = Asteroid.transform.position.y;

            yield return new WaitForSeconds(0.1f);

            Assert.Less(Asteroid.transform.position.y, initialYPos);

            Object.Destroy(Spawner);
            Object.Destroy(Asteroid);
            Object.Destroy(Camera);
        }

        [UnityTest]
        public IEnumerator EnemyCanSeePlayer()
        {
            GameObject Camera = GameObject.Instantiate(Resources.Load("Camera")) as GameObject;
            GameObject Spawner = GameObject.Instantiate(Resources.Load("Spawner")) as GameObject;
            GameObject Player = GameObject.Instantiate(Resources.Load("Player")) as GameObject;

            GameObject Enemy = Spawner.GetComponent<EnemySpawner>().SpawnEnemy();
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(Enemy.GetComponent<EnemyController>().FindPlayer());

            Object.Destroy(Spawner);
            Object.Destroy(Player);
            Object.Destroy(Enemy);
            Object.Destroy(Camera);
        }


        [UnityTest]
        public IEnumerator EnemyMovesToThePlayer()
        {
            GameObject Camera = GameObject.Instantiate(Resources.Load("Camera")) as GameObject;
            GameObject Spawner = GameObject.Instantiate(Resources.Load("Spawner")) as GameObject;
            GameObject Player = GameObject.Instantiate(Resources.Load("Player")) as GameObject;
            GameObject Enemy = Spawner.GetComponent<EnemySpawner>().SpawnEnemy();
            float initialEnemyYPos = Enemy.transform.position.y;

            yield return new WaitForSeconds(0.1f);

            Assert.Less(Enemy.transform.position.y, initialEnemyYPos);

            Object.Destroy(Spawner);
            Object.Destroy(Player);
            Object.Destroy(Enemy);
            Object.Destroy(Camera);
        }

        [UnityTest]
        public IEnumerator EnemyCanShootPlayerAfterShootingDelayTime()
        {
            GameObject Camera = GameObject.Instantiate(Resources.Load("Camera")) as GameObject;
            GameObject Spawner = GameObject.Instantiate(Resources.Load("Spawner")) as GameObject;
            GameObject Player = GameObject.Instantiate(Resources.Load("Player")) as GameObject;
            GameObject Enemy = Spawner.GetComponent<EnemySpawner>().SpawnEnemy();
            int enemyLevel = Enemy.GetComponent<EnemyController>().Level;

            Enemy.GetComponent<EnemyController>().SetMomentOfPrevShoot(-100f);
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(GameObject.FindWithTag("Ammo"));

            Object.Destroy(Spawner);
            Object.Destroy(Player);
            Object.Destroy(Enemy);
            Object.Destroy(Camera);
        }

        [UnityTest]
        public IEnumerator PlayerLevelUpWhenEnoughScore()
        {
            GameObject Player = GameObject.Instantiate(Resources.Load("Player")) as GameObject;
            int initLevel = Player.GetComponent<PlayerController>().Level;
            float scoreToNextLevel = Player.GetComponent<PlayerController>().data.scoresToNextLevel[initLevel];

            Score.SetScore(scoreToNextLevel + 1);
            yield return new WaitForSeconds(0.1f);

            Assert.Less(initLevel, Player.GetComponent<PlayerController>().Level);

            Object.Destroy(Player);
        }

        [UnityTest]
        public IEnumerator EnemySpawnerLevelUpWhenEnoughScore()
        {
            GameObject Camera = GameObject.Instantiate(Resources.Load("Camera")) as GameObject;
            GameObject Spawner = GameObject.Instantiate(Resources.Load("Spawner")) as GameObject;
            int initLevel = Spawner.GetComponent<EnemySpawner>().Level;
            float scoreToNextLevel = Spawner.GetComponent<EnemySpawner>().dataInParentObject.scoresToNextLevel[initLevel];

            Score.SetScore(scoreToNextLevel + 1);
            yield return new WaitForSeconds(0.1f);


            Assert.Less(initLevel, Spawner.GetComponent<EnemySpawner>().Level);

            Object.Destroy(Spawner);
            Object.Destroy(Camera);
        }

        [UnityTest]
        public IEnumerator AsteroidSpawnerLevelUpWhenEnoughScore()
        {
            GameObject Camera = GameObject.Instantiate(Resources.Load("Camera")) as GameObject;
            GameObject Spawner = GameObject.Instantiate(Resources.Load("Spawner")) as GameObject;
            int initLevel = Spawner.GetComponent<AsteroidSpawner>().Level;
            float scoreToNextLevel = Spawner.GetComponent<AsteroidSpawner>().dataInParentObject.scoresToNextLevel[initLevel];

            Score.SetScore(scoreToNextLevel + 1);
            yield return new WaitForSeconds(0.1f);

            Assert.Less(initLevel, Spawner.GetComponent<AsteroidSpawner>().Level);

            Object.Destroy(Spawner);
            Object.Destroy(Camera);
        }

        [UnityTest]
        public IEnumerator AsteroidCanAccessDataFile()
        {
            GameObject Camera = GameObject.Instantiate(Resources.Load("Camera")) as GameObject;
            GameObject Spawner = GameObject.Instantiate(Resources.Load("Spawner")) as GameObject;
            GameObject Asteroid = Spawner.GetComponent<AsteroidSpawner>().SpawnAsteroid();
            yield return new WaitForSeconds(0.1f);

            var data = Asteroid.GetComponent<AsteroidController>().data;

            Assert.IsTrue(data != null);

            Object.Destroy(Spawner);
            Object.Destroy(Camera);
            Object.Destroy(Asteroid);
        }

        [UnityTest]
        public IEnumerator PlayerCanAccessDataFile()
        {
            GameObject Player = GameObject.Instantiate(Resources.Load("Player")) as GameObject;
            yield return new WaitForSeconds(0.1f);

            var data = Player.GetComponent<PlayerController>().data;

            Assert.IsTrue(data != null);

            Object.Destroy(Player);
        }

        [UnityTest]
        public IEnumerator EnemyCanAccessDataFile()
        {
            GameObject Camera = GameObject.Instantiate(Resources.Load("Camera")) as GameObject;
            GameObject Spawner = GameObject.Instantiate(Resources.Load("Spawner")) as GameObject;
            yield return new WaitForSeconds(0.3f);
            GameObject Enemy = Spawner.GetComponent<EnemySpawner>().SpawnEnemy();
            yield return new WaitForSeconds(0.1f);

            var data = Enemy.GetComponent<EnemyController>().data;

            Assert.IsTrue(data != null);

            Object.Destroy(Spawner);
            Object.Destroy(Enemy);
            Object.Destroy(Camera);
        }

        [UnityTest]
        public IEnumerator AsteroidExplodeWhenNoMoreLife()
        {
            GameObject Camera = GameObject.Instantiate(Resources.Load("Camera")) as GameObject;
            GameObject Spawner = GameObject.Instantiate(Resources.Load("Spawner")) as GameObject;
            GameObject Asteroid = Spawner.GetComponent<AsteroidSpawner>().SpawnAsteroid();
            yield return new WaitForSeconds(0.1f);

            Asteroid.GetComponent<AsteroidController>().SetLife(-1);
            yield return new WaitForSeconds(0.1f);
            bool isThereIsExplosion = GameObject.FindObjectOfType(typeof(ExplosionController));

            Assert.IsTrue(Asteroid == null && isThereIsExplosion);

            Object.Destroy(Spawner);
            Object.Destroy(Camera);
            Object.Destroy(Asteroid);
        }

    }


}
