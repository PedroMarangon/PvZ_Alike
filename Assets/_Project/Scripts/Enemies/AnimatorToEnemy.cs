// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	public class AnimatorToEnemy : MonoBehaviour
	{
		public void Kill()
		{
			if (transform.parent) Destroy(transform.parent.gameObject);
			else Destroy(gameObject);
		}

		public void Attack()
		{
			if(transform.parent)
			{
				var enemyAtk = GetComponentInParent<EnemyAttack>();
				if(enemyAtk)
				{
					enemyAtk.Atk();
				}
			}
		}

	}

}