namespace TreeC4_5.Data;

public class Tree
{
    public Node Root;
    public class Node
    {
        public string Value;
        public List<NodeTransition> Transitions;
    }

    public class NodeTransition
    {
        public string TransitionValue;
        public Node Node;
    }

    public void PrintTree()
    {
        Console.WriteLine("BegginPrinting");
        PrintNode(0, Root, "Root");
    }
    

    private void PrintNode(int spaces, Node node, string transitionValue)
    {
        if(node.Value == null) return;
        string tabulation = string.Concat(Enumerable.Repeat("\t" , spaces));
        Console.WriteLine(tabulation + transitionValue  + " " +  node.Value);
        foreach (var nodeTransition in node.Transitions)
        {
            PrintNode(spaces + 1, nodeTransition.Node, nodeTransition.TransitionValue);
            
        } 
    }


}