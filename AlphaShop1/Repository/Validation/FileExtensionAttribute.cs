using System.ComponentModel.DataAnnotations;

namespace AlphaShop1.Repository.Validation
{
	public class FileExtensionAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value is IFormFile file)
			{
				var extension = Path.GetExtension(file.FileName);
				string[] extensions = { "jpg", "png" };
				bool result = extension.Any(x => extension.EndsWith(x));

				if (!result)
				{
					return new ValidationResult("Allow extensions are jpg or png");
				}
			}
			return ValidationResult.Success;
		}
	}

}
