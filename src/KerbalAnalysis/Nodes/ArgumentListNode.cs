using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Immutable;

namespace KerbalAnalysis.Nodes
{
    public class ArgumentListNode : KNode
    {
        public KSyntaxToken OpenParenToken { get; set; }
        public ImmutableList<ArgumentNode> Arguments { get; private set; } = ImmutableList.Create<ArgumentNode>();
        public int Count { get; private set; }
        public KSyntaxToken CloseParenToken { get; set; }

        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(OpenParenToken).AddRange(Arguments).Add(CloseParenToken);

        internal ArgumentListNode()
        {
            Kind = KSyntaxKind.ArgumentList;
        }

        public ArgumentListNode AddArguments(params ArgumentNode[] arguments)
        {
            foreach (var argument in arguments)
            {
                argument.Parent = this;
                Arguments = Arguments.Add(argument);
                Count++;
            }
            return this;
        }

        public ArgumentListNode WithOpenParenToken(KSyntaxToken openParentToken)
        {
            openParentToken.Parent = this;
            OpenParenToken = openParentToken;
            return this;
        }

        public ArgumentListNode WithCloseParenToken(KSyntaxToken closeParentToken)
        {
            closeParentToken.Parent = this;
            CloseParenToken = closeParentToken;
            return this;
        }
    }
}