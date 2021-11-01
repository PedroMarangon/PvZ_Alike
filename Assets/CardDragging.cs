// Maded by Pedro M Marangon
using UnityEngine;
using UnityEngine.EventSystems;

namespace S2P_Test
{
	public class CardDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		private RectTransform rectTransform;
		private Vector2 anchoredPosition;
		private Camera cam;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			cam = Camera.main;
		}

		public void OnBeginDrag(PointerEventData eventData) => anchoredPosition = rectTransform.anchoredPosition;

		public void OnDrag(PointerEventData eventData) => rectTransform.anchoredPosition = InputProvider.MousePosition;

		public void OnEndDrag(PointerEventData eventData)
		{
			rectTransform.anchoredPosition = anchoredPosition;

			Ray ray = cam.ScreenPointToRay(InputProvider.MousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				if(hit.collider != null && hit.collider.TryGetComponent(out GridPiece piece))
				{
					piece.SetEmissionColor(Color.red);
				}
			}
		}
	}
}