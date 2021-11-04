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
		#region Constant Values
		private const float SCALE_DURATION = 0.1f;
		private const float HIGHLIGHT_SCALE = 1.2f;
		private const float NORMAL_SCALE = 1f;
		#endregion

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

			if (towerManager.IsReadyToPlacePrefab) moneySystem.GiveMoneyBack();

			towerManager?.PrepareForPlacement(towerCard.Prefab, towerCard);
			moneySystem?.RemoveMoney(towerCard.Cost);
		}

		public void OnPointerExit(PointerEventData eventData) => rectTransform.DOScale(NORMAL_SCALE, SCALE_DURATION);

		public void OnPointerEnter(PointerEventData eventData)
		{
			if(towerCard.IsInCooldown || towerCard.DoesHaveEnoughMoney) return;
			rectTransform.DOScale(HIGHLIGHT_SCALE, SCALE_DURATION);
		}
	}
}