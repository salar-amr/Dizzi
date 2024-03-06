namespace Domain.Events.Accounts;

public class UserDeletedEvent : BaseEvent
{
    public UserDeletedEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
