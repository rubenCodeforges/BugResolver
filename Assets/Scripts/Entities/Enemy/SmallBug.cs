using UnityEngine;

namespace Entities.Enemy
{
    public class SmallBug: AbstractBug
    {
        private bool _isDead;

        public override bool isDead
        {
            get { return health <= 0; }
        }
    }
}