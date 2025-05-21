namespace Magic.Application.Commands
{
    public record InsertServiceCategoryCommand(ServiceCategoryDto serviceCategory)
       : ICommand<InsertServiceCategoryResponse>;

    public record InsertServiceCategoryResponse(int Id);

    public class InsertServiceCategoryHandler : ICommandHandler<InsertServiceCategoryCommand, InsertServiceCategoryResponse>
    {
        private readonly IServiceCategorySpecification _serviceCategorySpecification;

        public InsertServiceCategoryHandler(IServiceCategorySpecification serviceCategorySpecification)
        {
            _serviceCategorySpecification = serviceCategorySpecification;
        }
        public async Task<InsertServiceCategoryResponse> Handle(InsertServiceCategoryCommand command, CancellationToken cancellationToken)
        {
            var serviceCategory = ServiceCategoryExtension.CreateServiceCategory(command.serviceCategory);
            await _serviceCategorySpecification.InsertAsync(serviceCategory, cancellationToken);
            return new InsertServiceCategoryResponse(serviceCategory.Id);
        }
    }
}
