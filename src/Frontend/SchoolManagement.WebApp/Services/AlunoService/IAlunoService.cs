using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.WebApp.Services.AlunoService
{
    public interface IAlunoService
    {
        public Task<PagedResponse<ResponseReadAlunoDto>> GetAlunos();

        public Task<bool> Delete(int id);

	}
}
