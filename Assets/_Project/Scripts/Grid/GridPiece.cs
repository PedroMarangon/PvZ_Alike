// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	[RequireComponent(typeof(BoxCollider))]
	public class GridPiece : MonoBehaviour
	{
		private BoxCollider boxCollider;
		private Transform visual;
		private MeshRenderer rend;

		public bool IsOccupied => transform.childCount > 1;

		private void Awake() => GetVariables();

		private void GetVariables()
		{
			boxCollider = GetComponent<BoxCollider>();
			visual = transform.GetChild(0);
			rend = visual.GetComponentInChildren<MeshRenderer>();
		}

		public void Occupy(GameObject prefab) => Instantiate(prefab, transform.position, Quaternion.identity, transform);

		public void Init(float size)
		{
			GetVariables();

			boxCollider.size = new Vector3(size, boxCollider.size.y, size);
			visual.localScale = boxCollider.size;
		}
	}
}