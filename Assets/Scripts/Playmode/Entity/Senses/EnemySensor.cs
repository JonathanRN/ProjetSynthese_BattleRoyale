using System.Collections.Generic;
using Playmode.Enemy;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public delegate void EnemySensorEventHandler(Enemy.Enemy enemy);

    public class EnemySensor : MonoBehaviour
    {
        private ICollection<Enemy.Enemy> enemiesInSight;

        public event EnemySensorEventHandler OnEnemySeen;
        public event EnemySensorEventHandler OnEnemySightLost;

        public IEnumerable<Enemy.Enemy> EnemiesInSight => enemiesInSight;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            enemiesInSight = new HashSet<Enemy.Enemy>();
        }

        public void See(Enemy.Enemy enemy)
        {
            enemiesInSight.Add(enemy);

            NotifyEnemySeen(enemy);
        }

        public void LooseSightOf(Enemy.Enemy enemy)
        {
            enemiesInSight.Remove(enemy);

            NotifyEnemySightLost(enemy);
        }

        private void NotifyEnemySeen(Enemy.Enemy enemy)
        {
            if (OnEnemySeen != null) OnEnemySeen(enemy);
        }

        private void NotifyEnemySightLost(Enemy.Enemy enemy)
        {
            if (OnEnemySightLost != null) OnEnemySightLost(enemy);
        }
    }
}