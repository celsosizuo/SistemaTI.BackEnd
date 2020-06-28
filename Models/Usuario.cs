using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
    Classe: Usuario
    Criada por: Celso Sizuo
    Data: 20/06/2020
    Propósito: Para cadastramento de usuários de computadores, celulares, notebooks, etc.
 */

namespace Sistema.TI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public int DepartamentoId { get; set; }

        [StringLength(20)]
        public string Matricula { get; set; }
        
        [StringLength(100)]
        public string Nome { get; set; }
        
        [StringLength(11)]
        public string Cpf { get; set; }
        
        public int? Terceiro { get; set; }

        public virtual Departamento Departamento { get; set; }
    }


    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        protected FluentValidation.Results.ValidationResult ValidationResult { get; set; }
        private Usuario _usuario;

        public UsuarioValidator(Usuario usuario)
        {
            ValidationResult = new FluentValidation.Results.ValidationResult();
            _usuario = usuario;
        }

        public bool EhValido()
        {
            Validar();
            ValidationResult = Validate(_usuario);

            return ValidationResult.IsValid;
        }

        private void Validar()
        {
            ValidarNome();
            ValidarCpf();
            ValidarDepartamento();
        }

        public string GetErros()
        {
            var erros = "";
            ValidationResult.Errors.ToList().ForEach(e => erros += e.ErrorMessage + ";");
            return erros;
        }

        private void ValidarCpf()
        {
            RuleFor(a => a.Cpf)
                .NotEmpty().WithMessage("Campo CPF é obrigatório")
                .Length(11).WithMessage("Campo CPF tem que conter 11 dígitos");
        }
        private void ValidarDepartamento()
        {
            RuleFor(a => a.Departamento).NotNull().WithMessage("Campo Departamento é obrigatório");
        }

        private void ValidarNome()
        {
            RuleFor(a => a.Nome).NotEmpty().WithMessage("Campo Nome do Usuário é obrigatório");
        }
    }
}
