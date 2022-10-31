namespace TreeC4_5.Data;

public class Tree<T1, T2>
{
    public Node Root;
    public class Node
    {
        public T1 Value;
        public List<NodeTransition> Transitions;
    }

    public class NodeTransition
    {
        public T2 TransitionValue;
        public Node Node;
    }

    public void PrintTree()
    {
        Console.WriteLine("BegginPrinting");
        PrintNode(0, Root);
    }
    

    private void PrintNode(int spaces, Node node)
    {
        if(node.Value == null) return;
        string tabulation = string.Concat(Enumerable.Repeat("\t" , spaces));
        // Console.WriteLine(tabulation + node.Value);
        Console.WriteLine(node.Transitions.Count);
        foreach (var nodeTransition in node.Transitions)
        {
            PrintNode(spaces + 1, nodeTransition.Node);
            
        } 
    }


}