using ACHSimulartor.Application.Interfaces;
using ACHSimulartor.Domain.Dtos;
using ACHSimulartor.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferRequestController(ITransferRequestService transferRequestService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await transferRequestService.GetAllTransferRequestsAsync();
            if (models.IsFailure)
            {
                if (models.Message is null)
                    return NotFound(models.Message);
                return NotFound(ErrorMessages.RequestNotFoundError);
            }

            return Ok(models);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransferRequestDto model)
        {
            var resultModel = await transferRequestService.CreateTransferRequestAsync(model);
            if (resultModel.IsFailure)
            {
                if (resultModel.Message is null)
                    return BadRequest(ErrorMessages.BadRequestError);
                return BadRequest(resultModel.Message);
            }

            return Ok(resultModel);
        }

        [HttpPut("/cancel/{id:int}")]
        public async Task<IActionResult> CancelTransfer(int id)
        {
            var resultModel = await transferRequestService.CanceledTransferRequestAsync(id);
            if (resultModel.IsFailure)
            {
                if (resultModel.Message is null)
                    return NotFound(ErrorMessages.NotFoundError);
                return NotFound(resultModel.Message);
            }

            return Ok(resultModel);
        }

        [HttpPut("/confirm/{id:int}")]
        public async Task<IActionResult> ConfirmTransfer(int id)
        {
            var resultModel = await transferRequestService.ConfirmedTransferRequestAsync(id);
            if (resultModel.IsFailure)
            {
                if (resultModel.Message is null)
                    return BadRequest(ErrorMessages.ConfirmedRequestError);
                return BadRequest(resultModel.Message);
            }

            return Ok(resultModel);
        }

        [HttpGet("/{id:int}")]
        public async Task<IActionResult> GetRequest(int id)
        {
            var resultModel = await transferRequestService.GetTransferRequestAsync(id);
            if (resultModel.IsFailure)
            {
                if (resultModel.Message is null)
                    return NotFound(ErrorMessages.RequestNotFoundError);
                return NotFound(resultModel.Message);
            }

            return Ok(resultModel);
        }
    }
}