using System;
using System.Collections.Generic;
using System.Linq;
using Xperiments.TDD.Models;

namespace Xperiments.TDD
{
    public class Avaliador 
    {
        private double maiorDeTodos = Double.MinValue;
        private double menorDeTodos = Double.MaxValue;
        private double media = 0;
        private IList<Lance> maiores;

        public void Avalia(Leilao leilao) 
        {
            if(leilao.Lances.Count == 0)
                throw new Exception("Não é possível avaliar um leilão sem nenhum lance");

            double somaDosLances = 0;

            foreach(Lance lance in leilao.Lances) 
            {
                if(lance.Valor > maiorDeTodos) 
                    maiorDeTodos = lance.Valor;

                if(lance.Valor < menorDeTodos) 
                    menorDeTodos = lance.Valor;

                somaDosLances += lance.Valor;
            }
            
            pegaOsMaioresNo(leilao);
            media = somaDosLances / leilao.Lances.Count;
        }
        private void pegaOsMaioresNo(Leilao leilao)
        {
            var filtro = leilao.Lances.OrderByDescending(p => p.Valor).Take(3);
            maiores = new List<Lance>(filtro);
        }
        public double MaiorLance { get { return maiorDeTodos; } }
        public double MenorLance { get { return menorDeTodos; } }
        public double LanceMedio { get { return media; } }
        public IList<Lance> TresMaiores { get { return this.maiores; }
    }
    }
}