namespace CuaHang.Models.ResponseModels
{
    public class BasePaginationResponseModel<T>
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; }
        public BasePaginationResponseModel(int pageNo, int pageSize, List<T> data, int totalItem)
        {
            TotalPages = (totalItem % pageSize) == 0 ? (totalItem / pageSize) : (totalItem / pageSize) + 1;
            PageNo = pageNo > TotalPages ? TotalPages : pageNo;
            PageSize = pageSize;
            TotalItems = totalItem;
            Data = data;
        }

    }
}
