using Luban.Job.Common.Types;
using Luban.Job.Common.TypeVisitors;

namespace Luban.Job.Cfg.TypeVisitors
{
    class CsDeserializeVisitor : DecoratorFuncVisitor<string, string, string>
    {
        public static CsDeserializeVisitor Ins { get; } = new CsDeserializeVisitor();

        public override string DoAccept(TType type, string bufName, string fieldName)
        {
            if (type.IsNullable)
            {
                return $"if({bufName}.ReadBool()){{ {type.Apply(CsUnderingDeserializeVisitor.Ins, bufName, fieldName)} }} else {{ {fieldName} = null; }}";
            }
            else
            {
                return type.Apply(CsUnderingDeserializeVisitor.Ins, bufName, fieldName);
            }
        }

        public override string Accept(TBean type, string bufName, string fieldName)
        {
            return type.Apply(CsUnderingDeserializeVisitor.Ins, bufName, fieldName);
        }
    }
}
