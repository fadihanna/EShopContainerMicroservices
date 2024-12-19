namespace Magic.Domain.Models.Lookups
{
    public class LookUpBase<T>
    {
        public T Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool IsActive { get; set; }
    }
}
