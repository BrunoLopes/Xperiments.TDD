using System;
using System.Collections.Generic;
using System.Linq;
using Xperiments.TDD.Models;

namespace Xperiments.TDD
{
    public class CriadorDeLeilao 
    {
        private Leilao leilao;

        public CriadorDeLeilao Para(string descricao )
        {
            this.leilao = new Leilao(descricao);
            return this;
        }

        public CriadorDeLeilao Lance(Usuario usuario, double valor)
        {
            this.leilao.Propoe(new Lance(usuario, valor));
            return this;
        }

        public Leilao Constroi()
        {
            return this.leilao;
        }
    }

}