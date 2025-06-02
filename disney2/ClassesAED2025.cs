using System;
using System.Collections;

namespace AED
{
    #region Classe CCelula - representa a célula utilizada pelas classes CLista, CFila e CPilha
    /// <summary>
    /// Classe utilizada pelas classes CLista, CFila e CPilha
    /// </summary>
	public class CCelula<T>
	{
		public T Item;
		public CCelula<T> Prox;

		public CCelula()
		{
			Item = default(T);
			Prox = null;
		}

		public CCelula(T valorItem)
		{
			Item = valorItem;
			Prox = null;
		}

		public CCelula(T valorItem, CCelula<T> proxCelula)
		{
			Item = valorItem;
			Prox = proxCelula;
		}
	}
    #endregion

    #region Classe CFila - Fila (ou lista FIFO: first-in first-out)
	public class CFila<T> : IEnumerable<T>
	{
		private CCelula<T> Frente;
		private CCelula<T> Tras;
		private int Qtde = 0;

		public CFila()
		{
			Frente = new CCelula<T>();
			Tras = Frente;
		}

		public bool EstaVazia() => Frente == Tras;

		public void Enfileira(T valorItem)
		{
			Tras.Prox = new CCelula<T>(valorItem);
			Tras = Tras.Prox;
			Qtde++;
		}

		public T Desenfileira()
		{
			if (Frente != Tras)
			{
				Frente = Frente.Prox;
				T item = Frente.Item;
				Qtde--;
				return item;
			}
			return default(T);
		}

		public T Peek() => Frente != Tras ? Frente.Prox.Item : default(T);

		public bool Contem(T valorItem)
		{
			for (CCelula<T> aux = Frente.Prox; aux != null; aux = aux.Prox)
				if (EqualityComparer<T>.Default.Equals(aux.Item, valorItem))
					return true;
			return false;
		}

		public int Quantidade() => Qtde;

