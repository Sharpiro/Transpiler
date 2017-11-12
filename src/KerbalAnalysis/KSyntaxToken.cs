using KerbalAnalysis.Nodes;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis
{
    public class KSyntaxToken : INodeOrToken
    {
        internal KSyntaxToken(KSyntaxKind kind, string text)
        {
            Kind = kind;
            Text = text;
        }

        public KSyntaxKind Kind { get; set; }
        public string Text { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public KNode Parent { get; set; }

        public override string ToString() => Text;
    }
}