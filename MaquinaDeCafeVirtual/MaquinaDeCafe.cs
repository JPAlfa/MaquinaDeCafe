using System;
using System.Linq;


namespace MaquinaDeCafeVirtual
{
    public class MaquinaDeCafe
    {
        #region Atributos
       
        public double valor;
        public string[] opcoes; 
        public double[] valores;

        #endregion

        #region Construtores

        public MaquinaDeCafe(string[] opcoes, double[] valores)
        {
            this.opcoes = opcoes;
            this.valores = valores;
        }

        #endregion

        #region Métodos Públicos

        public void EnviarMensagemInicial()
        {
            Console.WriteLine("Olá! Temos essas opções de café:");
            DarOpcoesDeCafe();
            Console.WriteLine("Se quiser depoositar uma moeda digite:\n"
                              + "1 para 1 centavo\n"
                              + "5 para 5 centavos\n"
                              + "10 para 10 centavos\n"
                              + "25 para 25 centavos\n"
                              + "50 para 50 centavos\n"
                              + "100 para 1 real\n");
        }

        public string SomarMoedas()
        {
            MostrarValor();
            Console.WriteLine("Insira uma moeda:");
            string moeda = Console.ReadLine();
            return LerMoeda(moeda);
        }        

        public string DefinirCafe()
        {
            DarOpcoesDeCafe();
            MostrarValor();
            VerificarValor();
            string café = Console.ReadLine();
            return EscolherCafe(café);
        }

        private void MostrarValor()
        {
            Console.WriteLine($"\nValor depositado: {valor.ToString("C")}");
        }

        public bool EnviarMensagemDeTransicao()
        {
            Console.WriteLine("Você gostaria de mais um café? Se sim tecle 1, se não tecle qualquer outra tecla.");
            string numero = Console.ReadLine();
            if (numero == "1")
            {
                return true;
            }
            else
            {
                Console.WriteLine("Até mais!");
                return false;
            }
        }

        #endregion

        #region Métodos Privados

        private void VerificarValor()
        {
            if (valor < valores.Max())
                Console.WriteLine("Escolha seu café ou coloque mais moedas");
            else
                Console.WriteLine("Escolha seu café");
        }

        private string EscolherCafe(string cafe)
        {
            int indexador = -1;
            for (int i = 0; i < opcoes.Length; i++)
            {
                if (opcoes[i] == cafe && valor >= valores[i])
                {
                    valor -= valores[i];
                    indexador = i;
                    break;
                }
                else if (opcoes[i] == cafe && (valores[i] - valor) == 1)
                    return string.Format("\nVocê não colocou dinheiro o suficiente para esta opção. Falta {0}\n", (valores[i] - valor).ToString("C"));
                else if (opcoes[i] == cafe)
                    return string.Format("\nVocê não colocou dinheiro o suficiente para esta opção. Faltam {0}\n", (valores[i] - valor).ToString("C"));
            }
            
            if (indexador == -1 && valor < valores.Max())
                return SomarMoedas(cafe);
            else if (indexador == -1)
                return "\nNão entendi que café você gostaria de tomar. Tente de novo.\n";

            string troco = DividirTrocoEmMoedas(valor);
            return EnviarMensagemDeFimDaCompra(indexador, troco);
        }

        private string EnviarMensagemDeFimDaCompra(int indexador, string troco)
        {
            if (valor > 0)
                return String.Format("\nAproveite seu {0}! Ah, e aqui está o seu troco:\n{1}.", opcoes[indexador], troco);
            else
                return String.Format("\nAproveite seu {0}!", opcoes[indexador]);
        }

        private string DividirTrocoEmMoedas(double valor)
        {
            string trocoReceber = string.Empty;
            while (valor != 0)
            {
                if (valor >= 1)
                {
                    int troco = (int)valor;
                    trocoReceber += $"{troco} moeda de R$1,00\n";
                    valor -= troco;
                }
                else if (valor < 1 && valor >= 0.50)
                {
                    trocoReceber += $"1 moeda de R$0,50\n";
                    valor -= 0.50;
                }
                else if (valor < 0.50 && valor >= 0.25)
                {
                    trocoReceber += $"1 moeda de R$0,25\n";
                    valor -= 0.25;
                }
                else if (valor < 0.25 && valor >= 0.10)
                {
                    if(valor >= 0.20)
                        trocoReceber += $"2 moeda de R$0,10\n";
                    else
                        trocoReceber += $"1 moeda de R$0,10\n";

                    valor = 0;
                }
            }

            return trocoReceber;
        }

        private void DarOpcoesDeCafe()
        {
            string escolhaSeuCafe = "";
            for (int i = 0; i < valores.Length; i++)
                escolhaSeuCafe += $"{opcoes[i]} - Preço: {valores[i].ToString("C")}\n";

            Console.WriteLine(escolhaSeuCafe);
        }

        private string LerMoeda(string moeda)
        {
            switch (moeda)
            {
                case "1":
                    return "Moeda rejeitada\n";
                case "5":
                    return "Moeda rejeitada\n";
                case "10":
                    valor += 0.10;
                    return "Moeda aceita\n";
                case "25":
                    valor += 0.25;
                    return "Moeda aceita\n";
                case "50":
                    valor += 0.50;
                    return "Moeda aceita\n";
                case "100":
                    valor += 1;
                    return "Moeda aceita\n";
                default:
                    return "Não entendi a moeda que você tentou depositar. Tente novamente.\n";
            }
        }

        private string SomarMoedas(string moeda)
        {
            MostrarValor();
            return LerMoeda(moeda);
        }

        #endregion
    }
}
