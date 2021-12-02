using System;
using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.MiniBlog.Core.Domain.People.Events;

namespace Zamin.MiniBlog.Core.Domain.People.Entities
{
    public class Person : AggregateRoot
    {
        public Title FirstName { get; set; }
        public Title LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        private Person()
        {
        }

        private Person(Title firstName, Title lastName, DateTime? birthDate)
        {
            if (string.IsNullOrEmpty(firstName.Value))
                throw new InvalidEntityStateException(
                    ZaminMiniBlogValidationResources.ValidationErrorRequired,
                    ZaminMiniBlogResources.FirstName);

            if (string.IsNullOrEmpty(lastName.Value))
                throw new InvalidEntityStateException(
                    ZaminMiniBlogValidationResources.ValidationErrorRequired,
                    ZaminMiniBlogResources.LastName);

            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;

            AddEvent(new PersonCreated(BusinessId.Value, FirstName.Value, LastName.Value, birthDate));
        }

        public static Person Create(Title firstName, Title lastName, DateTime? birthDate)
            => new(firstName, lastName, birthDate);

        public void EditFirstName(Title firstName)
        {
            if (string.IsNullOrEmpty(firstName.Value))
                throw new InvalidEntityStateException(
                    ZaminMiniBlogValidationResources.ValidationErrorRequired,
                    ZaminMiniBlogResources.FirstName);

            if (FirstName != firstName)
            {
                FirstName = firstName;

                AddEvent(new PersonFirstNameEdited(BusinessId.Value, FirstName.Value));
            }
        }

        public void EditLastName(Title lastName)
        {
            if (string.IsNullOrEmpty(lastName.Value))
                throw new InvalidEntityStateException(
                    ZaminMiniBlogValidationResources.ValidationErrorRequired,
                    ZaminMiniBlogResources.LastName);

            if (LastName != lastName)
            {
                LastName = lastName;

                AddEvent(new PersonLastNameEdited(BusinessId.Value, LastName.Value));
            }
        }

        public void EditBirthDate(DateTime birthDate)
        {
            if (BirthDate != birthDate)
            {
                BirthDate = birthDate;

                AddEvent(new PersonBirthDateEdited(BusinessId.Value, BirthDate.Value));
            }
        }

        public void Remove() => AddEvent(new PersonRemoved(BusinessId.Value));
    }
}
