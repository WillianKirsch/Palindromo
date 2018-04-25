using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Palindromo
{
    class Program
    {
        static void Main(string[] args)
        {
            bool finalizar = false;
            while (!finalizar)
            {
                Executar();
                Console.WriteLine("Deseja verificar mais alguma frase? Digite a letra 'S' caso sua resposta seja SIM.");
                finalizar = !Console.ReadLine().Trim().ToLower().Equals("s");
            }
        }

        private static void Executar()
        {
            int quantidadeFrases = ObterQuantidadeFrases();

            List<String> listaRespostas = ObterListaRespostas(quantidadeFrases);

            EscreverResultadosConsole(listaRespostas);
        }

        private static int ObterQuantidadeFrases()
        {
            Console.WriteLine("Olá, poderias me informar a quantidade de frases que você deseja verificar?");
            int quantidadeFrases = 0;
            bool valorValido = false;
            while (!valorValido)
            {
                valorValido = int.TryParse(Console.ReadLine(), out quantidadeFrases);
                if (!valorValido) Console.WriteLine("Não consigui entender a quantidade desejada. Poderias me informar novamente?");
            }
            return quantidadeFrases;
        }

        private static void EscreverResultadosConsole(List<String> listaRespostas)
        {
            foreach (string resposta in listaRespostas)
            {
                Console.WriteLine(resposta);
            }
        }

        private static List<String> ObterListaRespostas(int quantidadeFrases)
        {
            List<String> listaRespostas = new List<string>();
            string frase = String.Empty;
            string resultado;

            Console.WriteLine(string.Format("Poderias me informar quais são as {0} frases? A cada frase informada, clique no botão 'ENTER'", quantidadeFrases));
            for (int posicao = 1; posicao <= quantidadeFrases; posicao++)
            {

                bool fraseVazia = true;
                while (fraseVazia)
                {
                    Console.Write(string.Format("Frase {0}. ", posicao));
                    frase = Console.ReadLine();
                    fraseVazia = String.IsNullOrWhiteSpace(frase);
                    if (fraseVazia) Console.WriteLine("A frase que você me informou está vazia. Poderias me informar novamente?");
                }

                resultado = frase.VerificaSePalindromo() ? "É palíndromo" : "Não é palíndromo";
                listaRespostas.Add(String.Format("{0} - ({1})", resultado, frase));
            }

            return listaRespostas;
        }
    }

    public static class Extensions
    {
        public static string Inverte(this string palavra)
        {
            char[] palavraSeparada = palavra.ToCharArray();
            Array.Reverse(palavraSeparada);
            return new String(palavraSeparada);
        }

        public static bool VerificaSePalindromo(this string frase)
        {
            frase = frase.ToLower().Replace(" ", "").RemoveAcentos();
            string fraseInversa = frase.Inverte();
            return frase.Equals(fraseInversa);
        }

        public static string RemoveAcentos(this string palavra)
        {
            string palavraNormalizada = palavra.Normalize(NormalizationForm.FormD);

            StringBuilder sb = new StringBuilder();

            for (int quantidadeCaracteres = 0; quantidadeCaracteres < palavraNormalizada.Length; quantidadeCaracteres++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(palavraNormalizada[quantidadeCaracteres]);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(palavraNormalizada[quantidadeCaracteres]);
            }
            return sb.ToString();
        }
    }
}
