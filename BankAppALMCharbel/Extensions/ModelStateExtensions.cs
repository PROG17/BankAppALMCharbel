using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BankAppALMCharbel.Extensions
{
    public static class ModelStateExtensions
    {
        public static void ValidateRemote(this ModelStateDictionary state, string propertyName, IActionResult validationResult)
        {
            if (!(validationResult is JsonResult json))
                throw new ArgumentException($"Expected JsonResult, got {validationResult?.GetType().Name ?? "null"}", nameof(validationResult));

            switch (json.Value)
            {
                case string error:
                    state.AddModelError(propertyName, error);
                    break;

                case bool success when success:
                    return;

                default:
                    throw new ArgumentException($"Unknown result type, {json.Value?.GetType().Name ?? "null"}", nameof(validationResult));
            }
        }
    }
}