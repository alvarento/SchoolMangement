using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
	public void Configure(EntityTypeBuilder<Aluno> builder)
	{
		builder.ToTable("Alunos");

		builder.HasBaseType<Pessoa>();


		builder.Property(a => a.Matricula)
			.HasMaxLength(50);

		builder.Property(a => a.DataMatricula)
			.HasColumnType("date");

		builder.Property(a => a.Turno)
			.HasConversion<string>();

		builder.Property(a => a.StatusAluno)
			.HasConversion<string>()
			.IsRequired();

		builder.Property(a => a.SituacaoAluno)
			.HasConversion<string>();

		builder.Property(a => a.MediaFinal);

		builder.HasOne(a => a.Boletim)
			.WithOne()
			.HasForeignKey<Aluno>("BoletimId")
			.OnDelete(DeleteBehavior.Cascade);
	}
}