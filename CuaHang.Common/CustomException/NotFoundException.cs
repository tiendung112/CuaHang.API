using CuaHang.Common;

namespace CuaHang.Common
{
    public class NotFoundException : Exception
    {
        public string Name { get; private set; }

        public NotFoundException(string name)
        {
            Name = name;
        }
        public override string Message => string.Format(CommonContaint.ExceptionMessage.NOT_FOUND, Name);
    }
}
