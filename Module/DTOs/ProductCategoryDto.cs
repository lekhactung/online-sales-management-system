namespace Model.DTOs
{
    public class ProductCategoryDto
    {
        public string CategoryId { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }

    public class CreateProductCategoryDto
    {
        public string CategoryName { get; set; } = string.Empty;
    }
}
