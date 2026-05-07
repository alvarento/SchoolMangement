using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.ValueObjects;

namespace SchoolManagement.Infrastructure.DataAccess.Configurations;

public class DisciplinaConfiguration : IEntityTypeConfiguration<Disciplina>
{
	public void Configure(EntityTypeBuilder<Disciplina> builder)
	{
		builder.ToTable("Disciplinas");

		builder.HasKey(d => d.Id);

		builder.Property(d => d.Nome)
			.HasConversion(nome => nome.Valor, value => NomeDisciplina.Criar(value))
			.HasMaxLength(100)
			.IsRequired();

		builder.HasIndex(d => d.Nome)
			.IsUnique();
	}
}