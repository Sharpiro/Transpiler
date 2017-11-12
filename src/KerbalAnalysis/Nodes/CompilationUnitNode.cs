using System.Collections.Generic;
using KerbalAnalysis.Nodes.Abstract;
using System.Linq;
using System.Collections.Immutable;
using System;

namespace KerbalAnalysis.Nodes
{
    public class CompilationUnitNode : KNode
    {
        public ImmutableList<MemberDeclarationNode> Members { get; private set; } = ImmutableList.Create<MemberDeclarationNode>();
        public override ImmutableList<INodeOrToken> Children => Members.Cast<INodeOrToken>().ToImmutableList();

        public CompilationUnitNode AddMember(MemberDeclarationNode member)
        {
            member.Parent = this;
            Members = Members.Add(member);
            return this;
        }

        public CompilationUnitNode WithMembers(IEnumerable<GlobalStatementNode> kGlobalStatements)
        {
            Members = Members.Clear();
            foreach (var kGlobalStatement in kGlobalStatements)
            {
                AddMember(kGlobalStatement);
            }
            return this;
        }
    }
}