		public IEnumerator<T> GetEnumerator()
		{
			for (var aux = Frente.Prox; aux != null; aux = aux.Prox)
				yield return aux.Item;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
    #endregion

    #region Classe CPilha - CPilha (ou lista LIFO: last-in first-out)
	public class CPilha<T> : IEnumerable<T>
	{
		private CCelula<T> Topo = null;
		private int Qtde = 0;

		public bool EstaVazia() => Topo == null;

		public void Empilha(T valorItem)
		{
			Topo = new CCelula<T>(valorItem, Topo);
			Qtde++;
		}

		public T Desempilha()
		{
			if (Topo != null)
			{
				T item = Topo.Item;
				Topo = Topo.Prox;
				Qtde--;
				return item;
			}
			return default(T);
		}

		public T Peek() => Topo != null ? Topo.Item : default(T);

		public bool Contem(T valorItem)
		{
			for (var aux = Topo; aux != null; aux = aux.Prox)
				if (EqualityComparer<T>.Default.Equals(aux.Item, valorItem))
					return true;
			return false;
		}

		public int Quantidade() => Qtde;

		public IEnumerator<T> GetEnumerator()
		{
			for (var aux = Topo; aux != null; aux = aux.Prox)
				yield return aux.Item;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
    #endregion

    #region Classe CLista - Lista encadeada (simples) com célula cabeça
	public class CLista<T> : IEnumerable<T>
	{
		private CCelula<T> Primeira;
		private CCelula<T> Ultima;
		private int Qtde = 0;

		public CLista()
		{
			Primeira = new CCelula<T>();
			Ultima = Primeira;
		}

		public bool Vazia() => Primeira == Ultima;

		public void InsereFim(T valorItem)
		{
			Ultima.Prox = new CCelula<T>(valorItem);
			Ultima = Ultima.Prox;
			Qtde++;
		}

		public void InsereComeco(T valorItem)
		{
			Primeira.Prox = new CCelula<T>(valorItem, Primeira.Prox);
			if (Primeira.Prox.Prox == null)
				Ultima = Primeira.Prox;
			Qtde++;
		}

		public bool InsereIndice(T valorItem, int posicao)
		{
			if (posicao < 1 || posicao > Qtde + 1) return false;

			CCelula<T> aux = Primeira;
			for (int i = 0; i < posicao - 1; i++)
				aux = aux.Prox;

			aux.Prox = new CCelula<T>(valorItem, aux.Prox);
			if (aux.Prox.Prox == null)
				Ultima = aux.Prox;
			Qtde++;
			return true;
		}

		public void InsereAntesDe(T valorItem, T elemento)
		{
			if (!Contem(elemento))
				throw new ArgumentException("Elemento não encontrado");

			CCelula<T> aux = Primeira;
			while (!EqualityComparer<T>.Default.Equals(aux.Prox.Item, elemento))
				aux = aux.Prox;

			aux.Prox = new CCelula<T>(valorItem, aux.Prox);
			Qtde++;
		}

		public void InsereDepoisDe(T valorItem, T elemento)
		{
			if (!Contem(elemento))
				throw new ArgumentException("Elemento não encontrado");

			CCelula<T> aux = Primeira;
			while (!EqualityComparer<T>.Default.Equals(aux.Item, elemento))
				aux = aux.Prox;

			aux.Prox = new CCelula<T>(valorItem, aux.Prox);
			Qtde++;
		}
		public bool Contem(T valorItem)
		{
			for (var aux = Primeira.Prox; aux != null; aux = aux.Prox)
				if (EqualityComparer<T>.Default.Equals(aux.Item, valorItem))
					return true;
			return false;
		}

		public T RetornaIndice(int posicao)
		{
			if (posicao < 1 || posicao > Qtde) return default(T);

			var aux = Primeira.Prox;
			for (int i = 1; i < posicao; i++)
				aux = aux.Prox;
			return aux.Item;
		}

		public T RetornaPrimeiro() => Primeira != Ultima ? Primeira.Prox.Item : default(T);

		public T RetornaUltimo() => Primeira != Ultima ? Ultima.Item : default(T);

		public T RemoveRetornaComecoSimples()
		{
			if (Primeira != Ultima)
			{
				Primeira = Primeira.Prox;
				Qtde--;
				return Primeira.Item;
			}
			return default(T);
		}

		public T RemoveRetornaFim()
		{
			if (Primeira == Ultima) return default(T);

			var aux = Primeira;
			while (aux.Prox != Ultima)
				aux = aux.Prox;

			var removida = Ultima.Item;
			Ultima = aux;
			Ultima.Prox = null;
			Qtde--;
			return removida;
		}

		public void Remove(T valorItem)
		{
			var aux = Primeira;
			while (aux.Prox != null)
			{
				if (EqualityComparer<T>.Default.Equals(aux.Prox.Item, valorItem))
				{
					aux.Prox = aux.Prox.Prox;
					if (aux.Prox == null)
						Ultima = aux;
					Qtde--;
					return;
				}
				aux = aux.Prox;
			}
		}

		public int Quantidade() => Qtde;

		public IEnumerator<T> GetEnumerator()
		{
			for (var aux = Primeira.Prox; aux != null; aux = aux.Prox)
				yield return aux.Item;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

    #endregion

    #region Classe CCelulaDup<T> - representa a célula utilizada pela classe CListaDup<T>
    /// <summary>
    /// Classe utilizada pela classe CListaDup<T>
    /// </summary>
    public class CCelulaDup<T>
    {
        public T Item; // O Item armazenado pela célula
        public CCelulaDup<T> Ant; // Referencia a célula anterior
        public CCelulaDup<T> Prox; // Referencia a próxima célula

        public CCelulaDup()
        {
            Item = default(T);
            Ant = null;
            Prox = null;
        }

        public CCelulaDup(T valorItem)
        {
            Item = valorItem;
            Ant = null;
            Prox = null;
        }

        public CCelulaDup(T valorItem, CCelulaDup<T> celulaAnt, CCelulaDup<T> proxCelula)
        {
            Item = valorItem;
            Ant = celulaAnt;
            Prox = proxCelula;
        }
    }
    #endregion

    #region Classe CListaDup<T> - Lista duplamente encadeada com célula cabeça
    /// <summary>
    /// Implementa uma lista duplamente encadeada genérica.
    /// </summary>
    public class CListaDup<T> : IEnumerable<T>
    {
        private CCelulaDup<T> Primeira; // Referencia a primeira célula da lista (célula cabeça)
        private CCelulaDup<T> Ultima; // Referencia a última célula da lista 
        private int Qtde = 0;

        public CListaDup()
        {
            Primeira = new CCelulaDup<T>();
            Ultima = Primeira;
        }

        public bool Vazia()
        {
            return Primeira == Ultima;
        }

        public void InsereFim(T valorItem)
        {
            Ultima.Prox = new CCelulaDup<T>(valorItem, Ultima, null);
            Ultima = Ultima.Prox;
            Qtde++;
        }

        public void InsereComeco(T valorItem)
        {
            if (Primeira == Ultima) // Se a lista estiver vazia insere no fim
            {
                Ultima.Prox = new CCelulaDup<T>(valorItem, Ultima, null);
                Ultima = Ultima.Prox;
            }
            else // senão insere no começo
            {
                Primeira.Prox = new CCelulaDup<T>(valorItem, Primeira, Primeira.Prox);
                Primeira.Prox.Prox.Ant = Primeira.Prox;
            }
            Qtde++;
        }

        public void RemoveComecoSemRetorno()
        {
            if (Primeira != Ultima)
            {
                Primeira = Primeira.Prox;
                Primeira.Ant = null;
                Qtde--;
            }
        }

        public void Imprime()
        {
            CCelulaDup<T> aux = Primeira.Prox;
            while (aux != null)
            {
                Console.WriteLine(aux.Item);
                aux = aux.Prox;
            }
        }

        public void ImprimeFor()
        {
            for (CCelulaDup<T> aux = Primeira.Prox; aux != null; aux = aux.Prox)
                Console.WriteLine(aux.Item);
        }

        public void ImprimeInv()
        {
            CCelulaDup<T> aux = Ultima;
            while (aux.Ant != null)
            {
                Console.WriteLine(aux.Item);
                aux = aux.Ant;
            }
        }

        public void ImprimeInvFor()
        {
            for (CCelulaDup<T> aux = Ultima; aux.Ant != null; aux = aux.Ant)
                Console.WriteLine(aux.Item);
        }

        public bool Contem(T elemento)
        {
            bool achou = false;
            CCelulaDup<T> aux = Primeira.Prox;
            while (aux != null && !achou)
            {
                achou = EqualityComparer<T>.Default.Equals(aux.Item, elemento);
                aux = aux.Prox;
            }
            return achou;
        }

        public bool ContemFor(T elemento)
        {
            bool achou = false;
            for (CCelulaDup<T> aux = Primeira.Prox; aux != null && !achou; aux = aux.Prox)
                achou = EqualityComparer<T>.Default.Equals(aux.Item, elemento);
            return achou;
        }

        public T RetornaPrimeiro()
        {
            if (Primeira != Ultima)
                return Primeira.Prox.Item;
            return default(T);
        }

        public T RetornaIndice(int Posicao)
        {
            if ((Posicao >= 1) && (Posicao <= Qtde) && (Primeira != Ultima))
            {
                CCelulaDup<T> aux = Primeira.Prox;
                for (int i = 1; i < Posicao; i++, aux = aux.Prox) ;
                if (aux != null)
                    return aux.Item;
            }
            return default(T);
        }

        public T RetornaUltimo()
        {
            if (Primeira != Ultima)
                return Ultima.Item;
            return default(T);
        }

        public void RemoveFimSemRetorno()
        {
            if (Primeira != Ultima)
            {
                Ultima = Ultima.Ant;
                Ultima.Prox = null;
                Qtde--;
            }
        }

        public void Remove(T valorItem)
        {
            if (Primeira != Ultima)
            {
                CCelulaDup<T> aux = Primeira.Prox;
                bool achou = false;
                while (aux != null && !achou)
                {
                    achou = EqualityComparer<T>.Default.Equals(aux.Item, valorItem);
                    if (!achou)
                        aux = aux.Prox;
                }
                if (achou) // achou o elemento
                {
                    CCelulaDup<T> anterior = aux.Ant;
                    CCelulaDup<T> proximo = aux.Prox;
                    anterior.Prox = proximo;
                    if (proximo != null)
                        proximo.Ant = anterior;
                    else
                        Ultima = anterior;
                    Qtde--;
                }
            }
        }

        public T RemoveRetornaComeco()
        {
            if (Primeira != Ultima)
            {
                CCelulaDup<T> aux = Primeira.Prox;
                Primeira = Primeira.Prox;
                Primeira.Ant = null;
                Qtde--;
                return aux.Item;
            }
            else
                return default(T);
        }

        public T RemoveRetornaFim()
        {
            if (Primeira != Ultima)
            {
                CCelulaDup<T> aux = Ultima;
                Ultima = Ultima.Ant;
                Ultima.Prox = null;
                Qtde--;
                return aux.Item;
            }
            else
                return default(T);
        }

        public int Quantidade()
        {
            return Qtde;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (CCelulaDup<T> aux = Primeira.Prox; aux != null; aux = aux.Prox)
                yield return aux.Item;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> Reverse
        {
            get
            {
                for (CCelulaDup<T> aux = Ultima; aux != Primeira; aux = aux.Ant)
                    yield return aux.Item;
            }
        }
    }
    #endregion
}