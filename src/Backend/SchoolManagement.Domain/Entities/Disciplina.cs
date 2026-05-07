using SchoolManagement.Domain.ValueObjects;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.Entities
{
	public class Disciplina
	{


		private const int MIN_CARGA_HORARIA = 10;
		private const int MAX_CARGA_HORARIA = 150;


		public int Id { get; set; }
		public string Codigo { get; set; }
		public NomeDisciplina Nome { get; set; }
		public int CargaHoraria { get; set; }

		public Disciplina(string nomeDisciplina, int cargaHoraria)
		{
			Nome = NomeDisciplina.Criar(nomeDisciplina);
			DefinirCargaHoraria(cargaHoraria);
			Codigo = GerarCodigo();
		}

		protected Disciplina() { }


		public void DefinirCargaHoraria(int cargaHoraria)
		{
			if (cargaHoraria < MIN_CARGA_HORARIA && cargaHoraria > MAX_CARGA_HORARIA)
				throw new DomainException($"Carga horária deve ser entre {MIN_CARGA_HORARIA} e {MAX_CARGA_HORARIA}");

			CargaHoraria = cargaHoraria;
		}

		private string GerarCodigo()
		{
			string abrevNome = Nome.Valor.ToString()[..3].ToLower();
			return $"D{abrevNome}.${Id}";
		}
	}
}
