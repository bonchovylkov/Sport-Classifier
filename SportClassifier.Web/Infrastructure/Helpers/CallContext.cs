using SportClassifier.Web.Infrastructure.Enums;
using System.Web.Script.Serialization;

namespace SportClassifier.Web.Infrastructure.Helpers
{
    public class CallContext
    {
        [ScriptIgnore]
        public Enumerations.CrudActionType Type { get; set; }

        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string DebugMessage { get; set; }
        public string SuccessMessage { get; set; }
        
        public string TypeValue
        {
            get
            {
                if (Type != null)
                {
                    return this.Type.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public object Item { get; set; }
    }
}