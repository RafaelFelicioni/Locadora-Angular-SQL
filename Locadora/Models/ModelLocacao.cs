using System;
using System.ComponentModel.DataAnnotations;

namespace Locadora.Models
{
    public class ModelLocacao
    {
        [Key]
        public int IdLocacao { get; set; }
        public int IdFilme { get; set; }
        public int IdCliente { get; set; }
        public int CodigoFilme { get; set; }
        public DateTime DataLocacao { get; set; } = DateTime.Now;
        public DateTime DataMaxDevolucao { get; set; }
        public DateTime DevolvidoEm { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
