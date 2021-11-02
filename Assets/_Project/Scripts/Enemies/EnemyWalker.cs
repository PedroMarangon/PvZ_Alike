// Maded by Pedro M Marangon
using DG.Tweening;
using UnityEngine;

namespace S2P_Test
{
	public class EnemyWalker : MonoBehaviour, IEnemyLogic
    {
		[SerializeField] private float speed = 2;
		[SerializeField] private float stoppingDistance = 2.5f;
		[SerializeField] private float checkDistance = 100f;
		[SerializeField] private float checkHeight = 1;

		public float Speed => speed;
		public float Damage => 0;

		public void MoveToPosition(Vector3 pos)
		{
			//TODO: Movement
			transform.DOMove(pos, Speed).SetSpeedBased(true);
		}

		public void MoveToPosition(Transform targetPos) { }

		void Start()
        {
			Ray ray = new Ray(transform.position + Vector3.up * checkHeight, Vector3.right);
			if (Physics.Raycast(ray, out RaycastHit hit, checkDistance))
			{
				Vector3 position = hit.collider.transform.position + (Vector3.left * stoppingDistance);
				MoveToPosition(position);
			}
		}

#if UNITY_EDITOR
		private void OnDrawGizmosSelected()
		{
			if (UnityEditor.EditorApplication.isPlaying) return;
			Gizmos.color = Color.red;
			Vector3 startPos = transform.position + Vector3.up * checkHeight;
			Gizmos.DrawLine(startPos, startPos + (Vector3.right * checkDistance));
		}
#endif
	}



	public class EnemyAttack : MonoBehaviour
	{
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}
	}

}