// Maded by Pedro M Marangon
using System;
using UnityEngine;

namespace S2P_Test
{
	public class TowerManager : MonoBehaviour
	{
		[System.Serializable]
		private struct MeshColor
		{
			public Color bgColor;
			[ColorUsage(false, true)] public Color hightlightColor;

			public MeshColor(Color color) => bgColor = hightlightColor = color;

		}

		private const string BG_COLOR = "_BGColor";
		private const string HIGHLIGHT_COLOR = "_HighlightColor";
		[SerializeField] private MeshRenderer meshVisualization;
		[SerializeField] private Transform visualTransform;
		[SerializeField] private MeshColor available = new MeshColor(Color.green);
		[SerializeField] private MeshColor unavailable = new MeshColor(Color.red);
		private Camera cam;
		private TowerCard card;
		private GameObject prefab;
		private RaycastHit hit;

		private void Awake()
		{
			cam = Camera.main;
			InputProvider.OnMouseClick += OnMouseClick;
		}

		private void OnMouseClick()
		{
			if (prefab == null) return;
			
			if(hit.collider.TryGetComponent(out GridPiece piece))
			{
				if (!piece.IsOccupied)
				{
					piece.Occupy(prefab);
					card?.StartCooldown();
					prefab = null;
					meshVisualization.enabled = false;
				}
			}
		}

		public void PrepareForPlacement(GameObject towerPrefab, TowerCard card)
		{
			this.card = card;
			prefab = towerPrefab;

			var meshFilter = towerPrefab.GetComponentInChildren<MeshFilter>();
			if (meshFilter)
			{
				var mesh = meshFilter.sharedMesh;

				meshVisualization.GetComponent<MeshFilter>().mesh = mesh;

				meshVisualization.enabled = true;
			}

		}

		private void Update()
		{
			Ray ray = cam.ScreenPointToRay(InputProvider.MousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider != null && hit.collider.TryGetComponent(out GridPiece piece))
				{
					SetVisualMaterial(!piece.IsOccupied);

					visualTransform.position = hit.collider.transform.position;
				}
			}

		}

		private void SetVisualMaterial(bool occupied)
		{
			Color bg = occupied ? available.bgColor : unavailable.bgColor;
			Color highlight = occupied ? available.hightlightColor : unavailable.hightlightColor;

			MaterialPropertyBlock propBlock = new MaterialPropertyBlock();

			meshVisualization.GetPropertyBlock(propBlock);
				propBlock.SetColor(BG_COLOR, bg);
				propBlock.SetColor(HIGHLIGHT_COLOR, highlight);
			meshVisualization.SetPropertyBlock(propBlock);
		}
	}

}