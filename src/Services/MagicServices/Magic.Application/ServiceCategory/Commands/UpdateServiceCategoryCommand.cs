using BuildingBlocks.Exceptions;
using Magic.Application.Dtos;

namespace Magic.Application.Commands
{
    public class UpdateServiceCategoryCommand : ICommand<UpdateServiceCategoryResponse>
    {
        public UpdateServiceCategoryCommand() { }

        public UpdateServiceCategoryCommand(ServiceCategoryDto serviceCategory, int id)
        {
            ServiceCategory = serviceCategory;
            Id = id;
        }

        public ServiceCategoryDto ServiceCategory { get; set; }
        public int Id { get; set; }
    }

    public record UpdateServiceCategoryResponse(int Id);

    public class UpdateServiceCategoryHandler : ICommandHandler<UpdateServiceCategoryCommand, UpdateServiceCategoryResponse>
    {
        private readonly IServiceCategorySpecification _serviceCategorySpecification;

        public UpdateServiceCategoryHandler(IServiceCategorySpecification serviceCategorySpecification)
        {
            _serviceCategorySpecification = serviceCategorySpecification;
        }

        public async Task<UpdateServiceCategoryResponse> Handle(UpdateServiceCategoryCommand command, CancellationToken cancellationToken)
        {
            var existingServiceCategory = await _serviceCategorySpecification.GetByIdAsync(x => x.Id.Equals(command.Id), cancellationToken);

            if (existingServiceCategory == null)
                throw new NotFoundException("ServiceCategory", command.Id);

            existingServiceCategory.NameEN = command.ServiceCategory.NameEN;
            existingServiceCategory.NameAR = command.ServiceCategory.NameAR;
            existingServiceCategory.IconName = command.ServiceCategory.IconName;
            existingServiceCategory.SortOrder = command.ServiceCategory.SortOrder;
            existingServiceCategory.IsActive = command.ServiceCategory.IsActive;

            await _serviceCategorySpecification.UpdateAsync(existingServiceCategory, cancellationToken);

            return new UpdateServiceCategoryResponse(existingServiceCategory.Id);
        }
    }
}
