
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{
    //public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductCommandEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/product/{Id}", async (Guid Id, ISender sender) => 
            {
                var result = await sender.Send(Id);

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })
                .WithName("DeleteProduct")
                .Produces<DeleteProductCommandEndpoint>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product");
        }
    }
}
