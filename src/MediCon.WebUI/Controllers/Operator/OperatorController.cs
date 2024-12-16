using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Controllers.Operator.ViewModels;
using MediCon.WebUI.Services.OperatorInfo.Models;
using MediCon.WebUI.Services.OperatorInfo.Services;

using Microsoft.AspNetCore.Mvc;

namespace MediCon.WebUI.Controllers.Operator
{
    public class OperatorController : BaseController
    {
        private readonly IOperatorInfoService _operatorInfoService;

        public OperatorController(IOperatorInfoService operatorInfoService)
        {
            _operatorInfoService = operatorInfoService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var model = new OperatorIndexViewModel
            {
                PageTitle = "List of Operators",
            };

            var result = await _operatorInfoService.Get(cancellationToken).ConfigureAwait(false);
            if (!result.Success)
            {
                return View(model.WithError(result.Message));
            }

            model.Operators = result.Data ?? [];
            return View(model);
        }

        public IActionResult Create()
        {
            return View(new OperatorCreateViewModel { PageTitle = "Create Operator" });
        }

        [HttpPost]
        public async Task<IActionResult> Create(OperatorInfoRequestModel model, CancellationToken cancellationToken)
        {
            var result = await _operatorInfoService.Create(model, cancellationToken).ConfigureAwait(false);
            if (!result.Success)
            {
                var viewModel = new OperatorCreateViewModel
                {
                    PageTitle = "Create Operator",
                    OperatorRequest = model,
                };

                return View(viewModel.WithError(result.Message));
            }

            var url = Url.Action(nameof(Index), nameof(OperatorController).GetControllerName());
            return LocalRedirect(url!);
        }

        public async Task<IActionResult> Edit([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            var model = new OperatorEditViewModel
            {
                PageTitle = "Edit Operator",
            };

            var result = await _operatorInfoService.GetById(id, cancellationToken).ConfigureAwait(false);
            if (!result.Success)
            {
                return View(model.WithError(result.Message));
            }
            if (result.Data is null)
            {
                return View(model.WithError("Unable to retrieve Operator"));
            }

            return View(new OperatorEditViewModel
            {
                PageTitle = "Edit Operator",
                OperatorResponse = result.Data,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] long id, [FromForm] OperatorInfoRequestModel model, CancellationToken cancellationToken = default)
        {
            var viewModel = new OperatorEditViewModel
            {
                PageTitle = "Edit Operator",
                OperatorRequest = model,
            };

            var result = await _operatorInfoService.Update(id, model, cancellationToken).ConfigureAwait(false);
            if (!result.Success)
            {
                return View(viewModel.WithError(result.Message));
            }

            var url = Url.Action(nameof(Index), nameof(OperatorController).GetControllerName());
            return LocalRedirect(url!);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken = default)
        {
            var result = await _operatorInfoService.Delete(id, cancellationToken).ConfigureAwait(false);
            if (!result.Success)
            {
                return ProblemJson(result.Message);
            }

            return SuccessJson();
        }
    }
}