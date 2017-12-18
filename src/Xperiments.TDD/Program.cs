using System;
using Xperiments.TDD.Models;

namespace Xperiments.TDD
{

    public class Program
    {
        private static Avaliador leiloeiro = new Avaliador();
        private static Usuario joao = new Usuario( 1, "Joao" );
        private static Usuario jose = new Usuario( 2, "José" );
        private static Usuario maria = new Usuario( 3, "Maria" );
        private static Usuario mario = new Usuario( 4, "Mario" );
        private static Usuario steeveJobs = new Usuario(5, "Steeve Jobs");
        private static Usuario steeveWonziak = new Usuario(6, "Steeve Wonziak");
        private static Usuario billGates = new Usuario(7, "Bill Gates");
        static void Main(string[] args)
        {
            DeveEntenderLancesEmOrdemCrescente();
            DeveCalcularMediaDosLances();
            DeveDevolverTodosLancesCasoNaoHajaNoMinimo3();
            DeveEncontrarOsTresMaioresLances();
            NaoDeveAceitarMaisDoQue5LancesDoMesmoUsuario();

           Console.ReadLine();
        }

        static void DeveEntenderLancesEmOrdemCrescente()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 3 Novo")
                            .Lance(maria, 250.00)
                            .Lance(joao, 300.00)
                            .Lance(jose, 400.00)
                            .Constroi();


            // executando a acao
            leiloeiro.Avalia(leilao);

            // comparando a saida com o esperado
            double maiorEsperado = 400;
            double menorEsperado = 250;

            Console.WriteLine(maiorEsperado == leiloeiro.MaiorLance);
            Console.WriteLine(menorEsperado == leiloeiro.MenorLance);
        }


        static void DeveCalcularMediaDosLances()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 3 Novo")
                            .Lance(maria, 5.00)
                            .Lance(joao, 4.00)
                            .Lance(jose, 6.00)
                            .Constroi();


            // executando a acao
            leiloeiro.Avalia(leilao);

            // comparando a saida com o esperado
            double mediaEsperada = 5;

            Console.WriteLine(mediaEsperada == leiloeiro.LanceMedio);
        }

        static void DeveDevolverTodosLancesCasoNaoHajaNoMinimo3()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 3 Novo")
                            .Lance(joao, 300.00)
                            .Lance(mario, 415.00)
                            .Constroi();

            // executando a acao
            leiloeiro.Avalia(leilao);

            var maiores = leiloeiro.TresMaiores;

            // comparando a saida com o esperado
            Console.WriteLine(2 == leiloeiro.TresMaiores.Count);
            Console.WriteLine(415 == maiores[0].Valor);
            Console.WriteLine(300 == maiores[1].Valor);
        }

        static void DeveEncontrarOsTresMaioresLances()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 3 Novo")
                            .Lance(maria, 250.00)
                            .Lance(joao, 300.00)
                            .Lance(jose, 400.00)
                            .Lance(joao, 415.00)
                            .Constroi();

            // executando a acao
            leiloeiro.Avalia(leilao);

            var maiores = leiloeiro.TresMaiores;

            // comparando a saida com o esperado
            Console.WriteLine(3 == leiloeiro.TresMaiores.Count);
            Console.WriteLine(415 == maiores[0].Valor);
            Console.WriteLine(400 == maiores[1].Valor);
            Console.WriteLine(300 == maiores[2].Valor);
        }

        static void NaoDeveAceitarMaisDoQue5LancesDoMesmoUsuario()
        {

            Leilao leilao = new CriadorDeLeilao().Para("Raspberry Pi 3")
                .Lance(steeveJobs, 2000.00)
                .Lance(billGates, 2500.00)
                .Lance(steeveJobs, 3000.00)
                .Lance(billGates, 3500.00)
                .Lance(steeveJobs, 4000.00)
                .Lance(billGates, 4500.00)
                .Lance(steeveJobs, 5000.00)
                .Lance(billGates, 5500.00)
                .Lance(steeveJobs, 6000.00)
                .Lance(billGates, 6500.00)
                .Constroi();

            //tem que ignorar
            leilao.Propoe(new Lance(steeveJobs, 9999.99));

            Console.WriteLine(10 == leilao.Lances.Count);

            var ultimo = leilao.Lances.Count - 1;

            var ultimoLance = leilao.Lances[ultimo];

            Console.WriteLine(6500 == ultimoLance.Valor);


        }
    }
}
