using E_Commrece.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commrece.Extentions
{
    public static class PresentationServicesExtention
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection Services)
        {
            Services.AddControllers().AddApplicationPart(typeof(Persentation.AssmblyRefrence).Assembly);
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrors;

            });
            return Services;
        }
    }
}
