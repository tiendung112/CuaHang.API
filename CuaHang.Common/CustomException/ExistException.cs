using CuaHang.Common;

namespace CuaHang.Common.CustomException
{
    public class ExistException : Exception
    {
        public string Name { get; private set; }

        public ExistException(string name)
        {
            Name = name;
        }
        public override string Message => string.Format(CommonContaint.ExceptionMessage.ALREADY_EXIST, Name);
    }
}
