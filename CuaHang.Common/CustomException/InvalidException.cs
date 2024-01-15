using CuaHang.Common;

namespace CuaHang.Common.CustomException
{
    public class InvalidException : Exception
    {

        public string Name { get; private set; }

        public InvalidException(string name)
        {
            Name = name;
        }
        public override string Message => string.Format(CommonContaint.ExceptionMessage.INVALID, Name);

    }
}
