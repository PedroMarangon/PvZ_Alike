// Maded by Pedro M Marangon
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace S2P_Test
{
	public class GridSetup : MonoBehaviour
    {
		#region Constant Variables
		private const string GRID_PARENT_NAME = "Grid";
		private const string SPAWNER_PARENT_NAME = "Spawners";
		private const string HEADER_GRID = "Grid Generation";
		private const string HEADER_SPAWNER = "Spawner Generation";
		#endregion

		[Header(HEADER_GRID)]
		[SerializeField] private Vector2Int gridSize = new Vector2Int(3, 2);
		[Min(1), SerializeField] private float cellSize = 1;
		[Required, SerializeField] private GridPiece gridPiecePrefab;
		[SerializeField] private string gridPieceName = "GridPiece_{0}-{1}";
		[SerializeField] private Color color01 = Color.white;
		[SerializeField] private Color color02 = Color.black;
		[Header(HEADER_SPAWNER)]
		[MaxValue(0), SerializeField] private float spawnerXPos = -10;
		[SerializeField] private string enemySpawnerName = "EnemySpawner_Row-{0}";
		
		private Dictionary<Vector2Int, GridPiece> instantiatedGrid = new Dictionary<Vector2Int, GridPiece>();

#if UNITY_EDITOR
		#region Build / Destroy grid in Unity

		[Button]
		private void BuildGrid()
		{
			DestroyGrid();

			CreateGrid();
			CreateSpawners();
		}

		[Button]
		private void DestroyGrid()
		{
			if (GameObject.Find(GRID_PARENT_NAME) != null) DestroyImmediate(GameObject.Find(GRID_PARENT_NAME));
			if (GameObject.Find(SPAWNER_PARENT_NAME) != null) DestroyImmediate(GameObject.Find(SPAWNER_PARENT_NAME));

			instantiatedGrid = new Dictionary<Vector2Int, GridPiece>();
		}

		#endregion

		#region Create Grid and Spawners
		private void CreateGrid()
		{
			if (!gridPiecePrefab) return;

			Transform parent = new GameObject(GRID_PARENT_NAME).transform;
			parent.position = transform.position;

			for (int x = 0; x < gridSize.x; x++)
			{
				for (int y = 0; y < gridSize.y; y++)
				{
					Vector3 point = transform.position + (new Vector3(x, 0, y) * cellSize);

					GridPiece gridPiece = (GridPiece)UnityEditor.PrefabUtility.InstantiatePrefab(gridPiecePrefab);
					gridPiece.gameObject.name = string.Format(gridPieceName, x, y);
					gridPiece.transform.parent = parent;
					gridPiece.transform.position = point;

					Color gridColorPiece = (x + y) % 2 != 1 ? color01 : color02;
					gridPiece.Init(cellSize, gridColorPiece);					
					
					Vector2Int gridPos = new Vector2Int(x, y);
					instantiatedGrid.Add(gridPos, gridPiece);
				}
			}
		}

		private void CreateSpawners()
		{
			Transform parent = new GameObject(SPAWNER_PARENT_NAME).transform;
			parent.position = transform.position + (Vector3.right * spawnerXPos);


			for (int y = 0; y < gridSize.y; y++)
			{
				Vector3 point = transform.position;
				point.z += y * cellSize;
				point.x += spawnerXPos;

				GameObject spawnerPos = new GameObject(string.Format(enemySpawnerName, (y + 1)));
				spawnerPos.transform.parent = parent;
			}

			parent.gameObject.AddComponent<EnemySpawner>().SetupSpawnerList();

		}

		#endregion

		private void OnDrawGizmos()
		{
			if (transform.childCount > 0) return;
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