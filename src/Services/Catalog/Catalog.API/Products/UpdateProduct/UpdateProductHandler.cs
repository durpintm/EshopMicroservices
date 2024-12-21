namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, int Price) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater than 0");
    }
}

public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductEndpoint> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null) { throw new ProductNotFoundException(); }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        try
        {
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Product with ID {Id} successfully updated.", command.Id);

            return new UpdateProductResult(true);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update product with ID {Id}.", command.Id);
            return new UpdateProductResult(false);
        }
    }
}

