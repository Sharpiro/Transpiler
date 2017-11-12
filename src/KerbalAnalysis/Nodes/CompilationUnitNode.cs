using System;
using System.Collections.Generic;
using KerbalAnalysis.Nodes.Abstract;
using System.Linq;

namespace KerbalAnalysis.Nodes
{
    public class CompilationUnitNode : KNode
    {
        public List<MemberDeclarationNode> Members { get; } = new List<MemberDeclarationNode>();
        public override List<INodeOrToken> Children => Members.Cast<INodeOrToken>().ToList();

        public CompilationUnitNode AddMember(MemberDeclarationNode member)
        {
            member.Parent = this;
            Members.Add(member);
            return this;
        }
    }
}