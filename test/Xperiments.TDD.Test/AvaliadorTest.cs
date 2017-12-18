using System;
using Xperiments.TDD;
using Xperiments.TDD.Models;
using NUnit.Framework;

namespace Xperiments.TDD.Test
{

    [TestFixture]
    public class AvaliadorTest
    {
        private Avaliador leiloeiro;
        private Usuario joao;
        private Usuario jose;
        private Usuario maria;
        private Usuario mario;

        [SetUp]
        public void CriaObjetos()
        {
            Console.WriteLine("Criando objetos avaliador");
            leiloeiro = new Avaliador();
            joao = new Usuario( 1, "Joao" );    
            jose = new Usuario( 2, "José" );
            maria = new Usuario( 3, "Maria" );
            mario = new Usuario( 4, "Mario" );
        }
        

        [Test]
        public void DeveEntenderLancesEmOrdemCrescente()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Xbox 360")
                            .Lance(maria, 250.00)
                            .Lance(joao, 300.00)
                            .Lance(jose, 400.00)
                            .Constroi();


            // executando a acao
            leiloeiro.Avalia(leilao);

            // comparando a saida com o esperado
            double maiorEsperado = 400;
            double menorEsperado = 250;

            Assert.AreEqual(maiorEsperado, leiloeiro.MaiorLance, 0.0001);
            Assert.AreEqual(menorEsperado, leiloeiro.MenorLance, 0.0001);
        }

        [Test]
        public void DeveCalcularMediaDosLances()
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

            Assert.AreEqual(mediaEsperada, leiloeiro.LanceMedio, 0.0001);
        }

        [Test]
        public void DeveEncontrarOsTresMaioresLances()
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
            Assert.AreEqual(3, leiloeiro.TresMaiores.Count);
            Assert.AreEqual(415, maiores[0].Valor, 0.00001);
            Assert.AreEqual(400, maiores[1].Valor, 0.00001);
            Assert.AreEqual(300, maiores[2].Valor, 0.00001);
        }

        [Test]
        public void DeveDevolverTodosLancesCasoNaoHajaNoMinimo3()
        {

            Leilao leilao = new CriadorDeLeilao().Para("Playstation 3 Novo")
                            .Lance(joao, 300.00)
                            .Lance(mario, 415.00)
                            .Constroi();

            // executando a acao
            leiloeiro.Avalia(leilao);

            var maiores = leiloeiro.TresMaiores;

            // comparando a saida com o esperado
            Assert.AreEqual(2, leiloeiro.TresMaiores.Count);
            Assert.AreEqual(415, maiores[0].Valor, 0.00001);
            Assert.AreEqual(300, maiores[1].Valor, 0.00001);
        }

        // [Test]
        // public void DeveDevolverListaVaziaCasoNaoHajaLances()
        // {
        //     Leilao leilao = new CriadorDeLeilao().Para("Playstation 3 Novo")
        //                     .Constroi();

        //     // executando a acao
        //     leiloeiro.Avalia(leilao);

        //     // comparando a saida com o esperado
        //     Assert.AreEqual(0, leiloeiro.TresMaiores.Count);
        // }

        [Test]
        public void NaoDeveAvaliarLeilaoSemLances()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 3 Novo").Constroi();

            // executando a acao e verificando se é disparada a exceção com a correta mensagem.
            Assert.Throws(Is.TypeOf<Exception>()
                            .And.Message.EqualTo("Não é possível avaliar um leilão sem nenhum lance"),
                delegate { leiloeiro.Avalia(leilao); });
        }


        [Test]
        public void DeveDevolverExceptionComLanceComValorZero()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 3 Novo")
                                                 .Lance(joao, 0)
                                                 .Constroi();

            // executando a acao e verificando se é disparada a exceção com a correta mensagem.
            Assert.Throws(Is.TypeOf<ArgumentException>()
                            .And.Message.EqualTo("O valor de um lance não pode ser igual ou menor que 0."),
                delegate { leiloeiro.Avalia(leilao); });
        }

        [TearDown]
        public void Finaliza()
        {
            Console.WriteLine("Finalizando AvaliadorTest.");
        }

    }
}
