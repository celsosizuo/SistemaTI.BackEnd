using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Sistema.TI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.TI.Repositories
{
    public class DepartamentoRepository
    {
        private ApplicationDBContext _dbContext;
        private DepartamentoValidator _departamentoValidator;

        public DepartamentoRepository()
        {
            _dbContext = new ApplicationDBContext();
        }

        public Departamento Add(Departamento departamento)
        {
            _departamentoValidator = new DepartamentoValidator(departamento);

            if (_departamentoValidator.EhValido())
            {
                _dbContext.Departamentos.Add(departamento);
                _dbContext.SaveChanges();
                return departamento;
            }
            else
            {
                string[] msgs = _departamentoValidator.GetErros().Split(';');
                string msg = "";
                msgs.ToList().ForEach(m => msg += m + "\n");
                throw new Exception(msg);
            }
        }

        public Departamento Update(Departamento departamento)
        {
            _departamentoValidator = new DepartamentoValidator(departamento);

            if (_departamentoValidator.EhValido())
            {
                _dbContext.Departamentos.Update(departamento);
                _dbContext.SaveChanges();
                return departamento;
            }
            else
            {
                string[] msgs = _departamentoValidator.GetErros().Split(';');
                string msg = "";
                msgs.ToList().ForEach(m => msg += m + "\n");
                throw new Exception(msg);
            }
        }

        public void Delete(int id)
        {
            _dbContext.Departamentos.Remove(_dbContext.Departamentos.ToList().Find(d => d.Id == id));
            _dbContext.SaveChanges();
        }

        public List<Departamento> Get()
        {
            return _dbContext.Departamentos.AsNoTracking().OrderBy(d => d.Nome).ToList();
        }
    }
}
