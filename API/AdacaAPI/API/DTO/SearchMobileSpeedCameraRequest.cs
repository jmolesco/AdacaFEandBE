namespace Adaca.Api.DTO
{
    public class SearchMobileSpeedCameraRequest
    {
        public OrderByField OrderByField { get; set; }  // for Order By , input fieldname column by value int and type of Order By
        public string Keyword { get; set; }
        public FilterByField FilterByField { get; set; }
    }
    public class FilterByField
    {
        public int? FieldName { get; set; }
        public string FilterKeyword { get; set; }
    }
    public class OrderByField
    {
        public int? FieldName { get; set; }
        public int? OrderBy { get; set; }
    }
}
