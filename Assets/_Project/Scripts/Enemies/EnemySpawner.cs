// Maded by Pedro M Marangon
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S2P_Test
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private List<Transform> spawnPoints = new List<Transform>();
		[ValidateInput("DoesImplementInterface", "All of the GameObjects associated must have a component that implements IEnemyLogic!!")]
		[SerializeField] private List<GameObject> enemies = new List<GameObject>();
		[MinMaxSlider(0.5f, 7f), SerializeField] private Vector2 timeBetweenSpawn = new Vector2(2, 3);
		[SerializeField] private float startDelay = 1f;


		public void SetupSpawnerList()
		{
			spawnPoints.Clear();

			foreach (Transform child in transform)
			{
				spawnPoints.Add(child);
			}

		}

		private void Start()
		{
			StartCoroutine(Spawn());
		}

		private IEnumerator Spawn()
		{
			yield return new WaitForSeconds(startDelay);
			while(true)
			{
				float timeDelay = Random.Range(timeBetweenSpawn.x, timeBetweenSpawn.y);

				yield return new WaitForSeconds(timeDelay);

				var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
				var enemy = enemies[Random.Range(0, enemies.Count)];

				Instantiate(enemy, spawnPoint.position, Quaternion.identity);

			}
		}


		private bool DoesImplementInterface(List<GameObject> ensGO)
		{
			foreach (var en in ensGO)
			{
				if (en == null) continue;
				if (!en.TryGetComponent(out IEnemyLogic log)) return false;
			}
			return true;
		}
	}
}