namespace AStarPathFinding
{
	public class Node
	{
		public Node(int cellIndex, Node parent)
		{
			CellIndex = cellIndex;
			Parent = parent;
		}

		public int F { get; set; }
		public int G { get; set; }
		public int H { get; set; }
		public Node Parent { get; set; }
		public int CellIndex { get; set; }
	}
}