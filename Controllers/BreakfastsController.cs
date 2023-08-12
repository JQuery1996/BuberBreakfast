using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers {
    public class BreakfastsController : ApiController {
        private readonly IBreakfastService _breakfastService;
        public BreakfastsController(IBreakfastService _breakfastService) {
            this._breakfastService = _breakfastService;
        }
        [HttpPost]
        public IActionResult CreateBreakfast(CreateBreakfastRequest request) {
            ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(request);

            if (requestToBreakfastResult.IsError) {
                return Problem(requestToBreakfastResult.Errors);
            }

            var breakfast = requestToBreakfastResult.Value;
            //TODO: save breakfast to the database
            ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);
            return createBreakfastResult.Match(created => CreatedAtGetBreakfast(breakfast), errors => Problem(errors));
        }


        [HttpGet("{Id:guid}")]
        public IActionResult GetBreakfast(Guid Id) {
            ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(Id);
            return getBreakfastResult.Match(breakfast => Ok(MapBreakfastResponse(breakfast)), errors => Problem(errors));
        }


        [HttpPut("{Id:guid}")]
        public IActionResult UpsertBreakfast(Guid Id, UpsertBreakfastRequest request) {
            var requestToBreakfastResult = Breakfast.From(Id, request);
            if (requestToBreakfastResult.IsError) {
                return Problem(requestToBreakfastResult.Errors);
            }
            var breakfast = requestToBreakfastResult.Value;
            ErrorOr<UpsertedBreakfast> upsertedBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);
            return upsertedBreakfastResult.Match(upserted => upserted.isNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(), errors => Problem(errors));
        }

        [HttpDelete("{Id:guid}")]
        public IActionResult DeleteBreakfast(Guid Id) {
            ErrorOr<Deleted> deleteBreakfastResult = _breakfastService.DeleteBreakfast(Id);
            return deleteBreakfastResult.Match(deleted => NoContent(), errors => Problem(errors));
        }

        private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast) {
            return new BreakfastResponse(
                breakfast.Id, breakfast.Name, breakfast.Description, breakfast.StartDateTime, breakfast.EndDateTime, breakfast.LastModifiedDateTime, breakfast.Savory, breakfast.Sweet);
        }
        private IActionResult CreatedAtGetBreakfast(Breakfast breakfast) {
            return CreatedAtAction(
                actionName: nameof(GetBreakfast),
                routeValues: new { Id = breakfast.Id },
                value: MapBreakfastResponse(breakfast)
            );
        }
    }
}

