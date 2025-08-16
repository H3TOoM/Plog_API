using Blog_API.DTOs;
using Blog_API.Models;
using Blog_API.Repoistries.Base;
using Blog_API.Services.Base;

namespace Blog_API.Services
{
    public class CommentService : ICommentService
    {

        // inject main repository and unit of work
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMainRepoistory<Comment> _commentRepository;


        // inject the spical repository for comments
        private readonly ICommentRepoistory _commentRepo;

        public CommentService( IUnitOfWork unitOfWork, IMainRepoistory<Comment> commentRepository, ICommentRepoistory commentRepo )
        {
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
            _commentRepo = commentRepo;
        }

        public async Task<CommentDto> AddCommentAsync( CommentDto dto )
        {
            var comment = new Comment
            {
                Content = dto.Content,
                UserId = dto.UserId,
                PlogId = dto.PlogId,
                CreatedAt = DateTime.UtcNow
            };


            await _commentRepository.AddAsync( comment );
            await _unitOfWork.SaveChangesAsync();

            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                UserId = comment.UserId,
                PlogId = comment.PlogId,
                CreatedAt = comment.CreatedAt
            };

        }

        public async Task<string> DeleteCommentAsync( int id )
        {
            var comment = await _commentRepository.GetByIdAsync( id );
            if (comment == null)
            {
                throw new KeyNotFoundException( $"Comment with ID {id} not found." );
            }

            await _commentRepository.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return $"Comment with ID {id} has been deleted successfully.";
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync()
        {
            var comments = await _commentRepository.GetAllAsync();

            if (comments == null || !comments.Any())
            {
                throw new KeyNotFoundException( "No comments found." );
            }
            // Map the Comment entities to CommentDto
            var commentDtos = comments.Select( c => new CommentDto
            {
                Id = c.Id,
                Content = c.Content,
                UserId = c.UserId,
                PlogId = c.PlogId,
                CreatedAt = c.CreatedAt
            } );
            return commentDtos;
        }

        public async Task<CommentDto> GetCommentByIdAsync( int id )
        {
            var comment = await _commentRepository.GetByIdAsync( id );
            if (comment == null)
            {
                throw new KeyNotFoundException( $"Comment with ID {id} not found." );
            }
            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                UserId = comment.UserId,
                PlogId = comment.PlogId,
                CreatedAt = comment.CreatedAt
            };
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByPlogIdAsync( int plogId )
        {
            var comments = await _commentRepo.GetCommentsByPlogIdAsync( plogId );

            if (comments == null || !comments.Any())
            {
                throw new KeyNotFoundException( $"No comments found for Plog ID {plogId}." );
            }


            // Map the Comment entities to CommentDto
            var commentDtos = comments.Select( c => new CommentDto
            {
                Id = c.Id,
                Content = c.Content,
                UserId = c.UserId,
                PlogId = c.PlogId,
                CreatedAt = c.CreatedAt
            } );
            return commentDtos;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByUserIdAsync( int userId )
        {
            var comments = await _commentRepo.GetCommentsByUserIdAsync( userId );

            if (comments == null || !comments.Any())
            {
                throw new KeyNotFoundException( $"No comments found for User ID {userId}." );
            }

            // Map the Comment entities to CommentDto
            var commentDtos = comments.Select( c => new CommentDto
            {
                Id = c.Id,
                Content = c.Content,
                UserId = c.UserId,
                PlogId = c.PlogId,
                CreatedAt = c.CreatedAt
            } );
            return commentDtos;

        }

        public async Task<CommentDto> UpdateCommentAsync( int id, CommentDto dto )
        {
            var comment = await _commentRepository.GetByIdAsync( id );
            if (comment == null)
            {
                throw new KeyNotFoundException( $"Comment with ID {id} not found." );
            }

            comment.Content = dto.Content;
            comment.UserId = dto.UserId;
            comment.PlogId = dto.PlogId;
            comment.UpdatedAt = DateTime.UtcNow;
            await _commentRepository.UpdateAsync( id, comment );
            await _unitOfWork.SaveChangesAsync();

            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                UserId = comment.UserId,
                PlogId = comment.PlogId,
                CreatedAt = comment.CreatedAt,
            };
        }
    }
}
