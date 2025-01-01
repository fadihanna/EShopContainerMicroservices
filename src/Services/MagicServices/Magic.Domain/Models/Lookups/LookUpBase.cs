namespace Magic.Domain.Models.Lookups
{
    public class LookUpBase<T>
    {
        public T Id { get; set; }
        public string? NameEN { get; set; }
        public string? NameAR { get; set; }
        public bool IsActive { get; set; }
    }
}
