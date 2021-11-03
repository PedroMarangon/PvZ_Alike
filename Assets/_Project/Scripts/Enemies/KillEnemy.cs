// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	public class KillEnemy : MonoBehaviour
	{
		public void Kill()
		{
			if (transform.parent) Destroy(transform.parent.gameObject);
			else Destroy(gameObject);
		}
	}

}