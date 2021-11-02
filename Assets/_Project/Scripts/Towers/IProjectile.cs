// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	public interface IProjectile
	{
		float Speed { get; }
		float TimeToDestroy { get; }
		void Init(Transform targetPos);
		void ProcessCollision(GameObject other);
	}
}