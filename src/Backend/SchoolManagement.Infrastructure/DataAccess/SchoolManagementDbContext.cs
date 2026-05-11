using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.DataAcess
{
	public class SchoolManagementDbContext : DbContext
	{
		public SchoolManagementDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Pessoa> Pessoas { get; set; }
		public DbSet<Aluno> Alunos { get; set; }
		public DbSet<Professor> Professores { get; set; }
		public DbSet<Turma> Turmas { get; set; }
		public DbSet<Matricula> Matriculas { get; set; }

		public DbSet<Disciplina> Disciplinas { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolManagementDbContext).Assembly);
		}

	}

}
