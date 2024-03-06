namespace Domain.Events.Accounts;

public class UserCreatedEvent : BaseEvent
{
    public UserCreatedEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
