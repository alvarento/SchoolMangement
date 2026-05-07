using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.Entities
{
	public class Boletim
    {

        public int Id { get; }

		public int MatriculaId { get; }
		public Matricula? Matricula { get; }

		public Aluno? Aluno { get; }
		public int AlunoId { get;  }

        private readonly List<NotasDisciplinaBoletim> _disciplinasNotas = [];
        public IReadOnlyCollection<NotasDisciplinaBoletim> DisciplinasNotas => _disciplinasNotas;

        public Boletim(Matricula matricula)
        {
            Matricula = matricula ?? throw new DomainException(nameof(matricula));
            MatriculaId = matricula.Id;
			Aluno = matricula.Aluno;
			AlunoId = matricula.AlunoId;
        }

		protected Boletim() { }


		public void AdicionarDisciplina(Disciplina disciplina)
		{
			if (_disciplinasNotas.Any(d => d.Id == disciplina.Id))
				throw new DomainException("Disciplina já adicionada");

			_disciplinasNotas.Add(new NotasDisciplinaBoletim(this, disciplina));
		}

		public void LancarNota(int disciplinaId, int unidade, double nota)
		{
			var disciplina = _disciplinasNotas
				.FirstOrDefault(d => d.DisciplinaId == disciplinaId)
				?? throw new DomainException("Disciplina não encontrada no boletim");

			disciplina.LancarNota(unidade, nota);
		}


		public double? MediaFinal()
		{
			var medias = _disciplinasNotas
				.Select(d => d.Media)
				.Where(m => m.HasValue)
				.Select(m => m!.Value)
				.ToList();

			if (medias.Count == 0)
				return null;

			return medias.Average();
		}

	}
}



