using System;
using Playmode.Enemy.Strategies;
using Playmode.Util;
using Playmode.Util.Collections;
using UnityEngine;

namespace Playmode.Ennemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private static readonly Color[] DefaultColors =
        {
            Color.white, Color.black, Color.blue, Color.cyan, Color.green,
            Color.magenta, Color.red, Color.yellow, new Color(255, 125, 0, 255)
        };

        private static readonly EnemyStrategy[] DefaultStrategies =
        {
            EnemyStrategy.Normal,
            EnemyStrategy.Careful,
            EnemyStrategy.Cowboy,
            EnemyStrategy.Camper
        };

        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Color[] colors = DefaultColors;

        private void Awake()
        {
            ValidateSerialisedFields();
        }

        private void Start()
        {
            SpawnEnemies();
        }

        private void ValidateSerialisedFields()
        {
            if (enemyPrefab == null)
                throw new ArgumentException("Can't spawn null enemy prefab.");
            if (colors == null || colors.Length == 0)
                throw new ArgumentException("Enemies needs colors to be spawned.");
            if (transform.childCount <= 0)
                throw new ArgumentException("Can't spawn enemies without spawn points. " +
                                            "Create children for this GameObject as spawn points.");
        }

        private void SpawnEnemies()
        {
            var strategyProvider = new LoopingEnumerator<EnemyStrategy>(DefaultStrategies);
            var colorProvider = new LoopingEnumerator<Color>(colors);

			for (var i = 0; i < transform.childCount; i++)
				SpawnEnemy(
					transform.GetChild(i),
					strategyProvider.Next(),
					colorProvider.Next()
				);
		}

        private void SpawnEnemy(Transform childTransform, EnemyStrategy strategy, Color color)
        {
            Instantiate(enemyPrefab, childTransform.position, Quaternion.identity)
                .GetComponentInChildren<Enemy.Enemy>()
                .Configure(strategy, color);
        }
    }
}