using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Sistema.TI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.TI.Repositories
{
    public class UsuarioRepository
    {
        private ApplicationDBContext _dbContext;

        public UsuarioRepository()
        {
            _dbContext = new ApplicationDBContext();
        }

        public int Add(Usuario usuario)
        {
            // Esta linha serve para que o Entity Framework não tente adicionar um departamento novo e sim pegue a referência;
            _dbContext.Entry(usuario.Departamento).State = EntityState.Modified; 

            _dbContext.Usuarios.Add(usuario);
            _dbContext.SaveChanges();

            _dbContext.Entry(usuario.Departamento).State = EntityState.Detached;

            return usuario.Id;
        }

        public void Update(Usuario usuario)
        {
            // Atualizando o contexto para que não dê erro de track;
            var user = _dbContext.Usuarios.Find(usuario.Id);
            user.Departamento = _dbContext.Departamentos.Find(usuario.Departamento.Id);

            _dbContext.Usuarios.Update(user);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _dbContext.Usuarios.Remove(_dbContext.Usuarios.ToList().Find(d => d.Id == id));
            _dbContext.SaveChanges();
        }
        public List<Usuario> Get()
        {
            // Incluindo o Departamento para que seja retornado;
            _dbContext.Usuarios.Include("Departamento").ToList();
            return _dbContext.Usuarios.OrderBy(u => u.Nome).ToList();
        }
    }
}
