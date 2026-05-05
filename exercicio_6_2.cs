using System;
using System.Collections.Generic;

// interface
interface IAvaliavel
{
    double CalcularNotaFinal();
}

// classe abstracta
abstract class Avaliacao : IAvaliavel
{
    public double Nota;

    public Avaliacao(double nota)
    {
        Nota = nota;
    }

    public abstract double CalcularNotaFinal();
}

// teste vale 30%
class Teste : Avaliacao
{
    public Teste(double nota) : base(nota) { }

    public override double CalcularNotaFinal()
    {
        return Nota * 0.30;
    }
}

// projecto vale 40%
class Projecto : Avaliacao
{
    public Projecto(double nota) : base(nota) { }

    public override double CalcularNotaFinal()
    {
        return Nota * 0.40;
    }
}

// exame final vale 30%
class ExameFinal : Avaliacao
{
    public ExameFinal(double nota) : base(nota) { }

    public override double CalcularNotaFinal()
    {
        return Nota * 0.30;
    }
}

// classe base
class Pessoa
{
    public string Nome;
    public string Numero;

    public Pessoa(string nome, string numero)
    {
        Nome = nome;
        Numero = numero;
    }
}

// docente herda de pessoa
class Docente : Pessoa
{
    public string Departamento;
    public string Tipo; // Titular ou Assistente

    public Docente(string nome, string numero, string dep, string tipo) : base(nome, numero)
    {
        Departamento = dep;
        Tipo = tipo;
    }
}

class Titular : Docente
{
    public Titular(string nome, string numero, string dep) : base(nome, numero, dep, "Titular") { }
}

class Assistente : Docente
{
    public Assistente(string nome, string numero, string dep) : base(nome, numero, dep, "Assistente") { }
}

// estudante herda de pessoa
class Estudante : Pessoa
{
    public string Curso;
    public List<Avaliacao> Avaliacoes = new List<Avaliacao>();

    public Estudante(string nome, string numero, string curso) : base(nome, numero)
    {
        Curso = curso;
    }

    public double CalcularNotaFinal()
    {
        double total = 0;
        foreach (Avaliacao a in Avaliacoes)
        {
            total = total + a.CalcularNotaFinal();
        }
        return total;
    }
}

// unidade curricular
class UnidadeCurricular
{
    public string Nome;
    public string Codigo;
    public Docente Docente;
    public List<Estudante> Estudantes = new List<Estudante>();

    public UnidadeCurricular(string nome, string codigo, Docente docente)
    {
        Nome = nome;
        Codigo = codigo;
        Docente = docente;
    }

    public void AdicionarEstudante(Estudante e)
    {
        Estudantes.Add(e);
    }

    public void EmitirPauta()
    {
        if (Estudantes.Count == 0)
        {
            Console.WriteLine("Nao ha estudantes inscritos nesta UC!");
            return;
        }

        // ordenar por nota (bubble sort)
        for (int i = 0; i < Estudantes.Count - 1; i++)
        {
            for (int j = 0; j < Estudantes.Count - 1 - i; j++)
            {
                if (Estudantes[j].CalcularNotaFinal() < Estudantes[j + 1].CalcularNotaFinal())
                {
                    Estudante temp = Estudantes[j];
                    Estudantes[j] = Estudantes[j + 1];
                    Estudantes[j + 1] = temp;
                }
            }
        }

        
        Console.WriteLine("PAUTA - " + Nome + " (" + Codigo + ")");
        Console.WriteLine("Docente: " + Docente.Nome + " (" + Docente.Tipo + ")");
        

        int pos = 1;
        foreach (Estudante e in Estudantes)
        {
            double nota = e.CalcularNotaFinal();
            string resultado = nota >= 10 ? "Aprovado" : "Reprovado";
            Console.WriteLine(pos + ". " + e.Nome + " (" + e.Numero + ") - " + nota.ToString("F1") + " - " + resultado);
            pos++;
        }

       
    }
}

class Program
{
    static List<Docente> docentes = new List<Docente>();
    static List<Estudante> estudantes = new List<Estudante>();
    static List<UnidadeCurricular> ucs = new List<UnidadeCurricular>();

    static void Main()
    {
        int opcao = 0;

        while (opcao != 6)
        {
            
            Console.WriteLine("     SISTEMA UNIVERSITARIO");
           
            Console.WriteLine("1. Registar docente");
            Console.WriteLine("2. Registar estudante");
            Console.WriteLine("3. Criar unidade curricular");
            Console.WriteLine("4. Inscrever estudante numa UC e lancar notas");
            Console.WriteLine("5. Emitir pauta de uma UC");
            Console.WriteLine("6. Sair");
            
            Console.Write("Escolha uma opcao: ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                RegistarDocente();
            }
            else if (opcao == 2)
            {
                RegistarEstudante();
            }
            else if (opcao == 3)
            {
                CriarUC();
            }
            else if (opcao == 4)
            {
                InscricaoENotas();
            }
            else if (opcao == 5)
            {
                EmitirPauta();
            }
            else if (opcao == 6)
            {
                Console.WriteLine("A sair...");
            }
            else
            {
                Console.WriteLine("Opcao invalida!");
            }
        }
    }

