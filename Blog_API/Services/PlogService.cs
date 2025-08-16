using Blog_API.DTOs;
using Blog_API.Models;
using Blog_API.Repoistries.Base;
using Blog_API.Services.Base;

namespace Blog_API.Services
{
    public class PlogService : IPlogService
    {

        // inject the unit of work and repository pattern here
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMainRepoistory<Plog> _plogRepository;


        public PlogService( IUnitOfWork unitOfWork, IMainRepoistory<Plog> plogRepoistory )
        {
            _plogRepository = plogRepoistory;
            _unitOfWork = unitOfWork;
        }


        // Get all plogs
        public async Task<IEnumerable<Plog>> GetAllPlogsAsync()
        {
            var plogs = await _plogRepository.GetAllAsync();
            if (plogs == null || !plogs.Any())
            {
                throw new KeyNotFoundException( "No plogs found." );
            }
            return plogs;
        }

        // Get plog by ID
        public async Task<Plog> GetPlogByIdAsync( int id )
        {
            var plog = await _plogRepository.GetByIdAsync( id );
            if (plog == null)
            {
                throw new KeyNotFoundException( $"Plog with ID {id} not found." );
            }

            return plog;
        }

        // Add a new plog
        public async Task<Plog> AddPlogAsync( PlogDto dto )
        {
            if (dto == null)
            {
                throw new ArgumentNullException( nameof( dto ), "Plog data cannot be null." );
            }

            // define a new Plog object and map the DTO properties to it
            var plog = new Plog
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = dto.UserId,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.Now,
            };

            await _plogRepository.AddAsync( plog );
            await _unitOfWork.SaveChangesAsync();

            return plog;
        }

        // Update an existing plog
        public async Task<Plog> UpdatePlogAsync( int id, PlogDto dto )
        {
            var existingPlog = await _plogRepository.GetByIdAsync( id );
            if (existingPlog == null)
            {
                throw new KeyNotFoundException( $"Plog with ID {id} not found." );
            }
            if (dto == null)
            {
                throw new ArgumentNullException( nameof( dto ), "Plog data cannot be null." );
            }

            // Map the DTO properties to the existing plog
            existingPlog.Title = dto.Title;
            existingPlog.Content = dto.Content;
            existingPlog.CategoryId = dto.CategoryId;
            existingPlog.UpdatedAt = DateTime.Now;
            await _plogRepository.UpdateAsync( id, existingPlog );

            await _unitOfWork.SaveChangesAsync();

            return existingPlog;
        }


        // Delete a plog by ID
        public async Task<string> DeletePlogAsync( int id )
        {
            var plog =await _plogRepository.GetByIdAsync( id );
            if (plog == null)
            {
                throw new KeyNotFoundException( $"Plog with ID {id} not found." );
            }

            await _plogRepository.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return "Plog deleted successfully.";
        }



    }
}
