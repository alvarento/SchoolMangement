using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.DataAccess.Configurations;

public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
{
	public void Configure(EntityTypeBuilder<Professor> builder)
	{


		builder.ToTable("Professores");


		builder.HasBaseType<Pessoa>();


		builder.Property(p => p.CargaHorariaSemanal)
			.IsRequired();


		builder.Property(p => p.ValorHora)
			.HasColumnType("decimal(10,2)")
			.IsRequired();


		builder.Property(p => p.Titulacao)
			.HasConversion<string>()
			.IsRequired();


		builder.Ignore(p => p.Salario);


        builder.HasMany(p => p.Disciplinas)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProfessorDisciplinas",
                j => j.HasOne<Disciplina>()
                      .WithMany()
                      .HasForeignKey("DisciplinaId"),
                j => j.HasOne<Professor>()
                      .WithMany()
                      .HasForeignKey("ProfessorId"),
                j =>
                {
                    j.HasKey("ProfessorId", "DisciplinaId");
                });


        builder.HasMany(p => p.Turmas)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProfessorTurmas",
                j => j.HasOne<Turma>()
                      .WithMany()
                      .HasForeignKey("TurmaId"),
                j => j.HasOne<Professor>()
                      .WithMany()
                      .HasForeignKey("ProfessorId"),
                j =>
                {
                    j.HasKey("ProfessorId", "TurmaId");
                });

    }
}