using System.Text;

namespace KerbalAnalysis.Tools
{
    public static class StringBuilderExtensions
    {
        public static void AppendLineAndTabs(this StringBuilder builder, int tabIndex)
        {
            const string tab = "    ";
            builder.AppendLine();
            for (var i = 0; i < tabIndex; i++)
            {
                builder.Append(tab);
            }
        }

        public static void AppendSpace(this StringBuilder builder)
        {
            const string space = " ";
            builder.Append(space);
        }
    }
}