namespace SchoolManagement.Communication.ResponsesDto
{
	public class ApiResponse<T>
	{
		public bool Sucess { get; set; } = false;
		public Dictionary<string, List<string>> Errors { get; set; } = [];
		public T? Data { get; set; }
	}
}
