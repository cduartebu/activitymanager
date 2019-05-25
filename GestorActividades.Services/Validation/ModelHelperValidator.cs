using GestorActividades.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestorActividades.Services.Validation
{
    public static class ModelHelperValidator
    {
        public static ResponseDto<bool> ValidateComponentModel<T>(T model)
        {
            var response = new ResponseDto<bool>();
            var valContext = new ValidationContext(model, null, null);
            var validationsResults = new List<ValidationResult>();
            var isOk = Validator.TryValidateObject(model, valContext, validationsResults, true);
            response.Data = isOk;
            if (!isOk)
            {
                var errors = new StringBuilder();
                foreach (var error in validationsResults)
                {
                    errors.Append(string.Concat(error.ErrorMessage, " "));
                }

                response.StatusMessage = errors.ToString();
            }

            return response;
        }
    }
}
