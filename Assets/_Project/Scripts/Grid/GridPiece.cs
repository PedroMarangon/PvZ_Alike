// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	[RequireComponent(typeof(BoxCollider))]
	public class GridPiece : MonoBehaviour
	{
		private const string BASE_COLOR = "_BaseColor";

		private BoxCollider boxCollider;
		private Transform visual;
		private MeshRenderer rend;

		public bool IsOccupied
		{
			get
			{
				Debug.Log(transform.childCount > 1, this);
				return transform.childCount > 1;
			}
		}

		private void Awake()
		{
			boxCollider = GetComponent<BoxCollider>();
			visual = transform.GetChild(0);
			rend = visual.GetComponentInChildren<MeshRenderer>();
		}

		public void Occupy(GameObject prefab)
		{
			Instantiate(prefab, transform.position, Quaternion.identity, transform);
		}

		public void Init(float size, Color color)
		{
			boxCollider.size = new Vector3(size, boxCollider.size.y, size);
			visual.localScale = boxCollider.size;

			rend.material.SetColor(BASE_COLOR, color);
		}
	}
}