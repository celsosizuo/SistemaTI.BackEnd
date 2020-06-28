using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.TI.Models;
using Sistema.TI.Repositories;
using FluentValidation.Results;

namespace Sistema.TI.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : BaseController
    {
        DepartamentoRepository _departamentoRepository;
        DepartamentoValidator _departamentoValidator;

        public DepartamentoController()
        {
            _departamentoRepository = new DepartamentoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_departamentoRepository.Get());
        }

        [HttpPost]
        public IActionResult Add([FromBody] Departamento departamento)
        {
            try
            {
                var result = _departamentoRepository.Add(departamento);
                return Response(result);
            }
            catch (Exception ex)
            {
                _notifications.Errors.Add(new ValidationFailure("erro", ex.Message));
                return Response(null);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Departamento departamento)
        {
            try
            {
                var result = _departamentoRepository.Update(departamento);
                return Response(result);
            }
            catch (Exception ex)
            {
                _notifications.Errors.Add(new ValidationFailure("erro", ex.Message));
                return Response(null);
            }
        }
    }
}
