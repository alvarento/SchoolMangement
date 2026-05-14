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
	}
}