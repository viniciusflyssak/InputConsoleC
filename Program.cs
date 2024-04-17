using ConsoleInputComponent;
using System;

namespace SistemaInput
{
    class Program
    {
        static void Main(string[] args)
        {
            Input<string> inputNome = new Input<string>("Digite seu nome", 0, 0, true, 20);
            Input<string> inputSobrenome = new Input<string>("Digite seu sobrenome", 0, 1, false, 50);
            Input<DateTime> inputDataNascimento = new Input<DateTime>("Digite sua data de nascimento", 0, 2, true, 0);
            Input<int> inputIdade = new Input<int>("Digite sua idade", 0, 3, false, 3);
            Input<decimal> inputAltura = new Input<decimal>("Digite sua altura", 0, 4, false, 4);

            inputNome.EventoDadoAlterado += EventoChange;
            inputNome.CorFrente = ConsoleColor.Red;
            inputNome.Draw(); 

            inputNome.focar();
            inputSobrenome.focar();
            inputDataNascimento.focar();
            inputIdade.focar();
            inputAltura.focar();
        }

        private static void EventoChange(object sender, string dado)
        {
            Console.Title = "Dado alterado para '" + dado + "'";
        }
    }
}
