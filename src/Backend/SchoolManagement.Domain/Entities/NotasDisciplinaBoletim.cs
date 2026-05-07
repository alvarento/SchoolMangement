using SchoolManagement.Domain.Enums;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.Entities
{
	public class NotasDisciplinaBoletim
	{

		private const int MIN_NOTA = 0;
		private const int MAX_NOTA = 10;
		private const int MIN_MEDIA_APROVACAO = 7;
		private const int MIN_MEDIA_RECUPERACAO = 4;

		public int Id { get; private set; }
		public int DisciplinaId { get; set; }
		public Disciplina? Disciplina { get; set; }
		public int BoletimId { get; private set; }
		public Boletim Boletim { get; private set; }
		public double? Nota1 { get; set; }
		public double? Nota2 { get; set; }
		public double? Nota3 { get; set; }
		public double? Nota4 { get; set; }
		public SituacaoAluno SituacaoAlunoDisciplina { get; set; }

		public NotasDisciplinaBoletim(Boletim boletim, Disciplina disciplina)
		{
			Disciplina = disciplina ?? throw new DomainException(nameof(disciplina));
			DisciplinaId = disciplina.Id;
			SituacaoAlunoDisciplina = SituacaoAluno.NA;
			Boletim = boletim;
			BoletimId = boletim.Id;
		}

		protected NotasDisciplinaBoletim() { }

		public double? Media
		{
			get
			{
				var notas = new[] { Nota1, Nota2, Nota3, Nota4 }
					.Where(n => n.HasValue)
					.Select(n => n!.Value)
					.ToList();

				if (notas.Count == 0) return null;

				return notas.Average();
			}
		}


		public void LancarNota(int bimestre, double nota)
		{
			if (nota < MIN_NOTA || nota > MAX_NOTA) throw new DomainException($"Nota deve ser entre {MIN_NOTA} e {MAX_NOTA}");

			switch (bimestre)
			{
				case 1: Nota1 = nota; break;
				case 2: Nota2 = nota; break;
				case 3: Nota3 = nota; break;
				case 4: Nota4 = nota; break;
				default: throw new DomainException("Bimestre inválido");
			}

			AtualizarSituacao();
		}


		private void AtualizarSituacao()
		{
			var media = Media;

			if (!media.HasValue)
			{
				SituacaoAlunoDisciplina = SituacaoAluno.NA;
				return;
			}

			SituacaoAlunoDisciplina =
				media >= MIN_MEDIA_APROVACAO ? SituacaoAluno.Aprovado :
				media >= MIN_MEDIA_RECUPERACAO ? SituacaoAluno.EmRecuperacao :
				SituacaoAluno.Reprovado;
		}

	}
}
