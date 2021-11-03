// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	public interface IProjectile
	{
		float Speed { get; }
		int Damage { get; }
		float TimeToDestroy { get; }

		void Init(Transform targetPos, int dmg);
		void ProcessCollision(GameObject other);
	}
}