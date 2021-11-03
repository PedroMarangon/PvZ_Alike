//Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	public interface IEnemyLogic
	{
		float Speed { get; }
		float Damage { get; }

		void MoveToPosition(Vector3 pos);
	}
}