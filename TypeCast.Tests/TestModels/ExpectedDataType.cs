using Umbraco.Core.Models;

namespace TypeCast.TestTarget.TestModels
{
    public class ExpectedDataType
    {
        public string PropertyEditorAlias { get; set; }
        public string DataTypeName { get; set; }
        public DataTypeDatabaseType DbType { get; set; }
    }
}