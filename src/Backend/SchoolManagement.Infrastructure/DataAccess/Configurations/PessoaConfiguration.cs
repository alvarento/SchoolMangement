using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.ValueObjects;

namespace SchoolManagement.Infrastructure.DataAccess.Configurations;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
	public virtual void Configure(EntityTypeBuilder<Pessoa> builder)
	{
		builder.ToTable("Pessoas");

		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id)
			.ValueGeneratedOnAdd();



		builder.OwnsOne(p => p.Nome, nome =>
		{
			nome.Property(n => n.Valor)
				.HasColumnName("Nome")
				.IsRequired()
				.HasMaxLength(150);
		});


		builder.OwnsOne(p => p.Cpf, cpf =>
		{
			cpf.Property(c => c.Valor)
				.HasColumnName("Cpf")
				.IsRequired()
				.HasMaxLength(11);

			cpf.HasIndex(c => c.Valor).IsUnique();
		});


		builder.OwnsOne(p => p.Email, email =>
		{
			email.Property(e => e.Valor)
				.HasColumnName("Email")
				.IsRequired()
				.HasMaxLength(100);

			email.HasIndex(e => e.Valor).IsUnique();
		});


		builder.OwnsOne(p => p.Telefone, telefone =>
		{
			telefone.Property(t => t.Valor)
				.HasColumnName("Telefone")
				.HasMaxLength(20);
		});

		builder.OwnsOne(p => p.DataNascimento, dataNascimento =>
		{
			dataNascimento.Property(d => d.Valor)
				.HasColumnName("DataNascimento")
				.HasColumnType("date")
				.IsRequired();
		});


		builder.Property(p => p.Sexo)
			.HasColumnName("Sexo")
			.HasColumnType("char(1)")
			.IsRequired();


		builder.Property(p => p.Role)
			.HasConversion<string>()
			.IsRequired();

		builder.Ignore(p => p.Idade);
	}
}