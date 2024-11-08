using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace OilChangeApp.Controllers
{
    public class CustomerDetailsViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CarName { get; set; }

        [Required(ErrorMessage = "لطفاً کیلومتر سرویس را وارد کنید.")]
        public string Kilometers { get; set; }

        [Required(ErrorMessage = "لطفاً کیلومتر بعدی سرویس را وارد کنید.")]
        public string NextServiceKilometers { get; set; }

        public string PhoneNumber { get; set; }
        public List<CustomerServiceHistoryViewModel> CustomerServiceHistory { get; set; }
        public List<ServiceViewModel> ServiceList { get; set; }
        public SelectList Oils { get; set; }
        public int? SelectedOilId { get; set; }
        public List<int> SelectedServiceIds { get; set; }

        public bool SendMessage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var persianCulture = new CultureInfo("fa-IR");

            if (!long.TryParse(Kilometers.Replace(",", "").Replace("٬", ""), NumberStyles.Number, persianCulture, out long kilometers))
            {
                yield return new ValidationResult(
                    "لطفاً یک مقدار عددی معتبر برای کیلومتر سرویس وارد کنید.",
                    new[] { "Kilometers" });
            }

            if (!long.TryParse(NextServiceKilometers.Replace(",", "").Replace("٬", ""), NumberStyles.Number, persianCulture, out long nextServiceKilometers))
            {
                yield return new ValidationResult(
                    "لطفاً یک مقدار عددی معتبر برای کیلومتر بعدی سرویس وارد کنید.",
                    new[] { "NextServiceKilometers" });
            }

            // اگر تبدیل‌ها موفق بودند، مقایسه را انجام می‌دهیم
            if (nextServiceKilometers <= kilometers)
            {
                yield return new ValidationResult(
                    "کیلومتر سرویس بعدی باید بزرگتر از کیلومتر سرویس باشد.",
                    new[] { "NextServiceKilometers" });
            }
        }
    }
}