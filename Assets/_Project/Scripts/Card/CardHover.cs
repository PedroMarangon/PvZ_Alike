// Maded by Pedro M Marangon
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace S2P_Test
{
	[RequireComponent(typeof(TowerCard))]
	[RequireComponent(typeof(CanvasGroup))]
	public class CardHover : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler //IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		private RectTransform rectTransform;
		private TowerCard towerCard;
		private TowerManager towerManager;
		private MoneySystem moneySystem;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			towerCard = GetComponent<TowerCard>();
			towerManager = FindObjectOfType<TowerManager>();
			moneySystem = FindObjectOfType<MoneySystem>();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (towerCard.IsInCooldown || towerCard.DoesHaveEnoughMoney) return;
			towerManager?.PrepareForPlacement(towerCard.Prefab, towerCard);
			moneySystem?.RemoveMoney(towerCard.Cost);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if(towerCard.IsInCooldown || towerCard.DoesHaveEnoughMoney) return;
			rectTransform.DOScale(1f, 0.1f);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if(towerCard.IsInCooldown || towerCard.DoesHaveEnoughMoney) return;
			rectTransform.DOScale(1.2f, 0.1f);
		}
	}
}