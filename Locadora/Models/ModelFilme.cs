﻿using System.ComponentModel.DataAnnotations;

namespace Locadora.Models
{
    public class ModelFilme
    {
        [Key]
        public int idFilme { get; set; }
        public string Nome { get; set; }
        public int CodigoFilme { get; set; }
        public bool Disponivel { get; set; } = true;
        public bool Ativo { get; set; } = true;
    }
}
