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
		//[SerializeField] private Unit occupyingUnit;

		private BoxCollider boxCollider;
		private Transform visual;
		private MeshRenderer rend;
		private Camera cam;

		//public bool IsOccupied => occupyingUnit != null;
		//public bool IsOccupiedByEnemy => occupyingUnit is EnemyUnit;
		//public bool IsOccupiedByTower => occupyingUnit is TowerUnit;

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
			rend.material.SetColor(EMISSION_COLOR, Color.black);
		}

		public void SetEmissionColor(Color color) => rend.material.SetColor(BASE_COLOR, color);
	}
}