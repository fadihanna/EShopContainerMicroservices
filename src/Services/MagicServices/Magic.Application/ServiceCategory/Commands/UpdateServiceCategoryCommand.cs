using BuildingBlocks.Exceptions;

namespace Magic.Application.Commands
{
    public record UpdateServiceCategoryCommand(ServiceCategoryDto serviceCategory, int Id)
       : ICommand<UpdateServiceCategoryResponse>;
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
            var existingServiceCategory = await _serviceCategorySpecification.GetByIdAsync(x=>x.Id.Equals(command.Id), cancellationToken);
            if(existingServiceCategory  ==null)
                throw new NotFoundException("ServiceCategory", command.Id);

            existingServiceCategory.NameEN = command.serviceCategory.NameEN;
            existingServiceCategory.NameAR = command.serviceCategory.NameAR;
            existingServiceCategory.IconName = command.serviceCategory.IconName;
            existingServiceCategory.SortOrder = command.serviceCategory.SortOrder;
            existingServiceCategory.IsActive = command.serviceCategory.IsActive;

            await _serviceCategorySpecification.UpdateAsync(existingServiceCategory, cancellationToken);
            return new UpdateServiceCategoryResponse(existingServiceCategory.Id);
        }
    }
}
