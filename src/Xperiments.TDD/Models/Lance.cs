using System;
using System.Collections.Generic;

namespace Xperiments.TDD.Models
{

    public class Lance
    {
        public double Valor { get; private set; }
        
        public Usuario Usuario { get; private set;}

        public Lance(Usuario usuario, double valor)
        {
            if(valor <= 0)
                throw new ArgumentException("O valor de um lance não pode ser igual ou menor que 0.");

            this.Valor = valor;
            this.Usuario = usuario;
        }
        
    }


}


