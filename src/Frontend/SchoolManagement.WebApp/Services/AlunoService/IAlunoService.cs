using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.WebApp.Services.AlunoService
{
    public interface IAlunoService
    {
        public Task<PagedResponse<ResponseReadAlunoDto>> GetAll();

        public Task<bool> Delete(int id);

        public Task<bool> Create(RequestCreateAlunoDto aluno);

        public Task<bool> Update(int id, RequestUpdateAlunoDto aluno);



	}
}
