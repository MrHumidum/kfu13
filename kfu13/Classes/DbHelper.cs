
namespace kfu13;
public class DbHelper
{
    public void SaveSubscriberToDb(Subscriber subscriber)
    {
        using var context = new MobileOperatorContext();
        context.Subscribers.Add(subscriber);
        context.SaveChanges();
    }

    public List<Subscriber> LoadSubscribersFromDb()
    {
        using var context = new MobileOperatorContext();
        return context.Subscribers.ToList();
    }
}
