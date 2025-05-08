using System;
class Program {
    static void Main (string[] args) {
        Console.WriteLine("Programa iniciado");
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++) {
            string[] producao = Console.ReadLine().Split(';');
        }
        int c = int.Parse(Console.ReadLine());
        for (int i = 0; i < c; i++) {
            string[] comandos = Console.ReadLine().Split(' ');
        }
    }
}