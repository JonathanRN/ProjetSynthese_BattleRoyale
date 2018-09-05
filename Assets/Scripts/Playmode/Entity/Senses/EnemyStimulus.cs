using Playmode.Ennemy;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public class EnemyStimulus : MonoBehaviour
    {
        private Enemy.Enemy enemy;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            enemy = transform.root.GetComponentInChildren<Enemy.Enemy>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<EnemySensor>()?.See(enemy);     
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            other.GetComponent<EnemySensor>()?.LooseSightOf(enemy);
        }
    }
}