using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Creatures.Mobs.PathFinding
{
	public class PathFinder : MonoBehaviour
	{
		[SerializeField] private Transform _seeker, _target;

		private FlyingMobAI _seekerAi;

		private void Start()
		{
			_seekerAi = _seeker.gameObject.GetComponent<FlyingMobAI>();
		}

		private FindPathGrid _grid;
		private void Awake()
		{
			_grid = GetComponent<FindPathGrid>();
		}

		private void Update()
		{
			if (_seekerAi._agro && !_seekerAi._isDead)
			{
				FindPath(_seeker.position, _target.position);
			}

		}

		private void FindPath(Vector2 startPos, Vector2 targetPos)
		{
			Node startNode = _grid.NodeFromWorldPoint(startPos);
			Node targetNode = _grid.NodeFromWorldPoint(targetPos);

			List<Node> openSet = new List<Node>();
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);

			while (openSet.Count > 0)
			{
				Node currentNode = openSet[0];
				for (int i = 1; i < openSet.Count; i++)
				{
					if (openSet[i].FCost < currentNode.FCost || (openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost))
					{
						currentNode = openSet[i];
					}
				}
				openSet.Remove(currentNode);
				closedSet.Add(currentNode);


				if (currentNode == targetNode)
				{
					RetracePath(startNode, targetNode);
					return;
				}

				foreach (Node neighbour in _grid.GetNeighbours(currentNode))
				{
					if (!neighbour.Walkable || closedSet.Contains(neighbour))
					{
						continue;
					}
					int newCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);
					if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
					{
						neighbour.GCost = newCostToNeighbour;
						neighbour.HCost = GetDistance(neighbour, targetNode);
						neighbour.Parent = currentNode;
						if (!openSet.Contains(neighbour))
						{
							openSet.Add(neighbour);
						}
					}
				}

			}
		}

		void RetracePath(Node startNode, Node endNode)
		{
			List<Node> path = new List<Node>();
			Node currentNode = endNode;

			while (currentNode != startNode)
			{
				path.Add(currentNode);
				currentNode = currentNode.Parent;
			}
			path.Reverse();

			_grid.Path = path;
		}
		private int GetDistance(Node nodeA, Node nodeB)
		{
			int distX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
			int distY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

			if (distX > distY)
			{
				return 14 * distY + 10 * (distX - distY);
			}
			return 14 * distX + 10 * (distY - distX);

		}
	}
}