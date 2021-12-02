using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.ValueObjects;
using Zamin.MiniBlog.Core.Domain.Posts.Events;

namespace Zamin.MiniBlog.Core.Domain.Posts.Entities
{
    public class Post : AggregateRoot
    {
        public BusinessId PersonBusinessId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }

        private Post()
        {

        }

        private Post(BusinessId personBusinessId, string title, string content)
        {
            PersonBusinessId = personBusinessId;
            Title = title;
            Content = content;

            AddEvent(new PostCreated(BusinessId.Value, Title, Content));
        }

        public static Post Create(BusinessId personBusinessId, string title, string content)
            => new(personBusinessId, title, content);

        public void EditTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new InvalidEntityStateException(
                    ZaminMiniBlogValidationResources.ValidationErrorRequired,
                    ZaminMiniBlogResources.Title,
                    ZaminMiniBlogResources.Post);

            if (Title != title)
            {
                Title = title;

                AddEvent(new PostTitleEdited(BusinessId.Value, Title));
            }
        }

        public void EditContent(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new InvalidEntityStateException(
                    ZaminMiniBlogValidationResources.ValidationErrorRequired,
                    ZaminMiniBlogResources.Content,
                    ZaminMiniBlogResources.Post);

            if (Content != content)
            {
                Content = content;

                AddEvent(new PostContentEdited(BusinessId.Value, Content));
            }
        }

        public void Removed() => AddEvent(new PostRemoved(BusinessId.Value));
    }
}
