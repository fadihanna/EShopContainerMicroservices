namespace Provider.Application.Dtos
{
    public class DamenServiceRequestDto
    {
        public string Name { get; set; }
        public string RequestType { get; set; }
        public List<DataFieldDto> Inputs { get; set; }
    }

    public class DataFieldDto
    {
        public string FieldName { get; set; }
        public string InputType { get; set; }
    }

}
