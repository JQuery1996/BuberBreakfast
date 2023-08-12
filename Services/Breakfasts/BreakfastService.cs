using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts {
    public class BreakfastService : IBreakfastService {
        private static readonly Dictionary<Guid, Breakfast> _breakfasts = new Dictionary<Guid, Breakfast>();
        public ErrorOr<Created> CreateBreakfast(Breakfast breakfast) {
            _breakfasts.Add(breakfast.Id, breakfast);
            return Result.Created;
        }

        public ErrorOr<Deleted> DeleteBreakfast(Guid Id) {
            var deleted = _breakfasts.Remove(Id);
            return deleted ? Result.Deleted : Errors.Breakfast.NotFound;
        }

        public ErrorOr<Breakfast> GetBreakfast(Guid Id) {
            if (_breakfasts.TryGetValue(Id, out var breakfast)) {
                return breakfast;
            }

            return Errors.Breakfast.NotFound;
        }

        public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast) {
            var isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);
            _breakfasts[breakfast.Id] = breakfast;
            return new UpsertedBreakfast(isNewlyCreated);
        }
    }
}
