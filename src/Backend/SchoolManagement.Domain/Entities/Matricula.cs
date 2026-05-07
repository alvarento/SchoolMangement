using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities
{
	public class Matricula
	{
		public int Id { get; private set; }

		public int AlunoId { get; private set; }
		public Aluno Aluno { get; private set; }

		public int TurmaId { get; private set; }
		public Turma Turma { get; private set; }

		public Turno Turno { get; set; }

		public StatusMatricula Status { get; private set; }

		public string? RegistroMatricula { get; private set; }
		public DateTime DataMatricula { get; private set; }

		public Boletim? Boletim { get; private set; }

		private Matricula(Aluno aluno, Turma turma)
		{
			Aluno = aluno;
			Turma = turma;
		}

		protected Matricula() { }


		public static Matricula MatricularAluno(Aluno aluno, Turma turma, Turno turno)
		{
			ArgumentNullException.ThrowIfNull(aluno);
			ArgumentNullException.ThrowIfNull(turma);


			Matricula matricula = new(aluno, turma)
			{
				DataMatricula = DateTime.UtcNow,
				Status = StatusMatricula.Ativa,
				Turno = turno,
				AlunoId = aluno.Id,
				TurmaId = turma.Id
			};


			matricula.Boletim = new Boletim(matricula);

			matricula.GerarRegistroMatricula();

			return matricula;
		}




		private string GerarRegistroMatricula()
		{
			int anoMatricula = DataMatricula.Year;
			return $"MATRI-${anoMatricula}-{Id}";
		}
	}





}

