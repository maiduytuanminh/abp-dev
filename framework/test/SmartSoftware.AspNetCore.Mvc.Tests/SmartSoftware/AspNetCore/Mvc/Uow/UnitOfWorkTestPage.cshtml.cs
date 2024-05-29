using Microsoft.AspNetCore.Mvc;
using Shouldly;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.Uow;

namespace SmartSoftware.AspNetCore.Mvc.Uow;

[IgnoreAntiforgeryToken]
public class UnitOfWorkTestPage : SmartSoftwarePageModel
{
    private readonly TestUnitOfWorkConfig _testUnitOfWorkConfig;

    public UnitOfWorkTestPage(TestUnitOfWorkConfig testUnitOfWorkConfig)
    {
        _testUnitOfWorkConfig = testUnitOfWorkConfig;
    }

    public IActionResult OnGetRequiresUow()
    {
        CurrentUnitOfWork.ShouldNotBeNull();
        CurrentUnitOfWork.Options.IsTransactional.ShouldBeFalse();

        return Content("OK");
    }

    public IActionResult OnPostRequiresUow()
    {
        CurrentUnitOfWork.ShouldNotBeNull();
        CurrentUnitOfWork.Options.IsTransactional.ShouldBeTrue();

        return Content("OK");
    }

    [UnitOfWork(isTransactional: true)]
    public ObjectResult OnGetHandledException()
    {
        CurrentUnitOfWork.ShouldNotBeNull();
        CurrentUnitOfWork.Options.IsTransactional.ShouldBeTrue();

        throw new UserFriendlyException("This is a sample exception!");
    }

    public ObjectResult OnGetExceptionOnComplete()
    {
        CurrentUnitOfWork.ShouldNotBeNull();
        CurrentUnitOfWork.Options.IsTransactional.ShouldBeFalse();

        _testUnitOfWorkConfig.ThrowExceptionOnComplete = true;

        //Prevent rendering of pages.
        return new ObjectResult("");
    }
}
