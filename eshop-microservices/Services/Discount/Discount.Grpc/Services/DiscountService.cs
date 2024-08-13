using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            // TODO: GetDiscount from database
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            
            coupon ??= new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Description" };

            logger.LogInformation("Discount is retrieved for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            // TODO: Create Discount to database
            var objToCreate = request.Coupon.Adapt<Coupon>() ?? throw new RpcException (new Status(StatusCode.InvalidArgument, "Invalid request object."));
            
            await dbContext.Coupons.AddAsync(objToCreate);
            
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully created. ProductName: {productName}, Amount: {amount}", objToCreate.ProductName, objToCreate.Amount);

            var result = objToCreate.Adapt<CouponModel>();
            
            return result;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            // TODO: update existing record
            var objToUpdate = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

            dbContext.Coupons.Update(objToUpdate);

            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully updated. ProductName: {productName}, Amount: {amount}", objToUpdate.ProductName, objToUpdate.Amount);

            var result = objToUpdate.Adapt<CouponModel>();

            return result;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            // TODO: Delete Existing record
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName: {request.ProductName} is not found!"));

            dbContext.Coupons.Remove(coupon);

            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully deleted. ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
