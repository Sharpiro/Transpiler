using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;
using static KerbalAnalysis.Tools.ImmutableExtensions;

namespace KerbalAnalysis.Nodes
{
    public class ForStatementNode : StatementNode
    {
        public KSyntaxToken FromKeyword { get; private set; }
        public BlockNode DeclarationBlock { get; private set; }
        public KSyntaxToken UntilKeyword { get; private set; }
        public ExpressionNode Condition { get; private set; }
        public KSyntaxToken StepKeyword { get; private set; }
        public BlockNode IncrementBlock { get; private set; }
        public KSyntaxToken DoKeyword { get; private set; }
        public StatementNode Statement { get; private set; }
        public override ImmutableList<INodeOrToken> Children =>
            CreateList(FromKeyword, DeclarationBlock, UntilKeyword, Condition, StepKeyword, IncrementBlock, DoKeyword, Statement);

        internal ForStatementNode()
        {
            Kind = KSyntaxKind.ForStatement;
        }

        public ForStatementNode WithFromKeyword(KSyntaxToken fromKeyword)
        {
            fromKeyword.Parent = this;
            FromKeyword = fromKeyword;
            return this;
        }

        public ForStatementNode WithDeclarationBlock(BlockNode declaration)
        {
            declaration.Parent = this;
            DeclarationBlock = declaration;
            return this;
        }

        public ForStatementNode WithUntilKeyword(KSyntaxToken untilKeyword)
        {
            untilKeyword.Parent = this;
            UntilKeyword = untilKeyword;
            return this;
        }

        public ForStatementNode WithCondition(ExpressionNode condition)
        {
            condition.Parent = this;
            Condition = condition;
            return this;
        }

        public ForStatementNode WithStepKeyword(KSyntaxToken stepKeyword)
        {
            stepKeyword.Parent = this;
            StepKeyword = stepKeyword;
            return this;
        }

        public ForStatementNode WithIncrementBlock(BlockNode incrementBlock)
        {
            incrementBlock.Parent = this;
            IncrementBlock = incrementBlock;
            return this;
        }

        public ForStatementNode WithDoKeyword(KSyntaxToken doKeyword)
        {
            doKeyword.Parent = this;
            DoKeyword = doKeyword;
            return this;
        }

        public ForStatementNode WithStatement(StatementNode statementBlock)
        {
            statementBlock.Parent = this;
            Statement = statementBlock;
            return this;
        }
    }
}