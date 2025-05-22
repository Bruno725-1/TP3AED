using System;
using System.Collections.Generic;
class Program {
    static void Main (string[] args) {
        Console.WriteLine("Off the king the power demanded the bastard, the king the ferry goes ma");
        int n = int.Parse(Console.ReadLine());
        List<Producao> producoes = new List<Producao>();
        for (int i = 0; i < n; i++) {
            string[] producao = Console.ReadLine().Split(';');
            producoes.Add(new Producao(producao));
        }
        int c = int.Parse(Console.ReadLine());
        Queue<Producao> fila = new Queue<Producao>();
        Stack<Producao> pilha = new Stack<Producao>();
        for (int i = 0; i < c; i++)
        {
            string[] comandos = Console.ReadLine().Split(' ');
            string comando = comandos[0];
            string parametro = comandos[1];
            if (comando == "push")
            {
                //codigo aqui
                for (int j = 0; j < producoes.Count; j++)
                {
                    
                }
            }
        }
    }
}
class Producao {
    //atributos aqui
    public string show_id;
    public string type;
    public string title;
    public string director;
    public string cast;
    public string country;
    public string date_added;
    public int release_year;
    public string rating;
    public string duration;
    public string listed_in;
    public string description;
    //função construtora
    public Producao(string[] producao) {
        this.show_id = producao[0];
        this.type = producao[1];
        this.title = producao[2];
        this.director = producao[3];
        this.cast = producao[4];
        this.country = producao[5];
        this.date_added = producao[6];
        this.release_year = int.Parse(producao[7]);
        this.rating = producao[8];
        this.duration = producao[9];
        this.listed_in = producao[10];
        this.description = producao[11];
    }
}