    static void RegistarDocente()
    {
        Console.WriteLine("\n--- Registar docente ---");

        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Numero: ");
        string numero = Console.ReadLine();

        Console.Write("Departamento: ");
        string dep = Console.ReadLine();

        Console.WriteLine("Tipo: 1 - Titular  2 - Assistente");
        Console.Write("Escolha: ");
        int tipo = int.Parse(Console.ReadLine());

        if (tipo == 1)
        {
            docentes.Add(new Titular(nome, numero, dep));
            Console.WriteLine("Docente Titular registado!");
        }
        else
        {
            docentes.Add(new Assistente(nome, numero, dep));
            Console.WriteLine("Docente Assistente registado!");
        }
    }

    static void RegistarEstudante()
    {
        Console.WriteLine("\n--- Registar estudante ---");

        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Numero: ");
        string numero = Console.ReadLine();

        Console.Write("Curso: ");
        string curso = Console.ReadLine();

        estudantes.Add(new Estudante(nome, numero, curso));
        Console.WriteLine("Estudante registado!");
    }

    static void CriarUC()
    {
        if (docentes.Count == 0)
        {
            Console.WriteLine("Nao ha docentes registados! Registe um docente primeiro.");
            return;
        }

        Console.WriteLine("\n--- Criar unidade curricular ---");

        Console.Write("Nome da UC: ");
        string nome = Console.ReadLine();

        Console.Write("Codigo da UC: ");
        string codigo = Console.ReadLine();

        Console.WriteLine("Docentes disponiveis:");
        for (int i = 0; i < docentes.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + docentes[i].Nome + " (" + docentes[i].Tipo + ")");
        }

        Console.Write("Escolha o numero do docente: ");
        int num = int.Parse(Console.ReadLine());

        if (num < 1 || num > docentes.Count)
        {
            Console.WriteLine("Numero invalido!");
            return;
        }

        ucs.Add(new UnidadeCurricular(nome, codigo, docentes[num - 1]));
        Console.WriteLine("Unidade Curricular criada!");
    }

    static void InscricaoENotas()
    {
        if (ucs.Count == 0)
        {
            Console.WriteLine("Nao ha UCs criadas!");
            return;
        }
        if (estudantes.Count == 0)
        {
            Console.WriteLine("Nao ha estudantes registados!");
            return;
        }

        Console.WriteLine("\n--- Inscrever estudante e lancar notas ---");

        Console.WriteLine("UCs disponiveis:");
        for (int i = 0; i < ucs.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + ucs[i].Nome + " (" + ucs[i].Codigo + ")");
        }

        Console.Write("Escolha o numero da UC: ");
        int numUC = int.Parse(Console.ReadLine());

        if (numUC < 1 || numUC > ucs.Count)
        {
            Console.WriteLine("Numero invalido!");
            return;
        }

        UnidadeCurricular uc = ucs[numUC - 1];

        Console.WriteLine("Estudantes disponiveis:");
        for (int i = 0; i < estudantes.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + estudantes[i].Nome + " (" + estudantes[i].Numero + ")");
        }

        Console.Write("Escolha o numero do estudante: ");
        int numEst = int.Parse(Console.ReadLine());

        if (numEst < 1 || numEst > estudantes.Count)
        {
            Console.WriteLine("Numero invalido!");
            return;
        }

        Estudante est = estudantes[numEst - 1];

        Console.WriteLine("\nLancar notas para " + est.Nome + ":");

        Console.Write("Nota do Teste (0-20): ");
        double notaTeste = double.Parse(Console.ReadLine());

        Console.Write("Nota do Projecto (0-20): ");
        double notaProjecto = double.Parse(Console.ReadLine());

        Console.Write("Nota do Exame Final (0-20): ");
        double notaExame = double.Parse(Console.ReadLine());

        // limpar avaliacoes anteriores desta UC para este estudante
        est.Avaliacoes.Clear();
        est.Avaliacoes.Add(new Teste(notaTeste));
        est.Avaliacoes.Add(new Projecto(notaProjecto));
        est.Avaliacoes.Add(new ExameFinal(notaExame));

        // verificar se ja esta inscrito
        bool jaInscrito = false;
        foreach (Estudante e in uc.Estudantes)
        {
            if (e.Numero == est.Numero)
            {
                jaInscrito = true;
            }
        }

        if (!jaInscrito)
        {
            uc.AdicionarEstudante(est);
        }

        Console.WriteLine("Notas lancadas! Nota final: " + est.CalcularNotaFinal().ToString("F1"));
    }

    static void EmitirPauta()
    {
        if (ucs.Count == 0)
        {
            Console.WriteLine("Nao ha UCs criadas!");
            return;
        }

        Console.WriteLine("\n--- Emitir pauta ---");
        Console.WriteLine("UCs disponiveis:");

        for (int i = 0; i < ucs.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + ucs[i].Nome + " (" + ucs[i].Codigo + ")");
        }

        Console.Write("Escolha o numero da UC: ");
        int num = int.Parse(Console.ReadLine());

        if (num < 1 || num > ucs.Count)
        {
            Console.WriteLine("Numero invalido!");
            return;
        }

        ucs[num - 1].EmitirPauta();
    }
}