// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	[RequireComponent(typeof(BoxCollider))]
	public class GridPiece : MonoBehaviour
	{
		private const string BASE_COLOR = "_BaseColor";
		private const string EMISSION_COLOR = "_EmissionColor";

		[ColorUsage(true,true), SerializeField] private Color glowColor;

		private BoxCollider boxCollider;
		private Transform visual;
		private MeshRenderer rend;
		private Camera cam;

		private void Awake()
		{
			boxCollider = GetComponent<BoxCollider>();
			visual = transform.GetChild(0);
			rend = visual.GetComponentInChildren<MeshRenderer>();
			cam = Camera.main;
		}

		public void Init(float size, Color color)
		{
			boxCollider.size = new Vector3(size, boxCollider.size.y, size);
			visual.localScale = boxCollider.size;

			rend.material.SetColor(BASE_COLOR, color);
		}

		private void Update()
		{
			Ray ray = cam.ScreenPointToRay(InputProvider.MousePosition);
			Color color = Color.black;

			if(Physics.Raycast(ray, out RaycastHit hit) && hit.collider == boxCollider) color = glowColor;

			rend.material.SetColor(EMISSION_COLOR, color);
		}


	}
}