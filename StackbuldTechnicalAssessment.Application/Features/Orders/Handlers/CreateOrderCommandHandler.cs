using AutoMapper;
using MediatR;
using StackbuldTechnicalAssessment.Application.Features.Orders.Commands;
using StackbuldTechnicalAssessment.Application.Features.Orders.Dtos;
using StackbuldTechnicalAssessment.Domain.Entities;
using StackbuldTechnicalAssessment.Infrastructure.Repository.Interface;
using StackbuldTechnicalAssessment.Infrastructure.Services.Interfaces;

namespace StackbuldTechnicalAssessment.Application.Features.Orders.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderSuccessAndErrorResponseDto<OrderResponseDto>>
    {
        private readonly IRepository<Order> _orderHeaderRepo;
        private readonly IRepository<OrderDetails> _orderDetailsRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IRepository<Order> orderHeaderRepo, IRepository<OrderDetails> orderDetailsRepo, IRepository<Product> productRepo, ITransactionService transactionService, IMapper mapper)
        {
            _orderHeaderRepo = orderHeaderRepo;
            _orderDetailsRepo = orderDetailsRepo;
            _productRepo = productRepo;
            _transactionService = transactionService;
            _mapper = mapper;

        }
        public async Task<OrderSuccessAndErrorResponseDto<OrderResponseDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            if (request.OrderCreateDto.OrderDetails == null || !request.OrderCreateDto.OrderDetails.Any())
            {
                return new OrderSuccessAndErrorResponseDto<OrderResponseDto>
                {
                    StatusCode = 400,
                    Message = "Order must contain at least one item.",

                };

            }
            // Start transaction to ensure consistency
            var transaction = await _transactionService.BeginTransactionAsync(cancellationToken);

            var orderDetails = _mapper.Map<List<OrderDetails>>(request.OrderCreateDto);

            // Validate stock availability
            foreach (var detail in orderDetails)
            {
                var product = _productRepo.Query().FirstOrDefault(p => p.Id == detail.ProductId);

                if (product == null)
                {
                    return new OrderSuccessAndErrorResponseDto<OrderResponseDto>
                    {
                        StatusCode = 404,
                        Message = $"Product with ID {detail.ProductId} not found.",

                    };

                }

                if (product.StockQuantity < detail.Quantity)
                {
                    await _transactionService.RollbackTransactionAsync(cancellationToken);
                    return new OrderSuccessAndErrorResponseDto<OrderResponseDto>
                    {
                        StatusCode = 409,
                        Message = $"Insufficient stock for product '{product.Name}'. Requested: {detail.Quantity}, Available: {product.StockQuantity}",

                    };
                }
                product.StockQuantity -= detail.Quantity;
                await _productRepo.UpdateAsync(product);
            }
            // Calculate total items and total price
            int totalItems = orderDetails.Sum(d => d.Quantity);
            decimal subtotal = orderDetails.Sum(d => d.Quantity * d.UnitPrice);

            var orderHeader = _mapper.Map<Order>(request.OrderCreateDto);

            orderHeader.TotalItems = totalItems;
            orderHeader.OrderTotal = subtotal;
            orderHeader.OrderDate = DateTime.UtcNow;

            await _orderHeaderRepo.AddAsync(orderHeader);
            await _orderHeaderRepo.SaveChanges();

            // Attach details to header
            foreach (var detail in orderDetails)
            {
                detail.OrderId = orderHeader.Id;
            }

            await _orderDetailsRepo.AddRangeAsync(orderDetails);
            await _orderDetailsRepo.SaveChanges();

            // Commit transaction
            await _transactionService.CommitTransactionAsync(cancellationToken);

            orderHeader.OrderDetails = orderDetails;
            var response = _mapper.Map<OrderResponseDto>(orderHeader);
            return new OrderSuccessAndErrorResponseDto<OrderResponseDto>
            {
                StatusCode = 200,
                Message = "order added successfully!!!",
                Data = response
            };
        }
    }
}
