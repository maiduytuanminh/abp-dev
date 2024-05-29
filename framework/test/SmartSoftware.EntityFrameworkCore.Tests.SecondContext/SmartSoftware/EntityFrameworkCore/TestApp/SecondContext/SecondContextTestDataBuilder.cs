using System;
using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Guids;

namespace SmartSoftware.EntityFrameworkCore.TestApp.SecondContext;

public class SecondContextTestDataBuilder : ITransientDependency
{
    private readonly IBasicRepository<BookInSecondDbContext, Guid> _bookRepository;
    private readonly IGuidGenerator _guidGenerator;

    public SecondContextTestDataBuilder(IBasicRepository<BookInSecondDbContext, Guid> bookRepository, IGuidGenerator guidGenerator)
    {
        _bookRepository = bookRepository;
        _guidGenerator = guidGenerator;
    }

    public async Task BuildAsync()
    {
        await _bookRepository.InsertAsync(
            new BookInSecondDbContext(
                _guidGenerator.Create(),
                "TestBook1"
            )
        );
    }
}
