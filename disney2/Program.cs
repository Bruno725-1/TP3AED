using System;
using AED;
using System.Globalization;
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
        CFila<Producao> fila = new CFila<Producao>();
        CPilha<Producao> pilha = new CPilha<Producao>();
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
                            pilha.Empilha(producoes[j]);
                    }
                }
                else
                {
                    for (int j = 0; j < producoes.Length; j++)
                    {
                        if (producoes[j].type == comandos[1])
                            pilha.Empilha(producoes[j]);
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
                            fila.Enfileira(producoes[j]);
                    }
                }
                else
                {
                    for (int j = 0; j < producoes.Length; j++)
                    {
                        if (producoes[j].type == comandos[1])
                            fila.Enfileira(producoes[j]);
                    }
                }
            }
            else if (comando == "pop")
            {
                if (int.TryParse(comandos[1], out int parametro))
                {
                    for (int j = 0; j < parametro; j++)
                    {
                        Producao p = pilha.Desempilha();
                        Console.WriteLine(p.ToStringFormatado());
                    }
                }
                else
                {
                    while (pilha.Quantidade() > 0)
                    {
                        Producao p = pilha.Desempilha();
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
                        Producao p = fila.Desenfileira();
                        Console.WriteLine(p.ToStringFormatado());
                    }
                }
                else
                {
                    while (fila.Quantidade() > 0)
                    {
                        Producao p = fila.Desenfileira();
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
        this.show_id = producao[0];
        this.type = producao[1];
        this.title = producao[2];
        this.director = producao[3];
        this.cast = producao[4];
        this.lengthry = producao[5];
        this.date_added = producao[6];
        this.release_year = int.Parse(producao[7]);
        this.rating = producao[8];
        this.duration = producao[9];
        this.listed_in = producao[10];
        this.description = producao[11];
    }
    public static string ChangeData(string data)
    {
        DateTime dt;
        // Tenta parsear como "MMMM d, yyyy" (ex.: September 24, 2021)
        if (DateTime.TryParseExact(data,
            "MMMM d, yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out dt))
        {
            return dt.ToString("dd/MM/yyyy");
        }
        // Tenta parsear como "dd/MM/yyyy"
        else if (DateTime.TryParseExact(data,
            "dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out dt))
        {
            return dt.ToString("dd/MM/yyyy");
        }
        else
        {
            // Se não conseguir parsear, retorna como está
            return data;
        }
    }
    public string ToStringFormatado()
    {
        return $"{show_id};{type};{title};{director};{cast};{lengthry};{ChangeData(date_added)};{release_year};{rating};{duration};{listed_in};{description}";
    }
}
