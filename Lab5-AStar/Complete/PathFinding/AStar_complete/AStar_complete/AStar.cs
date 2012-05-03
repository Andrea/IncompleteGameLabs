using System.Collections.Generic;
using System.Linq;

namespace AStarPathFinding
{
	public class AStar
	{
		private readonly Map _map;

		private readonly List<Node> _openList = new List<Node>();
		private readonly List<Node> _closedList = new List<Node>();
		private Node _currentNode;

		public AStar(Map map)
		{
			_map = map;
		}

		public void CalculatePath()
		{
			var firstNode = new Node(_map.StartCell, null);
			_openList.Add(firstNode);
			_currentNode = firstNode;
			
			while(true)
			{
				if (_openList.Count == 0)
				{
					break;
				}

				_currentNode = FindSmallestF();
				if (_currentNode.CellIndex == _map.TargetCell)
				{
					break;
				}
				
				_openList.Remove(_currentNode);
				_closedList.Add(_currentNode);

				AddAdjacentCellToOpenList(_currentNode, -1, -1, 14);
				AddAdjacentCellToOpenList(_currentNode, 0, -1, 10);
				AddAdjacentCellToOpenList(_currentNode, 1, -1, 14);
				AddAdjacentCellToOpenList(_currentNode, -1, 0, 10);
				AddAdjacentCellToOpenList(_currentNode, 1, 0, 10);
				AddAdjacentCellToOpenList(_currentNode, -1, 1, 14);
				AddAdjacentCellToOpenList(_currentNode, 0, 1, 10);
				AddAdjacentCellToOpenList(_currentNode, 1, 1, 14);
			}

			_map.HighlightPath(_currentNode);
		}

		private Node FindSmallestF()
		{
			var smallestF = int.MaxValue;
			Node selectedNode = null;

			foreach (var node in _openList)
			{
				if (node.F < smallestF)
				{
					selectedNode = node;
					smallestF = node.F;
				}
			}

			return selectedNode;
		}

		private void AddAdjacentCellToOpenList(Node parentNode, int columnOffset, int rowOffset, int gCost)
		{
			var adjacentCellIndex = _map.GetAdjacentCellIndex(parentNode.CellIndex, columnOffset, rowOffset);
			
			// ignore unwalkable nodes (or nodes outside the grid)
			if (adjacentCellIndex == -1)
				return;

			// ignore nodes on the closed list
			if (_closedList.Any(n => n.CellIndex == adjacentCellIndex))
				return;

			var adjacentNode = _openList.SingleOrDefault(n => n.CellIndex == adjacentCellIndex);
			if (adjacentNode != null)
			{
				if(parentNode.G + gCost < adjacentNode.G)
				{
					adjacentNode.Parent = parentNode;
					adjacentNode.G = parentNode.G + gCost;
					adjacentNode.F = adjacentNode.G + adjacentNode.H;
				}

				return;
			}

			var node = new Node(adjacentCellIndex, parentNode) { G = gCost, H = _map.GetDistance(adjacentCellIndex, _map.TargetCell) };
			node.F = node.G + node.H;
			_openList.Add(node);
		}
	}
}
