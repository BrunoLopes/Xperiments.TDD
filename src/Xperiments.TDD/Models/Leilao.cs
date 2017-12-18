using System;
using System.Collections.Generic;
using System.Linq;

namespace Xperiments.TDD.Models
{

    public class Leilao
    {
        public string Descricao { get; private set;}
        public IList<Lance> Lances { get; private set; }
        
        public Leilao(string descricao)
        {
            this.Descricao = descricao;
            this.Lances = new List<Lance>();
        }

        public void Propoe(Lance lance)
        {
            if(Lances.Count == 0 || 
                podeDarLance(lance.Usuario) )
                Lances.Add(lance);
        }

        public void DobrarLance(Usuario usuario)
        {
            if(podeDarLance(usuario))
            {
                var lan = UltimoLancesDoUsuario(usuario);

                if(null != lan)
                    Propoe( new Lance(lan.Usuario, lan.Valor * 2) );
            }
        }

        private bool podeDarLance(Usuario usuario)
        {
            return (!UltimoLanceDado().Usuario.Equals(usuario) && 
                QuantidadeDeLancesDoUsuario(usuario) < 5);
        }
        private Lance UltimoLanceDado()
        {
            return Lances[Lances.Count-1];
        }

        private int QuantidadeDeLancesDoUsuario(Usuario usuario)
        {
            int total = 0;

            foreach (Lance lan in this.Lances)
            {
                if(lan.Usuario.Equals(usuario)) total ++;
            }

            return total;
        }

        private Lance UltimoLancesDoUsuario(Usuario usuario)
        {
            Lance lance = null;
        

            foreach (Lance lan in this.Lances)
            {
                if(lan.Usuario.Equals(usuario)) 
                    lance = lan;
            }

            return lance;
        }

    }


}


