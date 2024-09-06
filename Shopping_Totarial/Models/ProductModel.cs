using Microsoft.Identity.Client;
using Shopping_Tutarial.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_Tutarial.Models
{
    public class ProductModel
    {
        [Key]
        public long Id { get; set; }

        [Required, MinLength(4, ErrorMessage = "Yêu cầu nhập Tên sản phẩm")]
		public string Name { get; set; }
        public string Slug { get; set; }
		[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập Mô tả Sản phẩm")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Yêu cầu nhập Giá Sản phẩm")]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(8,2)")]
		public decimal Price { get; set; }
        [Required, Range(1 ,int.MaxValue, ErrorMessage = "Chọn một thương hiệu")]
        public int BrandId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một thương hiệu")]
        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }
        public BrandModel Brand { get; set; }
        public string Image { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile? IMageUpload { get; set; }
    }
}
