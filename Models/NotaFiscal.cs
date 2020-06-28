using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.TI.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public string NumeroNF { get; set; }
        public string Fornecedor { get; set; }
        public DateTime Data { get; set; }
        public string LinkImagem { get; set; }
    }

    public class NotaFiscalValidator : AbstractValidator<NotaFiscal>
    {
        protected FluentValidation.Results.ValidationResult ValidationResult { get; set; }
        private NotaFiscal _notaFiscal;

        public NotaFiscalValidator(NotaFiscal notaFiscal)
        {
            ValidationResult = new FluentValidation.Results.ValidationResult();
            _notaFiscal = notaFiscal;
        }

        public bool EhValido()
        {
            Validar();
            ValidationResult = Validate(_notaFiscal);

            return ValidationResult.IsValid;
        }

        private void Validar()
        {
            ValidarNumeroNF();
            ValidarFornecedor();
            ValidarData();
        }

        public string GetErros()
        {
            var erros = "";
            ValidationResult.Errors.ToList().ForEach(e => erros += e.ErrorMessage + ";");
            return erros;
        }

        private void ValidarNumeroNF()
        {
            RuleFor(a => a.NumeroNF)
                .NotNull().WithMessage("Campo Número da NF é obrigatório")
                .Length(9).WithMessage("Campo Número da NF deve conter 9 dígitos. Complete com zeros a esquerda");
        }

        private void ValidarFornecedor()
        {
            RuleFor(a => a.Fornecedor).NotEmpty().WithMessage("Campo Fornecedor é obrigatório");
        }

        private void ValidarData()
        {
            RuleFor(a => a.Data)
                .NotEmpty().WithMessage("Campo Data é obrigatório");
        }
    }
}
