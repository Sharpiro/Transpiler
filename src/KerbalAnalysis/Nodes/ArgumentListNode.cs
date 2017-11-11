using System.Collections.Generic;

namespace KerbalAnalysis.Nodes
{
    public class ArgumentListNode : KNode
    {
        public List<ArgumentNode> Arguments { get; set; } = new List<ArgumentNode>();
        //public SyntaxToken OpenParenToken { get; set; }
        //public SyntaxToken CloseParenToken { get; set; }

        public ArgumentListNode()
        {
            Kind = KSyntaxKind.ArgumentList;
        }

        public void AddArgument(ArgumentNode argument)
        {
            Arguments.Add(argument);
        }
    }
}