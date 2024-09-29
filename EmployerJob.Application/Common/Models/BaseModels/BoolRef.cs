

namespace EmployerJob.Application.Common.Models.BaseModels
{
    public class BoolRef
    {
        public bool Result { get; set; }
        public string ResultMessage { get; set; }
        public BoolRef(bool value)
        {
            Result = value;
        }

        public BoolRef(bool value, string resultMessage)
        {
            Result = value;
            ResultMessage = resultMessage;
        }

        public BoolRef(string resultMessage)
        {
            Result = false;
            ResultMessage = resultMessage;
        }

        public static implicit operator BoolRef(bool val)
        {
            return new BoolRef(val);
        }

        public static implicit operator BoolRef(string message)
        {
            return new BoolRef(false, message);
        }

        public static bool operator true(BoolRef val) => val.Result;
        public static bool operator false(BoolRef val) => !val.Result;

    }
}
