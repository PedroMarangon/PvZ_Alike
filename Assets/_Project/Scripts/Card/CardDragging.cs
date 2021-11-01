// Maded by Pedro M Marangon
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace S2P_Test
{
	[RequireComponent(typeof(TowerCard))]
	[RequireComponent(typeof(CanvasGroup))]
	public class CardDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		private RectTransform rectTransform;
		private Vector2 position;
		private Camera cam;
		private TowerCard towerCard;
		private CanvasGroup canvasGroup;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			towerCard = GetComponent<TowerCard>();
			canvasGroup = GetComponent<CanvasGroup>();
			cam = Camera.main;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (!towerCard.CanMoveCard) return;

			canvasGroup.blocksRaycasts = false;
			canvasGroup.alpha = 0.25f;
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


			Sequence s = DOTween.Sequence();

			s.AppendCallback(() =>
			{
				canvasGroup.alpha = 1;
				rectTransform.anchoredPosition = position;
				Ray ray = cam.ScreenPointToRay(InputProvider.MousePosition);

				if (Physics.Raycast(ray, out RaycastHit hit))
				{
					if(hit.collider != null && hit.collider.TryGetComponent(out GridPiece piece))
					{
						if(!piece.IsOccupied)
						{
							piece.Occupy(towerCard.Prefab);
							towerCard?.StartCooldown();
						}
					}
				}
			});
			s.AppendInterval(0.05f);
			s.AppendCallback(() => canvasGroup.blocksRaycasts = true );

		}
	}
}