using UnityEngine;

namespace Entities.Enemy
{
    public class SmallBug: AbstractBug
    {
        public GameObject target;
        private bool _isDead;

        public override bool isDead
        {
            get { return health <= 0; }
        }

        private void Update()
        {
            MoveToTarget(target);
        }
    }
}