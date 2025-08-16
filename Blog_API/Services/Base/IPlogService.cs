using Blog_API.DTOs;
using Blog_API.Models;

namespace Blog_API.Services.Base
{
    public interface IPlogService
    {
        Task<IEnumerable<Plog>> GetAllPlogsAsync();
        Task<Plog> GetPlogByIdAsync(int id);

        Task<Plog> AddPlogAsync(PlogDto dto);

        Task<Plog> UpdatePlogAsync(int id, PlogDto dto);

        Task<string> DeletePlogAsync(int id);
    }
}
