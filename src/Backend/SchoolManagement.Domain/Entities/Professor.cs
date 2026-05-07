using SchoolManagement.Domain.Enums;
using SchoolManagement.Exceptions.ExceptionsBase;


 

namespace SchoolManagement.Domain.Entities
{
	public class Professor : Pessoa
	{

		private const int MIN_CARGA_HORARIA_SEMANAL = 10;
		private const int MAX_CARGA_HORARIA_SEMANAL = 40;
		private const int MIN_VALOR_HORA = 30;
		private const int MAX_VALOR_HORA = 150;
		private const int MAX_DISCIPLINAS = 3;
		private const int MAX_TURMAS = 5;



		public int RegistroFuncional { get; private set; }

		public int CargaHorariaSemanal { get; set; }

		public decimal ValorHora { get; set; }
		public decimal Salario => CargaHorariaSemanal * 5 * ValorHora;

		public TitulacaoProfessor Titulacao { get; private set; }
		public ICollection<Disciplina> Disciplinas { get; private set; } = [];
		public ICollection<Turma> Turmas { get; private set; } = [];

		public Professor(
			string nome, 
			string cpf, 
			string sexo, 
			string fone, 
			string email, 
			string dataNascimento, 
			int cargaHorariaSemanal, 
			decimal valorHora, 
		TitulacaoProfessor titulacao
		) : base(nome, cpf, sexo, fone, email, dataNascimento)
		{
			SetCargaHorariaSemanal(cargaHorariaSemanal);
			SetValorHora(valorHora);
			Titulacao = titulacao;
			Role = Role.Professor;
		}

		protected Professor() { }

		public void AdicionarDisciplina(Disciplina disciplina)
		{
			ArgumentNullException.ThrowIfNull(disciplina);
			if (Disciplinas.Count >= MAX_DISCIPLINAS) 
				throw new DomainException("Limite de disciplinas atingido");
			if (Disciplinas.Any(d => d.Id == disciplina.Id)) 
				throw new DomainException("Disciplina já adicionada");
			Disciplinas.Add(disciplina);
		}

		public void AdicionarTurma(Turma turma)
		{
			ArgumentNullException.ThrowIfNull(turma);
			if (Turmas.Count >= MAX_TURMAS) 
				throw new ArgumentException("Limite de turmas atingido");
			if (Turmas.Any(d => d.Id == turma.Id)) 
				throw new DomainException("Turma já adicionada");
			Turmas.Add(turma);
		}


		public void SetCargaHorariaSemanal(int valor) {
			if (valor < MIN_CARGA_HORARIA_SEMANAL || valor > MAX_CARGA_HORARIA_SEMANAL) 
				throw new DomainException($"Carga horária semanal deve estar entre {MIN_CARGA_HORARIA_SEMANAL} e {MAX_CARGA_HORARIA_SEMANAL}.");
			CargaHorariaSemanal = valor;
		}

		public void SetValorHora(decimal valor)
		{
			if (valor < MIN_VALOR_HORA || valor > MAX_VALOR_HORA) throw new DomainException($"O valor da hora deve estar entre {MIN_VALOR_HORA} e {MAX_VALOR_HORA}");
			ValorHora = valor;
		}




	}
}

