using FluentValidation;
using TheNewYorkTimes.Models;

namespace TheNewYorkTimes.Validations
{
    public class BaseValidator<T> : AbstractValidator<T> where T : BaseModel
    {
        
    }
}
