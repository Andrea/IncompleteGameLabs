namespace AStar_Incomplete
{
    public class Node
    {
        public Node(int cellIndex, Node parent)
        {
            CellIndex = cellIndex;
            Parent = parent;
        }

		// TODO: What does this variables mean?

        public int F { get; set; } 
        public int G { get; set; }
        public int H { get; set; }  
        public Node Parent { get; set; }
        public int CellIndex { get; set; }
    }
}