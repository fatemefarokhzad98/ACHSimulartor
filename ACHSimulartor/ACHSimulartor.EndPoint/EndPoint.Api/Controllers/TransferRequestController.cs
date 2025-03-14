using ACHSimulartor.Application.Interfaces;
using ACHSimulartor.Domain.Dtos;
using ACHSimulartor.Domain.Shared;
using EndPoint.Api.HttpManager;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EndPoint.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferRequestController(ITransferRequestService _service) : Controller
    {
        public async Task<IActionResult> List()
        {
            var models = await _service.GetAllTransferRequestsAsync();
            if (models.IsFailure)
            {
                if(models.Message is null)
                    return JsonResponseStatus.NotFound(ErrorMessages.RequestNotFoundError);
                return JsonResponseStatus.NotFound(models.Message);

            }
            return JsonResponseStatus.Success(models.Value,models.Message);
        }
        public async Task<IActionResult> Create([FromBody] CreateTransferRequestDto model)
        {
            var resultModel = await _service.CreateTransferRequestAsync(model);
            if (resultModel.IsFailure)
            {
                if (resultModel.Message is null)
                    return JsonResponseStatus.BadRequest(ErrorMessages.BadRequestError);
                return JsonResponseStatus.BadRequest(resultModel.Message);

            }
            if(resultModel.Message is null)
                return JsonResponseStatus.Success(SuccessMessages.SuccessfullyDone);
            return JsonResponseStatus.Success(resultModel.Message);
        }
        public async Task<IActionResult> CancledTransfer(int requestId)
        {
            var resultModel = await _service.CanceledTransferRequestAsync(requestId);
            if (resultModel.IsFailure)
            {
                if (resultModel.Message is null)
                    return JsonResponseStatus.Error(ErrorMessages.CancledRequestError);
                return JsonResponseStatus.Error(resultModel.Message);

            }
            if (resultModel.Message is null)
                return JsonResponseStatus.Success(SuccessMessages.CancledRequestSuccessfullyDone);
            return JsonResponseStatus.Success(resultModel.Message);
        }
        public async Task<IActionResult> ConfirmedTransfer(int requestId)
        {
            var resultModel = await _service.ConfirmedTransferRequestAsync(requestId);
            if (resultModel.IsFailure)
            {
                if (resultModel.Message is null)
                    return JsonResponseStatus.Error(ErrorMessages.ConfirmedRequestError);
                return JsonResponseStatus.Error(resultModel.Message);

            }
            if (resultModel.Message is null)
                return JsonResponseStatus.Success(SuccessMessages.ConfirmedRequestSuccessfullyDone);
            return JsonResponseStatus.Success(resultModel.Message);
        }
        public async Task<IActionResult> DetailRequest(int requestId)
        {
            var resultModel = await _service.GetTransferRequestAsync(requestId);
            if (resultModel.IsFailure)
            {
                if (resultModel.Message is null)
                    return JsonResponseStatus.NotFound(ErrorMessages.RequestNotFoundError);
                return JsonResponseStatus.NotFound(resultModel.Message);

            }
            if (resultModel.Message is null)
                return JsonResponseStatus.Success(resultModel.Value,SuccessMessages.GetRequestSuccessfullyDone);
            return JsonResponseStatus.Success(resultModel.Value,resultModel.Message);

        }
    }
}
