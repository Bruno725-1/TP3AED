using System;
using System.Globalization;
using System.Collections.Generic;
class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        Producao[] producoes = new Producao[n];
        for (int i = 0; i < n; i++)
        {
            string[] producao = Console.ReadLine().Split(';');
            producoes[i] = new Producao(producao);
            producoes[i].date_added = Producao.ChangeData(producoes[i].date_added);
        }
        int c = int.Parse(Console.ReadLine());
        Queue<Producao> fila = new Queue<Producao>();
        Stack<Producao> pilha = new Stack<Producao>();
        for (int i = 0; i < c; i++)
        {
            string[] comandos = Console.ReadLine().Split(' ');
            string comando = comandos[0];
            if (comando == "push")
            {
                if (int.TryParse(comandos[1], out int parametro))
                {
                    //percorrer o vetor para verificar quais filmes foram lançados naquele ano
                    for (int j = 0; j < producoes.Length; j++)
                    {
                        if (producoes[j].release_year == parametro)
                            pilha.Push(producoes[j]);
                    }
                }
                else
                {
                    for (int j = 0; j < producoes.Length; j++)
                    {
                        if (producoes[j].type == comandos[1])
                            pilha.Push(producoes[j]);
                    }
                }
            }
            else if (comando == "queue")
            {
                if (int.TryParse(comandos[1], out int parametro))
                {
                    for (int j = 0; j < producoes.Length; j++)
                    {
                        if (producoes[j].release_year == parametro)
                            fila.Enqueue(producoes[j]);
                    }
                }
                else
                {
                    for (int j = 0; j < producoes.Length; j++)
                    {
                        if (producoes[j].type == comandos[1])
                            fila.Enqueue(producoes[j]);
                    }
                }
            }
            else if (comando == "pop")
            {
                if (int.TryParse(comandos[1], out int parametro))
                {
                    for (int j = 0; j < parametro; j++)
                    {
                        Producao p = pilha.Pop();
                        Console.WriteLine(p.ToStringFormatado());
                    }
                }
                else
                {
                    while (pilha.Count > 0)
                    {
                        Producao p = pilha.Pop();
                        Console.WriteLine(p.ToStringFormatado());
                    }
                }
            }
            else
            {
                if (int.TryParse(comandos[1], out int parametro))
                {
                    for (int j = 0; j < parametro; j++)
                    {
                        Producao p = fila.Dequeue();
                        Console.WriteLine(p.ToStringFormatado());
                    }
                }
                else
                {
                    while (fila.Count > 0)
                    {
                        Producao p = fila.Dequeue();
                        Console.WriteLine(p.ToStringFormatado());
                    }
                }
            }
        }
    }
}
class Producao
{
    //atributos aqui
    public string show_id;
    public string type;
    public string title;
    public string director;
    public string cast;
    public string lengthry;
    public string date_added;
    public int release_year;
    public string rating;
    public string duration;
    public string listed_in;
    public string description;
    //função construtora
    public Producao(string[] producao)
    {
        show_id = producao[0];
        type = producao[1];
        title = producao[2];
        director = producao[3];
        cast = producao[4];
        lengthry = producao[5];
        date_added = producao[6];
        release_year = int.Parse(producao[7]);
        rating = producao[8];
        duration = producao[9];
        listed_in = producao[10];
        description = producao[11];
    }
    public static string ChangeData(string data)
    {
        try
    {
        DateTime dt = DateTime.Parse(data, System.Globalization.CultureInfo.InvariantCulture);
        return dt.ToString("dd/MM/yyyy");
    }
    catch
    {
        return data; // se der erro, retorna a data como veio
    }}

    public string ToStringFormatado()
    {
        return $"{show_id};{type};{title};{director};{cast};{lengthry};{date_added};{release_year};{rating};{duration};{listed_in};{description}";
    }
}