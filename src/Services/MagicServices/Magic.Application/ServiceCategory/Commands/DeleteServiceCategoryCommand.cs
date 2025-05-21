using BuildingBlocks.Exceptions;
using Magic.Domain.Specifications;

namespace Magic.Application.Commands
{
    public record DeleteServiceCategoryCommand(int Id)
      : ICommand<DeleteServiceCategoryResponse>;

    public record DeleteServiceCategoryResponse(int Id);

    public class DeleteServiceCategoryHandler : ICommandHandler<DeleteServiceCategoryCommand, DeleteServiceCategoryResponse>
    {
        private readonly IServiceCategorySpecification _serviceCategorySpecification;

        public DeleteServiceCategoryHandler(IServiceCategorySpecification serviceCategorySpecification)
        {
            _serviceCategorySpecification = serviceCategorySpecification;
        }
        public async Task<DeleteServiceCategoryResponse> Handle(DeleteServiceCategoryCommand command, CancellationToken cancellationToken)
        {
            var serviceCategory = await _serviceCategorySpecification.GetByIdAsync(x => x.Id.Equals(command.Id), cancellationToken);
            if (serviceCategory == null)
                throw new NotFoundException("ServiceCategory", command.Id);

            await _serviceCategorySpecification.DeleteAsync(serviceCategory, cancellationToken);
            return new DeleteServiceCategoryResponse(serviceCategory.Id);
        }
    }
}
