using System;
using System.Security.Cryptography;

namespace ConsoleInputComponent
{
    internal class Input<T>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Dado { get; set; }
        public ConsoleColor CorFrente { get; set; }
        public ConsoleColor CorFundo { get; set; }
        public string Rotulo { get; set; }
        public bool AutoAvancar { get; set; }
        public int MaximoDeCaracteres { get; set; }

        public event EventHandler<string> EventoDadoAlterado;

        public Input(string rotulo, int x, int y, bool autoAvancar, int maximoDeCaracteres)
        {
            CorFundo = ConsoleColor.White;
            CorFrente = ConsoleColor.Blue;
            Rotulo = rotulo;
            X = x;
            Y = y;
            AutoAvancar = autoAvancar;
            MaximoDeCaracteres = maximoDeCaracteres;
            Dado = "";
            Draw();
        }

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            if (Rotulo.Length > 0)
            {
                imprimirCaracteres(Rotulo + ": ");
            }

            if (typeof(T) == typeof(DateTime))
            {
                imprimirCaracteres("dd/mm/yyyy");
                MaximoDeCaracteres = 10;
            }
        }

        public void apagarUltimoCaractere()
        {
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }

        public void imprimirCaracteres(string texto)
        {
            Console.BackgroundColor = CorFundo;
            Console.ForegroundColor = CorFrente;
            Console.Write(texto);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void focar()
        {
            Console.SetCursorPosition((X + Rotulo.Length + 2 + Dado.Length), Y);
            lerEscrita();
        }

        public void lerEscrita()
        {
            while (true)
            {
                var tecla = Console.ReadKey();
                if (tecla.Key == ConsoleKey.Enter)
                {
                    break;
                }
                if (tecla.Key == ConsoleKey.Backspace)
                {
                    if (Dado.Length > 0)
                    {
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Dado = Dado.Substring(0, Dado.Length - 1);
                        EventoDadoAlterado?.Invoke(this, Dado);
                    } 
                    else
                    {
                        imprimirCaracteres(" ");
                    }
                }
                else
                {
                    if (Char.IsControl(tecla.KeyChar))
                    {
                        continue;
                    }
                    if (((Dado.Length == MaximoDeCaracteres) && (MaximoDeCaracteres > 0))
                       || (((typeof(T) == typeof(int)) || (typeof(T) == typeof(decimal)))
                       && ((!Char.IsDigit(tecla.KeyChar))
                       && ((Dado.Length > 0) || (tecla.KeyChar != '-'))
                       && (typeof(T) != typeof(decimal) || (tecla.KeyChar != ',' || Dado.Contains(",") || Dado.Length == 0)))))
                    {
                        apagarUltimoCaractere();
                        continue;
                    }
                    apagarUltimoCaractere();
                    imprimirCaracteres(tecla.KeyChar.ToString());
                    Dado += tecla.KeyChar.ToString();
                    if ((Dado.Length == 2 || Dado.Length == 5) && (typeof(T) == typeof(DateTime)))
                    {
                        Dado += "/";
                        imprimirCaracteres("/");
                    }
                    EventoDadoAlterado?.Invoke(this, Dado);
                    if ((AutoAvancar) && (Dado.Length == MaximoDeCaracteres))
                    {
                        break;
                    }
                    
                }
            }
        }
    }
}
