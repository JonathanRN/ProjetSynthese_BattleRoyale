using Playmode.Ennemy;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public class EnnemyStimulus : MonoBehaviour
    {
        private Enemy ennemy;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ennemy = transform.root.GetComponentInChildren<Enemy>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<EnnemySensor>()?.See(ennemy);     
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            other.GetComponent<EnnemySensor>()?.LooseSightOf(ennemy);
        }
    }
}