using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbcontext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            //Todo:Get Discount form Db
            var coupon = await dbcontext
                .Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
                coupon = new Coupon
                {
                    ProductName = "No disucount",
                    Description = "No Discount desc",
                    Amount = 0
                };
            logger.LogInformation("Discount is retrieved for product {ProductName}, Amount : {amount}", 
                coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;

        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            //
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
            }
            dbcontext.Coupons.Add(coupon);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully created for product {ProductName}, Amount : {amount}",
                coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;

        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
            }
            dbcontext.Coupons.Update(coupon);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully updated for product {ProductName}, Amount : {amount}",
                coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbcontext
                .Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
            }
            dbcontext.Coupons.Remove(coupon);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully deleted for product {ProductName}, Amount : {amount}",
                coupon.ProductName, coupon.Amount);
            return new DeleteDiscountResponse { Success = true };
        }
    }
}
