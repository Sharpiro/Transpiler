using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Generic;

namespace KerbalAnalysis.Nodes
{
    public class BlockNode : StatementNode
    {
        public KSyntaxToken OpenBraceToken { get; set; }
        public ImmutableList<StatementNode> Statements { get; set; }
        public KSyntaxToken CloseBraceToken { get; set; }
        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(OpenBraceToken).AddRange(Statements).Add(CloseBraceToken);

        internal BlockNode() => Kind = KSyntaxKind.Block;

        public BlockNode WithOpenBrace(KSyntaxToken openBrace)
        {
            openBrace.Parent = this;
            OpenBraceToken = openBrace;
            return this;
        }

        public BlockNode WithStatements(params StatementNode[] statements)
        {
            var statementList = new List<StatementNode>();
            foreach (var statement in statements)
            {
                statement.Parent = this;
                statementList.Add(statement);
            }
            Statements = statementList.ToImmutableList();
            return this;
        }

        public BlockNode WithCloseBrace(KSyntaxToken closeBrace)
        {
            closeBrace.Parent = this;
            CloseBraceToken = closeBrace;
            return this;
        }
    }
}