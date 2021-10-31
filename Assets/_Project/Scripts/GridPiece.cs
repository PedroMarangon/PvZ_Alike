// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	[RequireComponent(typeof(BoxCollider))]
	public class GridPiece : MonoBehaviour
	{
		private BoxCollider boxCollider;
		private Transform visual;

		public void Init(float size, Color color)
		{
			boxCollider = GetComponent<BoxCollider>();
			visual = transform.GetChild(0);

			boxCollider.size = new Vector3(size, boxCollider.size.y, size);
			visual.localScale = boxCollider.size;

			MeshRenderer rend = visual.GetComponentInChildren<MeshRenderer>();
			rend.material.SetColor("_BaseColor", color);
		}
	}
}