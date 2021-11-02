// Maded by Pedro M Marangon
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace S2P_Test
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private List<Transform> spawnPoints = new List<Transform>();
		[MinMaxSlider(0.5f, 7f), SerializeField] private Vector2 timeBetweenSpawn = new Vector2(2, 3);

		public void SetupSpawnerList()
		{
			spawnPoints.Clear();

			foreach (Transform child in transform)
			{
				spawnPoints.Add(child);
			}

		}
	}
}