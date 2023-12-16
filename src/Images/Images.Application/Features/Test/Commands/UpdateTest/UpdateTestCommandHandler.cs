using BuildingMarket.Images.Application.Contracts;
using MediatR;

namespace BuildingMarket.Images.Application.Features.Test.Commands.UpdateTest
{
    public class UpdateTestCommandHandler(ITestStore testStore) : IRequestHandler<UpdateTestCommand>
    {
        public ITestStore _testStore { get; set; } = testStore;

        public async Task Handle(UpdateTestCommand request, CancellationToken cancellationToken)
            => await _testStore.UpdateTestRedis();
    }
}
