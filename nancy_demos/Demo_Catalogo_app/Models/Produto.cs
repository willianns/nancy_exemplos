using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Demo_Catalogo_app.Models
{
    public class Produto
    {
        [Required(ErrorMessage="Descrição é obrigatório")]
        public string Descricao { get; set; }

        public decimal Preco { get; set; }
    }
}
