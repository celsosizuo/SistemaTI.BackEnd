using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Sistema.TI.Models
{
    public class Departamento
    {
        
        public int Id { get; set; }

        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(20)]
        public string CodCentroCusto { get; set; }

        [StringLength(100)]
        public string NomeCentroCusto { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

    }

    public class DepartamentoValidator : AbstractValidator<Departamento>
    {
        protected FluentValidation.Results.ValidationResult ValidationResult { get; set; }
        private Departamento _departamento;

        public DepartamentoValidator(Departamento departamento)
        {
            ValidationResult = new FluentValidation.Results.ValidationResult();
            _departamento = departamento;
        }

        public bool EhValido()
        {
            Validar();
            ValidationResult = Validate(_departamento);

            return ValidationResult.IsValid;
        }

        private void Validar()
        {
            ValidarNome();
            ValidarCodCentroDeCusto();
            ValidarCentroDeCusto();
        }

        public string GetErros()
        {
            var erros = "";
            ValidationResult.Errors.ToList().ForEach(e => erros += e.ErrorMessage + ";");
            return erros;
        }

        private void ValidarNome()
        {
            RuleFor(a => a.Nome).NotEmpty().WithMessage("Campo Nome do Departamento é obrigatório");
        }

        private void ValidarCodCentroDeCusto()
        {
            RuleFor(a => a.CodCentroCusto).NotEmpty().WithMessage("Campo Código do Centro de Custo é obrigatório");
        }

        private void ValidarCentroDeCusto()
        {
            RuleFor(a => a.NomeCentroCusto).NotEmpty().WithMessage("Campo Nome do Centro de Custo é obrigatório");
        }
    }
}
