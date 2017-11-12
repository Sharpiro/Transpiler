namespace KerbalAnalysis.Nodes.Abstract
{
    public interface INodeOrToken
    {
        KNode Parent { get; set; }
        KSyntaxKind Kind { get; set; }
    }
}