using System;
using System.Linq;

namespace MaquinaDeCafeVirtual
{
    class Program
    {
        static void Main(string[] args)
        {
            //Teste com o café que quiser e com o preço que quiser
            string[] opcoes = { "Capuccino", "Mocha", "Café com Leite" };
            double[] valores = { 3.50, 4.00, 3.00 };
            MaquinaDeCafe maquinaDeCafe = new MaquinaDeCafe(opcoes, valores);

            maquinaDeCafe.EnviarMensagemInicial();
            do
            {
                maquinaDeCafe.valor = 0;
                while (maquinaDeCafe.valor < maquinaDeCafe.valores.Min())
                    Console.WriteLine(maquinaDeCafe.SomarMoedas());

                while (maquinaDeCafe.valor >= maquinaDeCafe.valores.Min())
                    Console.WriteLine(maquinaDeCafe.DefinirCafe());
            } while (maquinaDeCafe.EnviarMensagemDeTransicao());
        }
    }

    
}
