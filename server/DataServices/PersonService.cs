using Server.DataContext;
using Server.Models;

namespace Server.DataServices;

public class PersonService
{
    private readonly SocialContext _context;
    public PersonService(SocialContext context)
    {
        _context = context;
    }

    public Person? GetPersonById(int id)
    {
        return _context.People.Find(id);
    }

    public IEnumerable<Person> All()
    {
        return _context.People.ToArray();
    }
}