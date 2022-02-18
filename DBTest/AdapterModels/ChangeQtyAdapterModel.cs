namespace InspectionBlazor.AdapterModels
{
    public class ChangeQtyAdapterModel
    {
        public int ItemId { get; set; }
        public int PartId { get; set; }
        public decimal QtyChange { get; set; }
        public string Remark { get; set; }
        public string PartName { get; set; }
    }
}
