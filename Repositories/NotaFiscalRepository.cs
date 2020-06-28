using Microsoft.EntityFrameworkCore;
using Sistema.TI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.TI.Repositories
{
    public class NotaFiscalRepository
    {
        private ApplicationDBContext _dbContext;

        public NotaFiscalRepository()
        {
            _dbContext = new ApplicationDBContext();
        }

        public int Add(NotaFiscal notaFiscal)
        {
            _dbContext.NotasFiscais.Add(notaFiscal);
            _dbContext.SaveChanges();

            return notaFiscal.Id;
        }

        public void Update(NotaFiscal notaFiscal)
        {
            _dbContext.NotasFiscais.Update(notaFiscal);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _dbContext.NotasFiscais.Remove(_dbContext.NotasFiscais.ToList().Find(nf => nf.Id == id));
            _dbContext.SaveChanges();
        }

        public List<NotaFiscal> Get()
        {
            return _dbContext.NotasFiscais.AsNoTracking().OrderBy(nf => nf.Data).ToList();
        }
    }
}
