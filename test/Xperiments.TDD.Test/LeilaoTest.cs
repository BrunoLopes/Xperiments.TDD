using System;
using Xperiments.TDD;
using Xperiments.TDD.Models;
using NUnit.Framework;

namespace Xperiments.TDD.Test
{

    [TestFixture]
    public class LeilaoTest
    {
        private Usuario steeveJobs = new Usuario(1, "Steeve Jobs");
        private Usuario steeveWonziak = new Usuario(2, "Steeve Wonziak");
        private Usuario billGates = new Usuario(3, "Bill Gates");

        [SetUp]
        public void CriaUsuarios()
        {

        }

        [Test]
        public void DeveReceberUmLance()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Mackbook pro 15")
                            .Lance(steeveJobs, 15000.00)
                            .Constroi();

            Assert.AreEqual(1, leilao.Lances.Count);
            Assert.AreEqual(15000.00, leilao.Lances[0].Valor, 0.00001);

        }

        [Test]
        public void DeveReceberVariosLances()
        {
            // cenario: 3 lances em ordem crescente 
            Leilao leilao = new CriadorDeLeilao().Para("Mackbook pro 15")
                            .Lance(steeveJobs, 2000.00)
                            .Lance(steeveWonziak, 3000.00)
                            .Constroi();

           Assert.AreEqual(2, leilao.Lances.Count);
           Assert.AreEqual(2000.00, leilao.Lances[0].Valor, 0.00001);
           Assert.AreEqual(3000.00, leilao.Lances[1].Valor, 0.00001);


        }

        [Test]
        public void NaoDeveAceitarDoisLancesSeguidosDoMesmoUsuario()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Mackbook pro 15")
                            .Lance(steeveJobs, 2000.00)
                            .Lance(steeveJobs, 3000.00)
                            .Constroi();

           Assert.AreEqual(1, leilao.Lances.Count);
           Assert.AreEqual(2000.00, leilao.Lances[0].Valor, 0.00001);
           Assert.AreEqual(2000.00, leilao.Lances[0].Valor, 0.00001);
           

        }

        
        [Test]
        public void DobrarUltimoLanceDoUsuario()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Mackbook pro 15")
                            .Lance(steeveWonziak, 2000.00)
                            .Lance(steeveJobs, 3000.00)
                            .Constroi();

           leilao.DobrarLance(steeveWonziak);

           Assert.AreEqual(3, leilao.Lances.Count);
           Assert.AreEqual(2000.00 * 2, leilao.Lances[leilao.Lances.Count - 1].Valor, 0.00001);

        }

        public void DobrarUltimoLanceDoUsuarioSemLances()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Mackbook pro 15")
                            .Lance(steeveWonziak, 2000.00)
                            .Constroi();

            leilao.DobrarLance(steeveJobs);

            Assert.AreEqual(1, leilao.Lances.Count);
            Assert.AreEqual(2000.00, leilao.Lances[leilao.Lances.Count - 1].Valor, 0.00001);

        }

        [Test]
        public void NaoDeveAceitarMaisDoQue5LancesDoMesmoUsuario()
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

            Assert.AreEqual(10, leilao.Lances.Count);

            var ultimo = leilao.Lances.Count - 1;

            var ultimoLance = leilao.Lances[ultimo];

            Assert.AreEqual(6500, ultimoLance.Valor, 0.00001);


        }
       
    }
}
