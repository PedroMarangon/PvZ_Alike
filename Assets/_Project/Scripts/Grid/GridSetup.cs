// Maded by Pedro M Marangon
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace S2P_Test
{
	public class GridSetup : MonoBehaviour
    {
		[Header("Grid generation")]
		[SerializeField] private Vector2Int gridSize = new Vector2Int(3, 2);
		[Min(1), SerializeField] private float cellSize = 1;
		[SerializeField] private GridPiece gridPiecePrefab;
		[SerializeField] private Color color01 = Color.white;
		[SerializeField] private Color color02 = Color.black;


		[Header("Spawner generation")]
		[SerializeField] private float spawnerXPos = -10;
		[SerializeField] private EnemySpawner enemySpawnerPrefab;

		private Dictionary<Vector2Int, GridPiece> instantiatedGrid = new Dictionary<Vector2Int, GridPiece>();

		private void Awake()
		{
			CreateGrid();

			CreateSpawners();
		}

		private void CreateGrid()
		{
			if (!gridPiecePrefab) return;

			for (int x = 0; x < gridSize.x; x++)
			{
				for (int y = 0; y < gridSize.y; y++)
				{
					Vector3 point = transform.position + (new Vector3(x, 0, y) * cellSize);

					GridPiece gridPiece = Instantiate(gridPiecePrefab, point, Quaternion.identity).GetComponent<GridPiece>();
					gridPiece.Init(cellSize, ((x + y) % 2 == 1) ? color02 : color01);					
					
					Vector2Int gridPos = new Vector2Int(x, y);
					
					instantiatedGrid.Add(gridPos, gridPiece);

				}
			}


		}

		private void CreateSpawners()
		{
			if (!enemySpawnerPrefab) return;

			for (int y = 0; y < gridSize.y; y++)
			{
				Vector3 point = transform.position;
				point.z += y * cellSize;
				point.x += spawnerXPos;

				GameObject instantiatedSpawner = Instantiate(enemySpawnerPrefab, point, Quaternion.identity).gameObject;
				instantiatedSpawner.name = $"{enemySpawnerPrefab.name} - ROW {y + 1}";
			}
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			if (UnityEditor.EditorApplication.isPlaying) return;
			Vector3 cellSizeVector = new Vector3(cellSize, 0, cellSize);
			for (int x = 0; x < gridSize.x; x++)
			{
				for (int y = 0; y < gridSize.y; y++)
				{
					Gizmos.color = ((x + y) % 2 == 1) ? color02 : color01;
					Vector3 point = transform.position + (new Vector3(x, 0, y) * cellSize);
					Gizmos.DrawCube(point + (Vector3.up * 0.001f), cellSizeVector);


					Gizmos.color = Color.red;
					point.x = transform.position.x;
					point.x += spawnerXPos;
					Gizmos.DrawSphere(point, 0.25f);

				}
			}
		}
#endif

	}
}