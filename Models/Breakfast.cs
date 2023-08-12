using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Models {
    public class Breakfast {

        public const int MinNameLength = 3;
        public const int MaxNameLenght = 50;

        public const int MinDescriptionLength = 50;
        public const int MaxDescriptionLength = 150;

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime StartDateTime { get; }
        public DateTime EndDateTime { get; }
        public DateTime LastModifiedDateTime { get; }
        public List<string> Savory { get; }
        public List<string> Sweet { get; }

        private Breakfast(Guid Id,
                         string Name,
                         string Description,
                         DateTime StartDateTime,
                         DateTime EndDateTime,
                         DateTime LastModifiedDateTime,
                         List<string> Savory,
                         List<string> Sweet) {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.StartDateTime = StartDateTime;
            this.EndDateTime = EndDateTime;
            this.LastModifiedDateTime = LastModifiedDateTime;
            this.Savory = Savory;
            this.Sweet = Sweet;
        }

        private static ErrorOr<Breakfast> Create(string Name,
                                                string Description,
                                                DateTime StartDateTime,
                                                DateTime EndDateTime,
                                                List<string> Savory,
                                                List<string> Sweet,
                                                Guid? Id = null) {

            List<Error> errors = new();

            if (Name.Length is < MinNameLength or > MaxNameLenght) {
                errors.Add(Errors.Breakfast.InvalidPropertyLength("Name", MinNameLength, MaxNameLenght));

            }
            if (Description.Length is < MinDescriptionLength or > MaxDescriptionLength) {
                errors.Add(Errors.Breakfast.InvalidPropertyLength("Description", MinDescriptionLength, MaxDescriptionLength));
            }


            if (errors.Count is > 0) {
                return errors;
            }
            return new Breakfast(Id ?? Guid.NewGuid(), Name, Description, StartDateTime, EndDateTime, DateTime.UtcNow, Savory, Sweet);

        }
        internal static ErrorOr<Breakfast> From(CreateBreakfastRequest request) {
            return Create(request.Name, request.Description, request.StartDateTime, request.EndDateTime, request.Savory, request.Sweet);
        }

        internal static ErrorOr<Breakfast> From(Guid id, UpsertBreakfastRequest request) {
            return Create(request.Name, request.Description, request.StartDateTime, request.EndDateTime, request.Savory, request.Sweet, Id);
        }
    }
}
