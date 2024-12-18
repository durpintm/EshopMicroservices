namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);

        var product = await session.LoadAsync<Product>(command.Id);

        if (product == null) { throw new ProductNotFoundException(); }

        session.Delete<Product>(product.Id);
        await session.SaveChangesAsync();
        return new DeleteProductResult(true);
    }
}

