using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace Sistema.TI.BackEnd.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        protected ValidationResult _notifications;

        public BaseController()
        {
            _notifications = new ValidationResult();
        }

        protected new IActionResult Response(object result = null)
        {
            try
            {
                if (!_notifications.Errors.Any())
                {
                    return Ok(new
                    {
                        success = true,
                        data = result
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    errors = _notifications.Errors.Select(n => n.ErrorMessage)
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected void AdicionarErrorFluentValidation(IList<ValidationFailure> errors)
        {
            foreach (var erro in errors)
            {
                _notifications.Errors.Add(erro);
            }
        }
    }

}
