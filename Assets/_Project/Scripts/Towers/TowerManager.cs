// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	public class TowerManager : MonoBehaviour
	{
		//Used for the preview of where to put the towers
		[System.Serializable]
		private struct MeshColor
		{
			public Color bgColor;
			[ColorUsage(false, true)] public Color hightlightColor;

			public MeshColor(Color color) => bgColor = hightlightColor = color;
		}

		#region Constant Values

		#region Material Parameters
		private const string BG_COLOR = "_BGColor";
		private const string HIGHLIGHT_COLOR = "_HighlightColor";
		#endregion

		private const int RAYCAST_DISTANCE = 999;

		#endregion

		[SerializeField] private MeshRenderer meshVisualization;
		[SerializeField] private Transform visualTransform;
		[SerializeField] private LayerMask gridMask;
		[SerializeField] private LayerMask towerMask;
		[SerializeField] private MeshColor available = new MeshColor(Color.green);
		[SerializeField] private MeshColor unavailable = new MeshColor(Color.red);

		private Camera cam;
		private TowerCard card;
		private GameObject prefab;
		private RaycastHit hit;
		private IGridMoveable crntSelectedGridMoveable;
		private bool isShowingPreview;

		public bool IsReadyToPlacePrefab => prefab != null;

		private void Awake()
		{
			cam = Camera.main;
			InputProvider.OnMouseClick += OnMouseClick;
			InputProvider.OnMouseRightClick += OnMouseRightClick;
		}

		private void OnMouseRightClick()
		{
			if (!cam) cam = Camera.main;

			Ray ray = cam.ScreenPointToRay(InputProvider.MousePosition);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, RAYCAST_DISTANCE, towerMask))
			{
				if (hitInfo.collider != null && hitInfo.collider.TryGetComponent(out IGridMoveable gridMoveable))
				{
					crntSelectedGridMoveable = gridMoveable;
					ActivateMeshVisualization(hitInfo.collider.gameObject);
				}
			}
		}
		private void OnMouseClick()
		{
			if (hit.collider == null || !isShowingPreview) return;

			if (hit.collider.TryGetComponent(out GridPiece piece))
			{
				if (!piece.IsOccupied)
				{
					if (prefab)
					{
						piece.Occupy(prefab);
						card?.StartCooldown();
						prefab = null;
					}
					else if(crntSelectedGridMoveable != null)
					{
						crntSelectedGridMoveable?.Move(piece.transform);
						crntSelectedGridMoveable = null;
					}
					meshVisualization.enabled = false;
					isShowingPreview = false;
				}
			}
		}

		public void PrepareForPlacement(GameObject towerPrefab, TowerCard card)
		{
			this.card = card;
			prefab = towerPrefab;

			ActivateMeshVisualization(towerPrefab);
		}

		private void Update()
		{
			if (!isShowingPreview) return;

			Ray ray = cam.ScreenPointToRay(InputProvider.MousePosition);

			if (Physics.Raycast(ray, out hit, RAYCAST_DISTANCE, gridMask))
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
		private void ActivateMeshVisualization(GameObject go)
		{
			isShowingPreview = true;
			var meshFilter = go.GetComponentInChildren<MeshFilter>();
			if (meshFilter)
			{
				var mesh = meshFilter.sharedMesh;

				meshVisualization.GetComponent<MeshFilter>().mesh = mesh;

				meshVisualization.enabled = true;
			}
		}
	}

}