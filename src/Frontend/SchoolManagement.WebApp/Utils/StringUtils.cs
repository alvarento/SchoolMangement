namespace SchoolManagement.WebApp.Utils
{
	public static class StringUtils
	{
		
		public static string FormatarCpfGrid(string? cpf)
		{
			if (string.IsNullOrWhiteSpace(cpf)) return "";
			var apenasNumeros = SomenteNumeros(cpf);
			if (apenasNumeros.Length != 11) return apenasNumeros;
			return long.Parse(apenasNumeros).ToString(@"000\.000\.000\-00");
		}

		public static string FormatarTelefoneGrid(string? tel)
		{
			if (string.IsNullOrWhiteSpace(tel)) return "";
			var apenasNumeros = SomenteNumeros(tel);
			if (apenasNumeros.Length < 10 || apenasNumeros.Length > 11) return apenasNumeros;

			return apenasNumeros.Length == 11
				? long.Parse(apenasNumeros).ToString(@"(00) 00000\-0000")
				: long.Parse(apenasNumeros).ToString(@"(00) 0000\-0000");
		}

		
		public static string AplicarMascaraCpfInput(string? valor)
		{
			var numeros = SomenteNumeros(valor);
			if (numeros.Length > 11) numeros = numeros.Substring(0, 11);

			if (numeros.Length <= 3) return numeros;
			if (numeros.Length <= 6) return $"{numeros.Substring(0, 3)}.{numeros.Substring(3)}";
			if (numeros.Length <= 9) return $"{numeros.Substring(0, 3)}.{numeros.Substring(3, 3)}.{numeros.Substring(6)}";
			return $"{numeros.Substring(0, 3)}.{numeros.Substring(3, 3)}.{numeros.Substring(6, 3)}-{numeros.Substring(9)}";
		}

		public static string AplicarMascaraTelefoneInput(string? valor)
		{
			var numeros = SomenteNumeros(valor);
			if (numeros.Length > 11) numeros = numeros.Substring(0, 11);

			if (numeros.Length <= 2) return numeros;
			if (numeros.Length <= 7) return $"({numeros.Substring(0, 2)}) {numeros.Substring(2)}";
			return $"({numeros.Substring(0, 2)}) {numeros.Substring(2, 5)}-{numeros.Substring(7)}";
		}

		public static string LimparCpf(string? cpf) => SomenteNumeros(cpf);

		public static string LimparTelefone(string? tel) => SomenteNumeros(tel);

				public static string SomenteNumeros(string? valor)
		{
			if (string.IsNullOrWhiteSpace(valor)) return "";
			return new string(valor.Where(char.IsDigit).ToArray());
		}
	}
}