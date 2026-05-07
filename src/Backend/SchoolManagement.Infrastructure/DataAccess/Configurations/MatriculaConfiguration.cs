using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.DataAccess.Configurations;

public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
	public void Configure(EntityTypeBuilder<Matricula> builder)
	{
		builder.ToTable("Matriculas");

		// 🔑 PK
		builder.HasKey(m => m.Id);
		builder.Property(m => m.Id)
			.ValueGeneratedOnAdd();

		// 📅 Data
		builder.Property(m => m.DataMatricula)
			.HasColumnType("datetime");

		// 📌 Status (Enum → string)
		builder.Property(m => m.Status)
			.HasConversion<string>()
			.IsRequired();

		// 🧾 Registro da matrícula
		builder.Property(m => m.RegistroMatricula)
			.HasMaxLength(50);

		builder.HasIndex(m => m.RegistroMatricula)
			.IsUnique();

		// 🔗 Relacionamento com Aluno
		builder.HasOne(m => m.Aluno)
			.WithMany() // ou .WithMany(a => a.Matriculas) se existir coleção
			.HasForeignKey(m => m.AlunoId)
			.OnDelete(DeleteBehavior.Restrict);

		// 🔗 Relacionamento com Turma
		builder.HasOne(m => m.Turma)
			.WithMany() // ou .WithMany(t => t.Matriculas)
			.HasForeignKey(m => m.TurmaId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.Property(a => a.Turno)
			.HasConversion<string>()
			.IsRequired();

		// 🔗 1:1 com Boletim
		builder.HasOne(m => m.Boletim)
			.WithOne(b => b.Matricula)
			.HasForeignKey<Boletim>(b => b.MatriculaId)
			.OnDelete(DeleteBehavior.Cascade);

		// 📌 Índice composto (evita aluno duplicado na mesma turma)
		builder.HasIndex(m => new { m.AlunoId, m.TurmaId })
			.IsUnique();
	}
}