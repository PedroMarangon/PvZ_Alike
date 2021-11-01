// Maded by Pedro M Marangon
using UnityEngine;
using UnityEngine.EventSystems;

namespace S2P_Test
{
	[RequireComponent(typeof(TowerCard))]
	public class CardDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		private RectTransform rectTransform;
		private Vector2 position;
		private Camera cam;
		private TowerCard towerCard;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			towerCard = GetComponent<TowerCard>();
			cam = Camera.main;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (!towerCard.CanMoveCard) return;

			position = rectTransform.anchoredPosition;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (!towerCard.CanMoveCard) return;

			rectTransform.anchoredPosition = InputProvider.MousePosition;
		}

		public void OnEndDrag(PointerEventData eventData)
		{

			if (!towerCard.CanMoveCard) return;

			rectTransform.anchoredPosition = position;
			Ray ray = cam.ScreenPointToRay(InputProvider.MousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				if(hit.collider != null && hit.collider.TryGetComponent(out GridPiece piece))
				{
					piece.SetEmissionColor(Color.red);
				}
			}

			towerCard?.StartCooldown();
		}
	}
}