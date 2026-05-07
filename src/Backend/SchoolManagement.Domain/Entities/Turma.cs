using SchoolManagement.Domain.Enums;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.Entities
{
	public class Turma
    {

		private const int MAX_ALUNOS_TURMA = 40;
		public int Id { get; set; }
		public string Codigo { get; set; }

		public int AnoTurma { get; set; } = DateTime.UtcNow.Year;

		public Turno Turno { get; set; }

		private readonly List<Aluno> _alunos = [];

        public IReadOnlyList<Aluno> Alunos => _alunos;


		public Turma(Turno turnoTurma)
		{
			Codigo = GerarCodigoTurma();
			Turno = turnoTurma;
		}

		protected Turma() { }

   

        public void AdicionarAluno(Aluno aluno)
        {
            if (_alunos.Count >= MAX_ALUNOS_TURMA) throw new DomainException("A Turma já está cheia.");
            if (_alunos.Any(a => a.Id == aluno.Id)) throw new DomainException("Aluno já está matriculado nesta turma");
            _alunos.Add(aluno);
        }


		public void TransferirAluno(Aluno aluno, Turma turmaNova)
		{
			_alunos.RemoveAll(a => a.Id == aluno.Id);
			turmaNova.AdicionarAluno(aluno);

		}

		public int TotalDeAlunos()
		{
			return _alunos.Count;
		}

		private string GerarCodigoTurma()
		{
			string turnoTurma = Turno.ToString()[..3].ToLower();
			return $"T${turnoTurma}{AnoTurma}.{Id}";
		}


	}
}
