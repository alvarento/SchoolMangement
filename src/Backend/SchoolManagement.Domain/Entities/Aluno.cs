using System.Runtime.InteropServices;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities
{
	public class Aluno : Pessoa

	{

		public string? Matricula { get; private set; }

		public DateTime? DataMatricula { get; private set; }

		public Turno? Turno { get; private set; }
		public Boletim? Boletim { get; private set; }
		public StatusAluno StatusAluno { get; private set; }

		public SituacaoAluno SituacaoAluno { get; private set; }

		public double? MediaFinal { get; private set; }
		public Aluno(
			string nome,
			string cpf,
			string sexo,
			string fone,
			string email,
			string dataNascimento) : base(nome, cpf, sexo, fone, email, dataNascimento)
		{
			StatusAluno = StatusAluno.PreCadastrado;
			SituacaoAluno = SituacaoAluno.NA;
			Role = Role.Aluno;
		}

		protected Aluno() { }


		//public static Aluno Create(
		//	string nome,
		//	string cpf,
		//	string sexo,
		//	string fone,
		//	string email,
		//	string dataNascimento)
		//{
		//	return new Aluno(
				

		//	);
		//}





	}
}
