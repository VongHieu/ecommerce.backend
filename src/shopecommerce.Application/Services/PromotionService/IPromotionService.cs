using shopecommerce.Domain.Models;

namespace shopecommerce.Application.Services.PromotionService;

public interface IPromotionService
{
    Task<IEnumerable<PromotionDto>> GetAllAsync();
}