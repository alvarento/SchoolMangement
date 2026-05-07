namespace SchoolManagement.Communication.Utils.Logs
{
	public static class LogLaunch
	{

		public static void PrintLauchConsole(string port, string message, bool isAPI = false)
		{
			
			Console.ForegroundColor = ConsoleColor.Green;
			for (int i = 0; i < 5; i++) Console.WriteLine(".");
			Console.Write($"{message} rodando em: ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"➜  https://localhost:{port} ✅");


			if (isAPI)
			{
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("📌 Acesse a documentação em: ");
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine($"➜  https://localhost:{port}/swagger ✅");
				Console.ResetColor();
			}

		}
	}
}
