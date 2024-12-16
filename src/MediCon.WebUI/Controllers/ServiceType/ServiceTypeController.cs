using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Controllers.ServiceType.ViewModels;
using MediCon.WebUI.Services.ServiceTypes.Models;
using MediCon.WebUI.Services.ServiceTypes.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCon.WebUI.Controllers.ServiceType;

public class ServiceTypeController : BaseController
{
    private readonly IServiceTypeService _serviceType;

    public ServiceTypeController(IServiceTypeService serviceType)
    {
        _serviceType = serviceType;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = new ServiceTypeIndexViewModel
        {
            PageTitle = "List of Service Types",
        };

        var result = await _serviceType.Get(cancellationToken).ConfigureAwait(false);
        if (!result.Success)
        {
            return View(model.WithError(result.Message));
        }

        model.ServiceTypes = result.Data ?? [];
        return View(model);
    }

    public IActionResult Create()
    {
        return View(new ServiceTypeCreateViewModel { PageTitle = "Create Service Type" });
    }

    [HttpPost]
    public async Task<IActionResult> Create(ServiceTypeRequestModel model, CancellationToken cancellationToken)
    {
        var result = await _serviceType.Create(model, cancellationToken).ConfigureAwait(false);
        if (!result.Success)
        {
            var viewModel = new ServiceTypeCreateViewModel
            {
                PageTitle = "Create Service Type",
                ServiceTypeRequest = model,
            };

            return View(viewModel.WithError(result.Message));
        }

        var url = Url.Action(nameof(Index), nameof(ServiceTypeController).GetControllerName());
        return LocalRedirect(url!);
    }

    public async Task<IActionResult> Edit([FromRoute] long id, CancellationToken cancellationToken = default)
    {
        var model = new ServiceTypeEditViewModel
        {
            PageTitle = "Edit Service Type",
        };

        var result = await _serviceType.GetById(id, cancellationToken).ConfigureAwait(false);
        if (!result.Success)
        {
            return View(model.WithError(result.Message));
        }
        if (result.Data is null)
        {
            return View(model.WithError("Unable to retrieve service type"));
        }

        return View(new ServiceTypeEditViewModel
        {
            PageTitle = "Edit Service Type",
            ServiceTypeResponse = result.Data,
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromRoute] long id, [FromForm] ServiceTypeRequestModel model, CancellationToken cancellationToken = default)
    {
        var viewModel = new ServiceTypeEditViewModel
        {
            PageTitle = "Edit Service Type",
            ServiceTypeRequest = model,
        };

        var result = await _serviceType.Update(id, model, cancellationToken).ConfigureAwait(false);
        if (!result.Success)
        {
            return View(viewModel.WithError(result.Message));
        }

        var url = Url.Action(nameof(Index), nameof(ServiceTypeController).GetControllerName());
        return LocalRedirect(url!);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken = default)
    {
        var result = await _serviceType.Delete(id, cancellationToken).ConfigureAwait(false);
        if (!result.Success)
        {
            return ProblemJson(result.Message);
        }

        return SuccessJson();
    }
}
