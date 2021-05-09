using System.Collections.Generic;
using System.Collections;
using UnityEngine;


namespace Platformer.Creatures.Mobs.PathFinding
{
	public class FindPathGrid : MonoBehaviour
	{
		
		[SerializeField] private LayerMask _unwalkableMask;
		[SerializeField] private Vector2 _gridWorldSize;
		[SerializeField] private float _nodeRadius;
		private Node[,] _grid;

		private float _nodeDiameter;
		private int gridSizeX, gridSizeY;

		public List<Node> Path;

		private void Start()
		{
			_nodeDiameter = _nodeRadius * 2;
			gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
			gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);

			CreateGrid();
		}
		private void CreateGrid()
		{
			_grid = new Node[gridSizeX, gridSizeY];
			Vector2 worldBottomLeft = new Vector2(transform.position.x, transform.position.y) - Vector2.right * _gridWorldSize.x / 2 - Vector2.up * _gridWorldSize.y / 2;
			for (int x = 0; x < gridSizeX; x++)
			{
				for (int y = 0; y < gridSizeY; y++)
				{
					Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * _nodeDiameter + _nodeRadius) + Vector2.up * (y * _nodeDiameter + _nodeRadius);
					bool walkable = !Physics2D.OverlapCircle(worldPoint, _nodeRadius - 0.1f, _unwalkableMask);
					_grid[x, y] = new Node(walkable, worldPoint, x, y);
				}
			}
		}

		public List<Node> GetNeighbours(Node node)
		{
			List<Node> neighbours = new List<Node>();

			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					if (x == 0 && y == 0)
					{
						continue;
					}
					int checkX = node.GridX + x;
					int checkY = node.GridY + y;

					if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
					{
						neighbours.Add(_grid[checkX, checkY]);
					}

				}
			}
			return neighbours;
		}

		public Node NodeFromWorldPoint(Vector2 worldPosition)
		{
			float percentX = (worldPosition.x + _gridWorldSize.x / 2) / _gridWorldSize.x;
			float percentY = (worldPosition.y + _gridWorldSize.y / 2) / _gridWorldSize.y;


			percentX = Mathf.Clamp01(percentX);
			percentY = Mathf.Clamp01(percentY);
			int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
			int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

			return _grid[x, y];

		}
		
#if UNITY_EDITOR
		private void OnDrawGizmos()
		{

			Gizmos.DrawWireCube(transform.position, new Vector2(_gridWorldSize.x, _gridWorldSize.y));
			if (_grid != null)
			{
				
				foreach (Node n in _grid)
				{


					Gizmos.color = n.Walkable ? new Color(0, 1, 0, 0.2f) : new Color(1, 0, 0, 0.2f);

					if(Path!=null)
					{
						if(Path.Contains(n))
						{
							Gizmos.color = new Color(0, 0, 1, 0.2f);
						}
					}
					
					Gizmos.DrawCube(n.WorldPosition, Vector2.one * (_nodeDiameter - .1f));


				}
			}

		}
#endif
	}
}