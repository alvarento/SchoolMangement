namespace SchoolManagement.Communication.Utils.Logs
{
	public static class ConsoleExtensios
	{
		public static void Log<T>(this T value)
		{

			if(value is IEnumerable<T> lista) foreach (var item in lista) Console.WriteLine($"{value}");

			Console.WriteLine(value);
		}
	}
}
